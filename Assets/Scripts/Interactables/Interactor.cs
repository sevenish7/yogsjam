using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Interactor : MonoBehaviour
{
    [SerializeField] Transform carryTransform = null;
    [SerializeField] private float interactRadius = 3f;
    [SerializeField] private Animator animator;

    public Pickupable CarriedPickup {get; private set; }
    public bool IsCarrying {get; private set; }

    public virtual bool CanInteract()
    {
        return true;
    }

    public void TryInteract()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRadius, 1 << LayerMask.NameToLayer(BaseInteractable.INTERACTABLE_LAYER));
        colliders = FilterCollidersForCurrentState(colliders);
        
        if(colliders.Length > 0)
        {
            foreach(var c in colliders)
            {
                var interactable = c.GetComponent<BaseInteractable>();
                if(interactable != null && interactable.CanInteract())
                {
                    interactable.Interact(this);
                    return;
                }
            }
        }

        if(IsCarrying)
        {
            PutdownCarried();
        }
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
            animator.SetTrigger("pickup");
            animator.SetBool("isCarrying", true);
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

        animator.SetTrigger("putdown");
        animator.SetBool("isCarrying", false);
    }

    private Collider[] FilterCollidersForCurrentState(Collider[] colliders)
    {
        IEnumerable<Collider> col = colliders.OrderBy(c => (c.transform.position - transform.position).sqrMagnitude);

        if(IsCarrying)
        {
            col = colliders.Where(c => c.GetComponent<BaseStation>() != null);//move all the stations to the front
        }

        return col.ToArray();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}