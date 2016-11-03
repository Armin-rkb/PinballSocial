using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    // The Text component we will use to display our score.
    [SerializeField] private Text scoreText;

    // Amount of score we have at the moment.
    private int totalScore = 0;

    void Start()
    {
        SetScoreText();
    }

    // Updating the displayed amount of score.
    void SetScoreText()
    {
        scoreText.text = "Score: " + totalScore;
    }
}
