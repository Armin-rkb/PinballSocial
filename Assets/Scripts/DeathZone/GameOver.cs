using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private GameObject ActionButtons;

    void Start()
    {
        // Making sure are menu's are set to default.
        GameOverMenu.SetActive(false);
        ActionButtons.SetActive(true);
    }

    void OnTriggerEnter(Collider coll)
    {
        // Stop the game when the ball hits the deathzone.
        if (coll.gameObject.CompareTag("Ball"))
            StopGame();
    }

    // Hide the player buttons and show the gameover screen.
    void StopGame()
    {
        GameOverMenu.SetActive(true);
        ActionButtons.SetActive(false);
    }
}