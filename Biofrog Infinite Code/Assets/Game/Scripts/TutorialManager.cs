using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hardcoded tutorial manager script
/// </summary>
public class TutorialManager : MonoBehaviour
{
    public Text LeftMouseText, RightMouseText, CameraText, LilyText;
    public float fadeSpeed = 1.0f;
    void Start()
    {
        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        LeftMouseText.enabled = true;
        StartCoroutine(TextFadeIn(LeftMouseText));
        RightMouseText.enabled = false; CameraText.enabled = false; LilyText.enabled = false;

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(TextFadeOut(LeftMouseText));
        yield return new WaitForSeconds(1.5f);
        //Hide first text, show second text

        LeftMouseText.enabled = false;

        yield return new WaitForSeconds(1.0f);
        RightMouseText.enabled = true;
        StartCoroutine(TextFadeIn(RightMouseText));

        while (!Input.GetMouseButtonDown(1))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(TextFadeOut(RightMouseText));

        yield return new WaitForSeconds(1.5f);

        RightMouseText.enabled = false;

        yield return new WaitForSeconds(1.0f);
        CameraText.enabled = true;
        StartCoroutine(TextFadeIn(CameraText));

        while (!(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(TextFadeOut(CameraText));

        yield return new WaitForSeconds(1.5f);
        //Hide text, show next text
        CameraText.enabled = false;

        yield return new WaitForSeconds(1.0f);
        LilyText.enabled = true;
        StartCoroutine(TextFadeIn(LilyText));

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(TextFadeOut(LilyText));

        yield return new WaitForSeconds(1.5f);
        LilyText.enabled = false;
        //Hide text

    }

    IEnumerator TextFadeIn(Text text)
    {
        float t = 0;
        while(t<1.0f)
        {
            t += Time.deltaTime * fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, t);
            yield return null;
        }
    }

    IEnumerator TextFadeOut(Text text)
    {
        float t = 1.0f;
        while (t > 0)
        {
            t -= Time.deltaTime * fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, t);

            yield return null;
        }
        yield return null;
    }
}
