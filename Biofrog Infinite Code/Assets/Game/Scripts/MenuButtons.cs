using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    bool htpActive = false;
    public GameObject howToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
        AudioSource squeak = GetComponent<AudioSource>();
        GameObject.Find("audioListener").GetComponent<Audio>().PlaySound(squeak);
    }

    public void ToggleHowToPlay()
    {
        htpActive = !htpActive;
        howToPlay.SetActive(htpActive);
    }

    public void MainMenu()
    {
        AudioSource squeak = GetComponent<AudioSource>();
        GameObject.Find("audioListener").GetComponent<Audio>().PlaySound(squeak);
        SceneManager.LoadScene("MainMenu");
    }
}
