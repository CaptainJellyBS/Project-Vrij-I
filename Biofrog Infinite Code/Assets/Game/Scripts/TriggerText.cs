using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerText : MonoBehaviour
{
    bool shown = false;
    public Text tutorialText;

    void Start()
    {
        //Disable text if enabled
        //DisableText(); This causes NullReferenceExceptions, removing it seems to have fixed that
    }

    /// <summary>
    /// Call this to show the text linked to this trigger
    /// </summary>
    /// <param name="seconds">The amount of seconds to show the text before it disappears</param>
    public void ShowText(float seconds = 2.0f)
    {
        if(shown) { return; }
        shown = true;
        tutorialText.enabled = true;
        Invoke("DisableText", seconds);
    }

    /// <summary>
    /// Disable the text linked to this gameobject
    /// </summary>
    void DisableText()
    {
        tutorialText.enabled = false;
    }
}
