using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;

public class FacebookUserData : MonoBehaviour
{
    private Sprite userPictureSprite;
    public Sprite UserPictureSprite
    {
        get { return userPictureSprite; }
    }

    private Texture2D userPictureTex;
    public Texture2D UserPictureTex
    {
        get { return userPictureTex; }
    }

    private string userName;
    public string UserName
    {
        get { return userName; }
    }

    private UserGreeting userGreeting;
    [SerializeField] private FacebookLogin facebookLogin;

	void Awake ()
    {
        DontDestroyOnLoad(gameObject);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        
        userGreeting = FindObjectOfType<UserGreeting>();
    }

    void Start()
    {
        // Making sure we are logged out when starting.
        facebookLogin.Logout();
    }

    public void PictureCallback(IGraphResult result)
    {
        // Saving the profile picture as a texture for when we start the game.
        userPictureTex = result.Texture;

        // Saving the profile picture as a sprite to be displayed in the UI.
        userPictureSprite = Sprite.Create(userPictureTex, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f));

        // Update the UI.
        userGreeting.DisplayGreeting();
    }

    public void NameCallback(IGraphResult result)
    {
        // Getting and saving the name of our player to be displayed in the UI.
        IDictionary<string, object> playerName = result.ResultDictionary;
        userName = playerName["first_name"].ToString();

        // Update the UI.
        userGreeting.DisplayGreeting();
    }
}
