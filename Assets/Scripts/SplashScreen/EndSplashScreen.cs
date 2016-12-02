using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndSplashScreen : MonoBehaviour
{
    [SerializeField] private Image fadeScreen;
    [SerializeField] private string sceneName;
    private Color fadeColor;
    private IEnumerator fadeToNextScene;
    public IEnumerator _fadeToNextScene
    {
        get { return fadeToNextScene; }
    }

    void Start()
    {
        fadeColor = fadeScreen.color;

        fadeToNextScene = FadeToNextScene();
        StartCoroutine(SkipSplashScreen());
    }

    IEnumerator SkipSplashScreen()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
                ChangeScene();
            if (Input.touchCount > 0)
                ChangeScene();

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeToNextScene()
    {
        while (true)
        {
            if (fadeScreen.color.a < 1)
            {
                fadeColor.a += 0.025f;
                fadeScreen.color = fadeColor;
            }

            else if (fadeScreen.color.a >= 1)
                ChangeScene();

            yield return new WaitForSeconds(0.025f);
        }
    }

    void ChangeScene()
    {
        // Check if the scene name is given before changing to the next scene.
        if (string.IsNullOrEmpty(sceneName))
           Debug.LogError("Please name the next scene in the inspector.");
        else
            SceneManager.LoadScene(sceneName);
    }
}
