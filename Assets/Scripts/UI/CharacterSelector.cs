using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{

    [SerializeField] private string horizontalAxis;
    [SerializeField] private string confirmButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI readyLabel;
    [SerializeField] private TextMeshProUGUI characterNameLabel;
    [SerializeField] private float changeCooldown = 0.25f;//so if we hold down we dont cycle through everything instantly
    [SerializeField] private Transform characterSpawnLocation;
    [SerializeField] private float characterScaleFactor = 200f;
    [SerializeField] private Vector3 characterSpawnRotation = new Vector3(0, 0, 180);
    [SerializeField] private GameObject notSelectingCover;

    public CharacterType SelectedCharacter { get { return (CharacterType)currentSelectedCharacter; } }

    public bool IsReady { get { return isReady; } }
    public bool IsSelecting { get { return isSelecting; } }

    private bool isSelecting = false;
    private bool isReady = false;

    private bool isSwapCoolingdown = false;

    private float elapsedTime = 0;

    private int currentSelectedCharacter;

    private GameObject spawnedCharacter = null;

    void Start()
    {
        isSwapCoolingdown = false;
        isReady = false;
        currentSelectedCharacter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSelecting)
        {
            if(Input.GetButtonDown(confirmButton))
            {
                isSelecting = true;
                notSelectingCover.SetActive(false);
                currentSelectedCharacter = 1;
                UpdateSelectedCharacter();
            }
            return;
        }

        if(Input.GetButtonDown(confirmButton))
        {
            if(isReady)
            {
                Unready();
            }
            else
            {
                Ready();
            }
        }
        else if(isReady)
        {
            return;
        }

        if(isSwapCoolingdown)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > changeCooldown)
            {
                isSwapCoolingdown = false;
            }
        }
        else
        {
            ProcessControllerInput();
        }
    }

    private void Ready()
    {
        isReady = true;
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        readyLabel.text = "Ready";
    }

    private void Unready()
    {
        isReady = false;
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
        readyLabel.text = "Not Ready";
    }

    private void UpdateSelectedCharacter()
    {
        characterNameLabel.text = SelectedCharacter.ToString();
        if(spawnedCharacter != null)
        {
            Destroy(spawnedCharacter);
        }

        spawnedCharacter = Instantiate(CharacterSelectionLookupManager.Instance.GetPrefabForCharacterType(SelectedCharacter), characterSpawnLocation);
        spawnedCharacter.transform.localScale = Vector3.one * characterScaleFactor;
        spawnedCharacter.transform.rotation = Quaternion.Euler(characterSpawnRotation);
    }

    private void SwapLeft()
    {
        currentSelectedCharacter--;
        if(currentSelectedCharacter < 1)//0 is no character
        {
            currentSelectedCharacter = System.Enum.GetValues(typeof(CharacterType)).Length - 1;
        }
        UpdateSelectedCharacter();
    }

    private void SwapRight()
    {
        currentSelectedCharacter++;
        if(currentSelectedCharacter >= System.Enum.GetValues(typeof(CharacterType)).Length)//0 is no character
        {
            currentSelectedCharacter = 1;
        }
        UpdateSelectedCharacter();
    }

    private void ProcessControllerInput()
    {
        float horizontal = Input.GetAxis(horizontalAxis);
        if(horizontal > 0)
        {
            SwapRight();
            StartCooldown();
        }
        else if(horizontal < 0)
        {
            SwapLeft();
            StartCooldown();
        }
    }

    private void StartCooldown()
    {
        elapsedTime = 0f;
        isSwapCoolingdown = true;
    }
}
