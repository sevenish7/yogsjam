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

    public override void Interact(Interactor interactor)
    {
        if(!interactor.IsCarrying)
        {
            Pickupable p = SpawnPickup();
            p.Interact(interactor);
        }
    }

    private Pickupable SpawnPickup()
    {
        return Instantiate(spawnablePrefab, transform.position, Quaternion.identity);
    }
}
