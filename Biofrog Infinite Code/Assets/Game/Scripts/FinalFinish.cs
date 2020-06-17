using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalFinish : MonoBehaviour
{
    public float textFadeTime, panelFadeTime;
    public Text finishText;
    public Image panel;
    public GameObject endCanvas;

    public string nextLevel;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            endCanvas.SetActive(true);
            StartCoroutine(FinalFinishC());
        }
    }

    IEnumerator FinalFinishC()
    {
        StartCoroutine(ImageFadeIn(panel));
        yield return new WaitForSeconds(panelFadeTime + 0.5f);
        StartCoroutine(TextFadeIn(finishText));
        yield return new WaitForSeconds(textFadeTime + 1.0f);
        StartCoroutine(TextFadeOut(finishText));
        yield return new WaitForSeconds(textFadeTime + 1.0f);
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Coroutine to fade in text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator TextFadeIn(Text text)
    {
        float t = 0;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (1 / textFadeTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, t);
            yield return null;
        }
    }

    /// <summary>
    /// Coroutine to fade out text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator TextFadeOut(Text text)
    {
        float t = 1.0f;
        while (t > 0)
        {
            t -= Time.deltaTime * (1 / textFadeTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, t);

            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// Coroutine to fade in text
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    IEnumerator ImageFadeIn(Image img)
    {
        float t = 0;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (1 / panelFadeTime);
            img.color = new Color(img.color.r, img.color.g, img.color.b, t);
            yield return null;
        }
    }

}
