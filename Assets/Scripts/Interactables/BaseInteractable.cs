using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class BaseInteractable : MonoBehaviour, IInteractable
{
    public const string INTERACTABLE_LAYER = "Interactable";
    
    public abstract bool CanInteract();

    public abstract bool Interact(IInteractor interactor);

}

public interface IInteractable
{
    bool Interact(IInteractor interactor);
    bool CanInteract();
}

