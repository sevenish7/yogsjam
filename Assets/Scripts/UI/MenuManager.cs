using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        //reset when we hit the menu in case we came from a game
        CharacterSelection.ResetCharacterData();
    }

    private void Update()
    {
        if(mainMenu.IsShowing)
        {
            if(Input.GetButtonDown("Jump"))
            {
                OpenCharacterSelect();
            }
        }
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("Main");
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
