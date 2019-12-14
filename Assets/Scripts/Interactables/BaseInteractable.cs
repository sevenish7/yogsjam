using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class BaseInteractable : MonoBehaviour, IInteractable
{
    public const string INTERACTABLE_LAYER = "Interactable";
    
    public abstract bool CanInteract();

    public abstract bool Interact(Interactor interactor);

    protected Collider coll;

    protected virtual void Awake()
    {
        coll = GetComponentInChildren<Collider>();
    }

}

public interface IInteractable
{
    bool Interact(Interactor interactor);
    bool CanInteract();
}

