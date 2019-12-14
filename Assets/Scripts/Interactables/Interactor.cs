using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Interactor : MonoBehaviour, IInteractor
{
    [SerializeField] Transform carryTransform = null;
    [SerializeField] private float interactRadius = 3f;

    private Pickupable carriedPickup = null;
    private bool isCarrying = false;

    public virtual bool CanInteract()
    {
        return true;
    }

    public void TryInteract()
    {
        if(isCarrying)
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
        if(!isCarrying)
        {
            carriedPickup = pickup;
            isCarrying = true;

            pickup.transform.SetParent(carryTransform, true);
            pickup.transform.position = carryTransform.position;
            // pickup.transform.DOMove(carryTransform.position, 0.2f);
            // pickup.transform.DORotate(Vector3.zero, 0.2f);
            return true;
        }
        return false;
    }

    private void PutdownCarried()
    {
        carriedPickup.transform.SetParent(null);
        carriedPickup.Putdown();
        isCarrying = false;
        carriedPickup = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}

public interface IInteractor
{
    bool CanInteract();
    void TryInteract();
}
