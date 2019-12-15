using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : BaseInteractable
{

    [SerializeField] private IngredientType ingredientType;
    [SerializeField] private IngredientPickup spawnablePrefab;

    public override bool CanInteract(Interactor interactor)
    {
        return !interactor.IsCarrying;
    }

    public override void Interact(Interactor interactor)
    {
        if(!interactor.IsCarrying)
        {
            IngredientPickup p = SpawnPickup();
            p.Interact(interactor);
        }
    }

    private IngredientPickup SpawnPickup()
    {
        IngredientPickup pickup = Instantiate(spawnablePrefab, transform.position, Quaternion.identity);
        pickup.InitialiseIngredientData(new IngredientData(ingredientType));
        return pickup;
    }
}
