using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private float returnToMenuAfter = 10f;
    [SerializeField] private TextMeshProUGUI timeoutLabel;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI timeSurvivedLabel;

    [SerializeField] private string scoreText = "You brought happiness to {0} little boys and girls all around the world!";
    [SerializeField] private string timeSurvivedText = "Santa put up with your shenanigans for {0} seconds.";


    private float timeElapsed = 0f;

    public void Show()
    {
        gameObject.SetActive(true);
        timeElapsed = 0f;

        scoreLabel.text = string.Format(scoreText, GameMode.Instance.Points);
        timeSurvivedLabel.text = string.Format(timeSurvivedText, Mathf.Floor(GameMode.Instance.Timer.TimeElapsed).ToString());
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Main");
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        timeoutLabel.text = "Returning to menu in " + Mathf.Floor(returnToMenuAfter - timeElapsed) + "...";
    
        if(timeElapsed > returnToMenuAfter)
        {
            timeElapsed = 0;
            ReturnToMenu();
        }
    }

}
