using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificationStation : BaseStation
{

    [SerializeField] private ProcessType processToApply;
    [SerializeField] private IngredientPickup ingredientPrefab;

    public override bool CanInteract(Interactor interactor)
    {
        if(completionCondition.IsRunning)
        {
            return completionCondition.CanInteract();
        }
        else
        {
            return interactor.CarriedPickup is IngredientPickup pickup && pickup.Ingredient.AppliedProcess == ProcessType.NONE;
        }
    }

    protected override void FinishProcess()
    {
        IngredientData input = containedIngredients[0];
        input.AppliedProcess = processToApply;

        IngredientPickup pickup = Instantiate(ingredientPrefab, finalProductSpawnTransform.position, finalProductSpawnTransform.rotation);
        pickup.InitialiseIngredientData(input);

        containedIngredients.Clear();
    }

}
