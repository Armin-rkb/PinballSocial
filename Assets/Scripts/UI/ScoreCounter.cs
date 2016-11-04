using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreCounter : MonoBehaviour
{
    // The Text component we will use to display our score.
    [SerializeField] private Text scoreText;

    // Amount of score we have at the moment.
    private int totalScore = 0;
    public int TotalScore
    {
        get { return totalScore; }
    }

    void Start()
    {
        SetScoreText();

        // Subscribing to the BallHit function, so when it hits we can add score.
        Bumper.BallHit += IncreaseScore;
    }

    // Increase our current score with the given amount of points.
    void IncreaseScore(Bumper bumper)
    {
        totalScore += bumper.BumpPoints;
        SetScoreText();
    }

    // Updating the displayed amount of score.
    void SetScoreText()
    {
        scoreText.text = "Score: " + totalScore;
    }
    
    // Unsubscribing when we change scenes.
    void OnDestroy()
    {
        Bumper.BallHit -= IncreaseScore;
    }
}
