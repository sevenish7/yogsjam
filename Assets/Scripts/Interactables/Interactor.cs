using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Interactor : MonoBehaviour
{
    [SerializeField] Transform carryTransform = null;
    [SerializeField] private float interactRadius = 3f;

    public Pickupable CarriedPickup {get; private set; }
    public bool IsCarrying {get; private set; }

    public virtual bool CanInteract()
    {
        return true;
    }

    public void TryInteract()
    {
        if(IsCarrying)
        {
        //if is carrying try to place object in station
            //if no stattion then put down object
            PutdownCarried();
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, interactRadius)
                .OrderBy(c => (c.transform.position - transform.position).sqrMagnitude)
                .ToArray();
            
            foreach(var c in colliders)
            {
                var interactable = c.GetComponent<BaseInteractable>();
                if(interactable != null && interactable.CanInteract())
                {
                    interactable.Interact(this);
                    break;
                }
            }
        }
        //if not carrying then try to start carrying
    }

    public bool Pickup(Pickupable pickup)
    {
        if(!IsCarrying)
        {
            CarriedPickup = pickup;
            IsCarrying = true;

            pickup.transform.SetParent(carryTransform, true);
            pickup.transform.position = carryTransform.position;
            // pickup.transform.DOMove(carryTransform.position, 0.2f);
            // pickup.transform.DORotate(Vector3.zero, 0.2f);
            return true;
        }
        return false;
    }

    public void PutdownCarried()
    {
        CarriedPickup.transform.SetParent(null);
        CarriedPickup.Putdown();
        IsCarrying = false;
        CarriedPickup = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}