using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    Button button;
    public void Start()
    {
        button = GetComponent<Button>();
        if(FindObjectOfType<LevelCounterThingy>().highestAchievedLevel < 1)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void OnClick()
    {
        SceneManager.LoadScene(FindObjectOfType<LevelCounterThingy>().highestAchievedLevel);
    }
}
