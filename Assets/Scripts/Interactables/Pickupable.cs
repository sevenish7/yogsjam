using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : BaseInteractable
{

    private Rigidbody rb;

    private bool isPickedUp = false;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    public override bool CanInteract()
    {
        return !isPickedUp;
    }

    public override bool Interact(IInteractor interactor)
    {
        if(!isPickedUp)
        {
            if(interactor is Interactor i)
            {
                if(i.Pickup(this))
                {
                    Pickup();
                    return true;
                }
            }
        }
        return false;
    }

    private void Pickup()
    {
        isPickedUp = true;
        rb.isKinematic = true;
    }

    public void Putdown()
    {
        isPickedUp = false;
    }
}
