using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModificationStation : BaseStation
{

    [SerializeField] private ProcessType processToApply;
    [SerializeField] private IngredientPickup ingredientPrefab;

    protected override void FinishProcess()
    {
        IngredientData input = containedIngredients[0];
        input.AppliedProcess = processToApply;

        IngredientPickup pickup = Instantiate(ingredientPrefab, finalProductSpawnTransform.position, finalProductSpawnTransform.rotation);
        pickup.InitialiseIngredientData(input);

        containedIngredients.Clear();

        Debug.Log("process finished");
    }

}
