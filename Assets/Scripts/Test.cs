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
    private GameObject LoggedInUI;

	void Awake ()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }
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

    // Check if the login went succesfull.
    void LoginCallback(ILoginResult result)
    {
        if(result.Error == null)
        {
            print("Login went succesfull");
        }
        else
        {
            Debug.Log("Login error: " + result.Error);
        }
    }
}
