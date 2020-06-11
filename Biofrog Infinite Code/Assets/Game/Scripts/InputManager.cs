using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject player;
    bool GamePaused = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyInput();
    }

    void HandleKeyInput()
    {
        //quit when esc is pressed
        /*if (Input.GetKeyUp(KeyCode.Escape))
        {
            player.GetComponent<Movement>().Squeak();
            Application.Quit();
        }*/

        //quit when Escape is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!GamePaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale=0;
                GamePaused = true;
            }
            else
            {
                UnPause();
            }
        }

        //make squeak sound when space is pressed
        if (Input.GetKeyDown("space"))
        {
            player.GetComponent<Movement>().Squeak();
        }

        //kill active frog
        if (Input.GetKeyDown("end"))
        {
            player.GetComponent<Death>().Die();
        }

    }

    public void UnPause()
    {
        Time.timeScale = 1;
        GamePaused = false;
        pauseMenu.SetActive(false);
    }

}
