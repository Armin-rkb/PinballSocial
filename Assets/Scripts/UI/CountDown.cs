using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour
{
    // The text component we will use to display the countdown.
    [SerializeField] private Text countDownText;
    // Amount of time we have until we can play.
    [SerializeField] private int time = 3;
    // The UI that we will disable once the countdown is finished.
    [SerializeField] private GameObject countDownUI;
    // The background of our UI that we will smoothly fade.
    [SerializeField] private Image backgroundImage;
    // Wall that stops us from having a head start.
    [SerializeField] private GameObject springWall;

    [SerializeField] private CameraFollow cameraFollow;
    private IEnumerator startCountDown;

    void Start()
    {
        countDownUI.SetActive(true);

        StartCoroutine(FadeBackground(backgroundImage.color, 0.025f));
        startCountDown = StartCountDown();
        StartCoroutine(startCountDown);
    }

    IEnumerator StartCountDown()
    {
        while (true)
        {
            time--;
            SetCountdownText();
            yield return new WaitForSeconds(1);
        }
    }

    void SetCountdownText()
    {
        switch(time)
        {
            case 1:
                countDownText.text = time.ToString();
                cameraFollow.IsWaiting = false;
                break;
            case 0:
                countDownText.text = "Start!";
                springWall.SetActive(false);
                break;
            case -1:
                StopCoroutine(startCountDown);
                countDownUI.SetActive(false);
                break;
            default:
                countDownText.text = time.ToString();
                break;
        }
    }

    IEnumerator FadeBackground(Color imageColor, float fadeTime)
    {
        while (true)
        {
            imageColor.a -= fadeTime;
            backgroundImage.color = imageColor;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
