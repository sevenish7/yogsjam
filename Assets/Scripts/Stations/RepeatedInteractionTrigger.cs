using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedInteractionTrigger : MonoBehaviour, IStationCompletionCondition, ICompleteable
{

    [SerializeField] private int numInteractionsToComplete = 4;
    //might want some cooldown between button presses
    [SerializeField] private CompletionBar completionBar;
    [SerializeField] private ParticleSystem interactionFX;

    private int currentNumInteractions = 0;
    private Action onCompletion;

    public bool IsRunning { get { return isRunning; } }

    private bool isRunning = false;

    public void StartProcess(Action onCompletion)
    {
        currentNumInteractions = 0;
        isRunning = true;
        this.onCompletion = onCompletion;
        completionBar.HideShow(true);
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
        if(currentNumInteractions == numInteractionsToComplete)
        {
            onCompletion.Invoke();
            completionBar.HideShow(false);
            isRunning = false;
        }

    }

    public bool CanInteract()
    {
        return true;
    }

    public float GetPercentComplete()
    {
        return (float)currentNumInteractions / (float)numInteractionsToComplete;
    }
}
