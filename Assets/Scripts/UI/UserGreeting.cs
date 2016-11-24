using UnityEngine;
using UnityEngine.UI;

public class UserGreeting : MonoBehaviour
{
    [SerializeField] private Image welcomePicture;
    [SerializeField] private Text welcomeText;

    private FacebookUserData facebookUserData;

    void Awake()
    {
        facebookUserData = FindObjectOfType<FacebookUserData>();
    }

    void Start()
    {
        DisplayGreeting();
    }

    public void DisplayGreeting()
    {
        // Getting the profile picture and displaying it on the UI.
        if (welcomePicture != null)
            welcomePicture.sprite = facebookUserData.UserPictureSprite;

        // Setting the name in our welcome text.
        welcomeText.text = "Welcome, " + facebookUserData.UserName;
    }
}
