using UnityEngine;

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
        if (coll.gameObject.CompareTag(Tags.ball))
            StopGame(coll.GetComponent<Rigidbody>());
    }

    // Hide the player buttons and show the gameover screen.
    void StopGame(Rigidbody ball)
    {
        Destroy(ball); // Destroying our ball's physics so it wont bounce everywhere when we are gameover. 

        GameOverMenu.SetActive(true);
        ActionButtons.SetActive(false);
    }
}