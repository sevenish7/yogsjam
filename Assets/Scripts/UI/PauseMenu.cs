using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void HideShow(bool show)
    {
        gameObject.SetActive(show);
    }
}
