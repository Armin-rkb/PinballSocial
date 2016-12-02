using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoAnimation : MonoBehaviour
{
    // Rotating
    [SerializeField] private Transform logoTrans;
    [SerializeField] private Transform endRotation;
    [SerializeField] private float rotationSpeed;
    private float addedRotationSpeed;
    
    // Scaling
    [SerializeField] private float scaleSpeed;

    // End Animation
    [SerializeField] private ParticleSystem particleEffect;
    [SerializeField] private Text creditsText;
    private float animationTime;
    [SerializeField] private float MaxAnimationTime;

    [SerializeField] private EndSplashScreen endSplashScreen;
    private bool animationEnded = false;

    private IEnumerator rotateLogo;
    private IEnumerator scaleLogo;
    private IEnumerator stopAnimation;
    private IEnumerator endAnimation;
    private IEnumerator fadeText;

    void Start ()
    {
        scaleLogo = ScaleLogo();
        rotateLogo = RotateLogo();
        stopAnimation = StopAnimation();
        endAnimation = EndAnimation();

        StartCoroutine(scaleLogo);
        StartCoroutine(rotateLogo);
        StartCoroutine(stopAnimation);
	}

    IEnumerator StopAnimation()
    {
        while (true)
        {
            animationTime += 0.5f;

            if (animationTime == MaxAnimationTime)
            {
                StopCoroutine(scaleLogo);
                StopCoroutine(rotateLogo);

                StartCoroutine(endAnimation);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    IEnumerator EndAnimation()
    {
        while (true)
        {
            if (!animationEnded)
            {
                logoTrans.rotation = Quaternion.Euler(0, 0, 0);
                StartCoroutine(FadeText(creditsText.color, 0.025f));
                particleEffect.Play();
            }
            animationEnded = true;

            yield return new WaitForSeconds(1.5f);

            StopCoroutine(endAnimation);
            StartCoroutine(endSplashScreen._fadeToNextScene);
        }
    }

    IEnumerator ScaleLogo()
    {
        while (true)
        {
            logoTrans.localScale = new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
            scaleSpeed += 0.02f;

            yield return new WaitForSeconds(0.015f);
        }
    }

    IEnumerator RotateLogo()
    {
        while (true)
        {
            addedRotationSpeed = 0.15f;
            rotationSpeed += addedRotationSpeed;

            if (rotationSpeed >= 50)
                rotationSpeed = 50;

            logoTrans.rotation = Quaternion.Slerp(logoTrans.rotation, endRotation.rotation, rotationSpeed * Time.deltaTime);

            yield return new WaitForSeconds(0.015f);
        }
    }

    IEnumerator FadeText(Color textColor, float fadeSpeed)
    {
        while (true)
        {
            textColor.a += fadeSpeed;
            creditsText.color = textColor;

            yield return new WaitForSeconds(0.025f);
        }
    }
}
