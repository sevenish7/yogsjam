using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public abstract class BaseStation : BaseInteractable
{
    [SerializeField] protected int containerSize = 1;
    [SerializeField] protected Transform finalProductSpawnTransform;
    [OdinSerialize] protected IStationCompletionCondition completionCondition;

    protected List<IngredientData> containedIngredients = new List<IngredientData>();
    protected int NumContents { get { return containedIngredients.Count; } }

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
            }
        }
        else
        {
            completionCondition.OnInteract();
        }
    }

    protected abstract void FinishProcess();
}

public interface IStationCompletionCondition
{
    void StartProcess(System.Action onCompletion);

    void OnInteract();

    bool CanInteract();

    bool IsRunning { get; }
}
