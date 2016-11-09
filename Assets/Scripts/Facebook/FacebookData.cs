using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Facebook.Unity;

public class FacebookData : MonoBehaviour
{
    [SerializeField] private GameObject notLoggedInUI;
    [SerializeField] private GameObject loggedInUI;
    [SerializeField] private Image playerPictureUI;
    [SerializeField] private Text welcomeText;

    private Texture2D profilePicture;
    public Texture2D ProfilePicture
    {
        get { return profilePicture; }
    }

	void Awake ()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }
        ShowUI();

        DontDestroyOnLoad(gameObject);
	}

    void Start()
    {
        if (FB.IsLoggedIn)
            loggedInUI.SetActive(true);
        else if (!FB.IsLoggedIn)
            notLoggedInUI.SetActive(true);
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
    
    // Check if the login went succesfull.
    void LoginCallback(ILoginResult result)
    {
        if(result.Error == null)
        {
            print("Login went succesfull");

            // Getting the picture of the logged in user.
            FB.API("me/picture?width=100&height=100", HttpMethod.GET, PictureCallback);

            // Getting the name of the logged in user.
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
