using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = GameMode.Instance.Points.ToString();
    }
}
