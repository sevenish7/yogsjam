using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    [SerializeField] private List<CharacterSelector> characterSelectors = new List<CharacterSelector>();

    [SerializeField] private TextMeshProUGUI countdownLabel;
    [SerializeField] private float countDownLength = 5f;
    [SerializeField] private MenuManager menuManager;

    private float elapsedTime = 0f;

    private bool isCountingDown = false;

    private void Update()
    {
        CheckIfShouldBeCountingDown();

        if(isCountingDown)
        {
            elapsedTime += Time.deltaTime;
            
            if(elapsedTime > countDownLength)
            {
                CompleteCountdown();
            }
            else
            {
                countdownLabel.text = "Starting in " + Mathf.FloorToInt(countDownLength - elapsedTime).ToString();   
            }
        }
    }

    private void CheckIfShouldBeCountingDown()
    {
        if(!characterSelectors.Any(cs => cs.IsSelecting))
        {//handle case where noone is selecting
            return;
        }

        bool shouldCountdown = true;
        foreach(var cs in characterSelectors)
        {
            if(!cs.IsSelecting)
            {//skip those who arent selecting, we dont care about them
                continue;
            }

            if(!cs.IsReady)
            {
                shouldCountdown = false;
            }
        }

        if(shouldCountdown && !isCountingDown)
        {
            StartCountdown();
        }
        else if(!shouldCountdown && isCountingDown)
        {
            ResetCountdown();
        }
    }

    private void StartCountdown()
    {
        isCountingDown = true;
        elapsedTime = 0;        
        countdownLabel.gameObject.SetActive(true);
    }

    private void ResetCountdown()
    {
        isCountingDown = false;
        elapsedTime = 0;
        countdownLabel.gameObject.SetActive(false);
    }

    private void CompleteCountdown()
    {
        ResetCountdown();

        for(int i = 0; i < characterSelectors.Count; i++)
        {
            CharacterSelection.Players[i] = characterSelectors[i].SelectedCharacter;
            Debug.Log("Player " + i + " selected " + characterSelectors[i].SelectedCharacter.ToString());
        }

        menuManager.BeginGame();
    }
}
