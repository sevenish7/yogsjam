using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedInteractionTrigger : MonoBehaviour, IStationCompletionCondition
{

    [SerializeField] private int numInteractionsToComplete = 4;
    //might want some cooldown between button presses
    [SerializeField] private ParticleSystem interactionFX;

    private int currentNumInteractions = 0;
    private Action onCompletion;

    private bool isRunning = false;

    public void StartProcess(Action onCompletion)
    {
        currentNumInteractions = 0;
        isRunning = true;
        this.onCompletion = onCompletion;
    }

    public void OnInteract()
    {
        if(!isRunning)
        {
            return;
        }

        if(interactionFX != null)
        {
            interactionFX.Play(true);
        }
        
        currentNumInteractions++;
        Debug.Log(currentNumInteractions);
        if(currentNumInteractions == numInteractionsToComplete)
        {
            onCompletion.Invoke();
            isRunning = false;
        }

    }

    public bool CanInteract()
    {
        return true;
    }
}
