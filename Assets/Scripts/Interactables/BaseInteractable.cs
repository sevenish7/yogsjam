using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class BaseInteractable : SerializedMonoBehaviour, IInteractable
{
    public const string INTERACTABLE_LAYER = "Interactable";
    
    public abstract bool CanInteract();

    public abstract void Interact(Interactor interactor);

    protected Collider coll;

    protected virtual void Awake()
    {
        coll = GetComponentInChildren<Collider>();
    }

}

public interface IInteractable
{
    void Interact(Interactor interactor);
    bool CanInteract();
}

