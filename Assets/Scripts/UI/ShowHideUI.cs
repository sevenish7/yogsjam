using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] private bool shownByDefault = false;

    public bool IsShowing { get { return isShowing; } }
    private bool isShowing = false;

    private void Awake()
    {
        if(shownByDefault)
        {
            Show();
        }
    }

    public void Show()
    {
        isShowing = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isShowing = false;
        gameObject.SetActive(false);
    }

    public void ToggleShowHide()
    {
        isShowing = !isShowing;
        if(isShowing)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

}
