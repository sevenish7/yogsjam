using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryStation : BaseInteractable
{
    public override bool CanInteract(Interactor interactor)
    {
        return interactor.IsCarrying && interactor.CarriedPickup is ProductPickup;
    }

    public override void Interact(Interactor interactor)
    {
        if(interactor.IsCarrying && interactor.CarriedPickup is ProductPickup product)
        {
            GameMode.Instance.DeliverProduct(product.Product);
            interactor.PutdownCarried();
            Destroy(product.gameObject);
        }
    }
}
