using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton; 

    [SerializeField] private ShowHideUI mainMenu;
    [SerializeField] private ShowHideUI characterSelect;

    private void Start()
    {
        playButton.onClick.AddListener(OpenCharacterSelect);
        quitButton.onClick.AddListener(Quit);
    }

    private void OpenCharacterSelect()
    {
        mainMenu.Hide();
        characterSelect.Show();
    }

    private void Quit()
    {
        Application.Quit();
    }

}
