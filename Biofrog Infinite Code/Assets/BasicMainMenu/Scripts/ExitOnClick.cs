using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOnClick : MonoBehaviour
{
    void Start()
    {

    }

    public void ExitGame()
    {
        AudioSource squeak = GetComponent<AudioSource>();
        GameObject.Find("audioListener").GetComponent<Audio>().PlaySound(squeak);
        Application.Quit();
    }
}
