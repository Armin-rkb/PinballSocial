using UnityEngine;
using Facebook.Unity;
using System.Collections;

public class Ball : MonoBehaviour
{
    private FacebookData facebookData;
    private Texture2D texture;
    [SerializeField] private Renderer rend;

    void Awake()
    {
        facebookData = FindObjectOfType<FacebookData>();
    }

	void Start ()
    {
        if (FB.IsLoggedIn)
        {
            print("We are Logged in! :)");
            ChangeMaterialTexture();
        }

        else if(!FB.IsLoggedIn)
            print("We are NOT Logged in :(");
    }

    void ChangeMaterialTexture()
    {
        texture = facebookData.ProfilePicture;
        rend.material.mainTexture = texture;
    }
}
