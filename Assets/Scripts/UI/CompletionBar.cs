using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class CompletionBar : SerializedMonoBehaviour
{
    [SerializeField] private Slider bar;
    [SerializeField] private ICompleteable target;

    public void HideShow(bool show)
    {
        gameObject.SetActive(show);
    }

    private void Update()
    {
        bar.value = target.GetPercentComplete();
    }
}

public interface ICompleteable
{
    float GetPercentComplete();
}
