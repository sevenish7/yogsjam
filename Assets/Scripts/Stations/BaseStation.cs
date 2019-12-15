using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public abstract class BaseStation : BaseInteractable
{
    [SerializeField] protected int containerSize = 1;
    [SerializeField] protected List<Transform> containedItemTransforms = new List<Transform>();
    [SerializeField] protected Transform finalProductSpawnTransform;
    [OdinSerialize] protected IStationCompletionCondition completionCondition;

    protected List<IngredientData> containedIngredients = new List<IngredientData>();
    protected int NumContents { get { return containedIngredients.Count; } }

    protected List<GameObject> spawnedContainedItemVisuals = new List<GameObject>();

    public override bool CanInteract(Interactor interactor)
    {
        return completionCondition.CanInteract();
    }

    public override void Interact(Interactor interactor)
    {
        if(interactor.IsCarrying)
        {
            if((NumContents < containerSize) && interactor.CarriedPickup is IngredientPickup pickup)
            {
                containedIngredients.Add(pickup.Ingredient);
                if(NumContents == containerSize)
                {
                    completionCondition.StartProcess(FinishProcess);
                }

                interactor.PutdownCarried();
                Destroy(pickup.gameObject);

                SpawnContainedItemVisuals();
            }
        }
        else
        {
            completionCondition.OnInteract();
        }
    }

    protected virtual void FinishProcess()
    {
        CleanupContainerVisuals();
    }

    protected void SpawnContainedItemVisuals()
    {
        CleanupContainerVisuals();

        for(int i = 0; i < containedIngredients.Count; i++)
        {
            GameObject prefab = IngredientDataLookupManager.Instance.GetPrefabForIngredientType(containedIngredients[i]);
            GameObject spawned = Instantiate(prefab, containedItemTransforms[i]);

            spawnedContainedItemVisuals.Add(spawned);
        }
    }

    protected void CleanupContainerVisuals()
    {
        foreach(GameObject go in spawnedContainedItemVisuals)
        {
            if(go != null)
            {
                Destroy(go);
            }
        }
        spawnedContainedItemVisuals.Clear();
    }
}

public interface IStationCompletionCondition
{
    void StartProcess(System.Action onCompletion);

    void OnInteract();

    bool CanInteract();

    bool IsRunning { get; }
}
