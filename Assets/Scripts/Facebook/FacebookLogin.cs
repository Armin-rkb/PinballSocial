using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FacebookLogin : MonoBehaviour
{
    [SerializeField] private GameObject notLoggedInUI;
    [SerializeField] private GameObject loggedInUI;

    private IEnumerator checkSuccesfulLogout;
    private FacebookUserData facebookUserData;

	void Awake ()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }

        facebookUserData = FindObjectOfType<FacebookUserData>();

        ShowUI();
	}

    void Start()
    {
        if (FB.IsLoggedIn)
            loggedInUI.SetActive(true);
        else if (!FB.IsLoggedIn)
            notLoggedInUI.SetActive(true);

        checkSuccesfulLogout = CheckSuccesfulLogin();
    }
    
    void InitCallback()
    {
        print("Facebook succesfully initialized!");
        // Making sure we are logged out, when starting.
        Logout();
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
        FB.LogOut();
        StartCoroutine(checkSuccesfulLogout);
    }

    IEnumerator CheckSuccesfulLogin()
    {
        while (true)
        {
            if (FB.IsLoggedIn)
            {
                FB.LogOut();
            }
            else
            {
                ShowUI();
                StopCoroutine(checkSuccesfulLogout);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    
    // Check if the login went succesfull.
    void LoginCallback(ILoginResult result)
    {
        if(result.Error == null)
        {
            print("Login went succesfull");

            // Getting the picture of the logged in user.
            FB.API("me/picture?width=100&height=100", HttpMethod.GET, facebookUserData.PictureCallback);

            // Getting the name of the logged in user.
            FB.API("me?fields=first_name", HttpMethod.GET, facebookUserData.NameCallback);
            
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
}
