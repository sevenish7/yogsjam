using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField, FoldoutGroup("InputBindings")] 
    private string interactBinding = "Fire1";
    [SerializeField, FoldoutGroup("InputBindings")] 
    private string horizontalAxisBinding = "Horizontal";
    [SerializeField, FoldoutGroup("InputBindings")] 
    private string verticalAxisBinding = "Vertical";

    [SerializeField] private int playerNum = 1;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float turnSpeed = 0.5f;

    [SerializeField] private Interactor interactor = null;
    [SerializeField] private Transform characterMeshParent = null;
    
    private Animator animator = null;

    private CharacterController characterController;

    private void Awake()
    {
        //move this into the spawn player function
        characterController = GetComponentInChildren<CharacterController>();
    }

    private void Start()
    {
        SpawnCharacterPrefab();
        interactor.Initialise(animator);
    }

    private void SpawnCharacterPrefab()
    {
        CharacterType chosenCharacter = CharacterSelection.Players[playerNum];

        if(chosenCharacter != CharacterType.NONE)
        {
           GameObject prefab = CharacterSelectionLookupManager.Instance.GetPrefabForCharacterType(chosenCharacter);
           GameObject spawned = Instantiate(prefab, characterMeshParent);
           animator = spawned.GetComponentInChildren<Animator>(true);
        }
        else
        {
            gameObject.SetActive(false);//we arent playing this round
        }

    }

    void Update()
    {
        Move();
        if(Input.GetButtonDown(interactBinding))
        {
            interactor.TryInteract();
        }
    }

    private void Move()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis(horizontalAxisBinding);
        dir.z = Input.GetAxis(verticalAxisBinding);

        characterController.transform.forward = Vector3.MoveTowards(characterController.transform.forward, dir, turnSpeed);

        Vector3 delta = (dir * speed * Time.deltaTime);

        characterController.Move(delta);

        if(animator != null)
        {
            animator.SetFloat("velX", Mathf.Abs(characterController.velocity.x));
            animator.SetFloat("velZ", Mathf.Abs(characterController.velocity.z));
        }
    }
}
