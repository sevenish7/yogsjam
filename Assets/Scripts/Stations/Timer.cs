using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour, IStationCompletionCondition, ICompleteable
{
    [SerializeField] private float duration;
    [SerializeField] private CompletionBar completionBar;
    private float timeElapsed = 0;

    public bool IsRunning { get; private set; }

    private System.Action onComplete;

    public void StartProcess(System.Action onComplete)
    {
        if(IsRunning)
        {//cant be running already
            return;
        }

        completionBar.HideShow(true);

        IsRunning = true;
        timeElapsed = 0;
        this.onComplete = onComplete;
    }

    public void OnInteract()
    {

    }
    
    private void Update()
    {
        if(IsRunning)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > duration)
            {
                IsRunning = false;
                completionBar.HideShow(false);
                onComplete.Invoke();
            }
        }
    }

    public bool CanInteract()
    {
        return !IsRunning;
    }

    public float GetPercentComplete()
    {
        return timeElapsed / duration;
    }
}
