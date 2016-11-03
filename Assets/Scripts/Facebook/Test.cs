using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Facebook.Unity;

public class Test : MonoBehaviour
{
    [SerializeField]
    private GameObject notLoggedInUI;
    [SerializeField]
    private GameObject loggedInUI;
    [SerializeField]
    private Image playerPictureUI;
    private Texture2D profilePicture;

    [SerializeField]
    private Text welcomeText;

	void Awake ()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }
        ShowUI();
	}

    void InitCallback()
    {
        print("Facebook succesfully initialized!");
    }

    // Login function for our button.
    public void Login()
    {
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> {"user_friends"}, LoginCallback);
        }
    }

    // Logout function for our button.
    public void Logout()
    {
        if (FB.IsLoggedIn)
        {
            FB.LogOut();
            ShowUI();
        }
    }

    public void Share()
    {
        // Een post maken waar je een score kan sharen.
    }

    public void Invite()
    {
        FB.AppRequest(message: "This pinball game is lots of fun! Play it right now!", title: "Try this out!");
    }

    // Check if the login went succesfull.
    void LoginCallback(ILoginResult result)
    {
        if(result.Error == null)
        {
            print("Login went succesfull");

            // Getting the picture of the logged in user.
            FB.API("me/picture?width=100&height=100", HttpMethod.GET, PictureCallback);

            // Gettomg the name of the logged in user.
            FB.API("me?fields=first_name", HttpMethod.GET, NameCallback);

            ShowUI();
        }
        else
        {
            Debug.Log("Login error: " + result.Error);
        }
    }

    // Change the UI depending on if we are logged in.
    void ShowUI()
    {
        if (FB.IsLoggedIn)
        {
            loggedInUI.SetActive(true);
            notLoggedInUI.SetActive(false);
        }
        else
        {
            loggedInUI.SetActive(false);
            notLoggedInUI.SetActive(true);
        }
    }

    void PictureCallback(IGraphResult result)
    {
        profilePicture = result.Texture;
        // Setting the profile picture in the UI.
        playerPictureUI.sprite = Sprite.Create(profilePicture, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));
    }

    void NameCallback(IGraphResult result)
    {
        IDictionary<string, object> playerName = result.ResultDictionary;
        // Setting the name in our welcome text.
        welcomeText.text = "Welcome, " + playerName["first_name"];
    }
}
