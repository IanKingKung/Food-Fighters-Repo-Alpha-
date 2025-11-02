using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI introText;
    public Image background;
    public float fadeDuration = 2f;
    public float holdDuration = 2f;
    private string nextSceneName = "Scene";

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

IEnumerator PlayIntro()
{
    // Start fully invisible
    Color textColor = introText.color;
    Color bgColor = background.color;
    textColor.a = 0;
    bgColor.a = 1; // keep background black at start
    introText.color = textColor;
    background.color = bgColor;

    float fadeDuration = 3f; 
    float holdDuration = 2f;    //needs to be 40 seconds

    //Fade in
    float t = 0;
    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        float alpha = Mathf.Lerp(0, 1, t / fadeDuration);
        textColor.a = alpha;
        introText.color = textColor;
        yield return null;
    }

    //give player time to read
    yield return new WaitForSeconds(holdDuration);

    //fade out
    t = 0;
    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
        textColor.a = alpha;
        bgColor.a = alpha; // fade out background too
        introText.color = textColor;
        background.color = bgColor;
        yield return null;
    }

    //Load Game Scene
    SceneManager.LoadScene(nextSceneName);
}

}
