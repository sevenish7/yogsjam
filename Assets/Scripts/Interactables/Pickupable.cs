using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : BaseInteractable
{

    private Rigidbody rb;

    private bool isPickedUp = false;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInChildren<Rigidbody>();
    }

    public override bool CanInteract()
    {
        return !isPickedUp;
    }

    public override void Interact(Interactor interactor)
    {
        if(!isPickedUp)
        {
            if(interactor.Pickup(this))
            {
                Pickup();
            }
        }
    }

    private void Pickup()
    {
        isPickedUp = true;
        rb.isKinematic = true;
        coll.enabled = false;
    }

    public void Putdown()
    {
        isPickedUp = false;
        rb.isKinematic = false;
        coll.enabled = true;
    }
}
