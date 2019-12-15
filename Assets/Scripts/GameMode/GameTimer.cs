using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour, ICompleteable
{
    [SerializeField] private float duration;
    [SerializeField] private CompletionBar completionBar;
    private float timeElapsed = 0;

    public bool IsRunning { get; private set; }

    public float TimeElapsed { get { return timeElapsed; } }

    private System.Action onComplete;

    public void Initialise(System.Action onComplete)
    {
        IsRunning = true;
        timeElapsed = 0;
        this.onComplete = onComplete;
    }

    public void AddTime(float toAdd)
    {
        duration += toAdd;
    }
    
    private void Update()
    {
        if(IsRunning)
        {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > duration)
            {
                IsRunning = false;
                onComplete.Invoke();
            }
        }
    }

    public float GetPercentComplete()
    {
        return timeElapsed / duration;
    }
}
