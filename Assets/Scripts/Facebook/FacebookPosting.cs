using UnityEngine;
using Facebook.Unity;
using System;

public class FacebookPosting : MonoBehaviour
{
    // Making a scoreCounter variable so we can access our totalScore.
    [SerializeField] private ScoreCounter scoreCounter;

    public void ShareScore()
    {
        if (FB.IsLoggedIn)
        {
            FB.FeedShare(null, 
                link: new Uri("http://armin-rkb.com/"),
                linkName: "I just scored: " + scoreCounter.TotalScore + " Points on PinballSocial!",
                linkCaption: "PinballScocial",
                linkDescription: "Made by: Armin Karimi Birgani",
                picture: new Uri ("http://armin-rkb.com/wp-content/uploads/2016/10/O_B_R_Bird_Big.png"),
                mediaSource: null);
        }
    }
}
