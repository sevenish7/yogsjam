using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : BaseInteractable
{

    [SerializeField] private Pickupable spawnablePrefab;

    public override bool CanInteract()
    {
        return true;
    }

    public override bool Interact(Interactor interactor)
    {
        if(!interactor.IsCarrying)
        {
            Pickupable p = SpawnPickup();
            p.Interact(interactor);
            return true;
        }
        return false;
    }

    private Pickupable SpawnPickup()
    {
        return Instantiate(spawnablePrefab, transform.position, Quaternion.identity);
    }
}
