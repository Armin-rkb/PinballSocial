using UnityEngine;
using Facebook.Unity;

public class Ball : MonoBehaviour
{
    private FacebookUserData facebookUserData;
    private Texture2D texture;
    [SerializeField] private Renderer rend;

    void Awake()
    {
        facebookUserData = FindObjectOfType<FacebookUserData>();
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
        texture = facebookUserData.UserPictureTex;
        rend.material.mainTexture = texture;
    }
}
