using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultipleFrogs : MonoBehaviour
{
    //array for froggies
    public GameObject[] frogs;
    //which frog is active
    public int frogNumber;
    public GameObject activefrog;
    Texture2D cursor;

    //gameobjects that need to know which frog is active
    public GameObject followingCamera;
    public GameObject audioListener;
    public GameObject eventManager;
    //array for herons (and any other areahazards that are added)
    public GameObject[] areaHazards;

    void Start()
    {
        followingCamera = GameObject.Find("Main Camera");
        audioListener = GameObject.Find("audioListener");
        eventManager = GameObject.Find("EventSystem");
        areaHazards = GameObject.FindGameObjectsWithTag("AreaHazard");

        //arrays start at 0
        frogNumber = 0;
        //put the playable frogs in the array
        frogs = GameObject.FindGameObjectsWithTag("Player");

        //give each frog a different random colour and dectivate their movement & trigger scripts
        foreach (GameObject frog in frogs)
        {
            Color c = frog.GetComponent<FrogColour>().randomColor();
            frog.GetComponent<FrogColour>().SetColor(c);
            frog.GetComponent<Movement>().enabled = false;
            frog.GetComponent<TriggerManager>().enabled = false;
        }
        //set the first frog as active frog
        activefrog = frogs[frogNumber];

        //enable the movement & trigger script on the first frog
        activefrog.GetComponent<Movement>().enabled = true;
        activefrog.GetComponent<TriggerManager>().enabled = true;

        ChangePlayerInScripts();
    }

    /// <summary>
    /// Goes to next frog.
    /// </summary>
    public void NextFrog()
    {
        Debug.Log(frogs[frogNumber]);
        //if there are still frogs alive
        if (frogNumber < frogs.Length)
        {            
            //set the new active frog as active frog
            activefrog = frogs[frogNumber];

            //enable the movement & trigger script on the next frog
            activefrog.GetComponent<Movement>().enabled = true;
            activefrog.GetComponent<TriggerManager>().enabled = true;

            ChangePlayerInScripts();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameObject.Find("finish").GetComponent<Finish>().finishTextFuckYouUnity.SetActive(true);
        }
    }

    /// <summary>
    /// Make the current frog inactive. This had to be separated from the NextFrog method to achieve the delay in moving the camera.
    /// </summary>
    public void MakeCurrentFrogInactive()
    {
        //disable the movement & trigger script on the next frog
        activefrog.GetComponent<Movement>().enabled = false;
        activefrog.GetComponent<TriggerManager>().enabled = false;
        activefrog.SetActive(false);
        frogNumber++;
        if (frogNumber == frogs.Length)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //GameObject.Find("finish").GetComponent<Finish>().finishTextFuckYouUnity.SetActive(true);
        }
    }

    /// <summary>
    /// Changes the player referenced in different scripts to the currently active frog
    /// </summary>
    public void ChangePlayerInScripts()
    {
        //let the camera follow the active frog
        followingCamera.GetComponent<CameraFollowNew>().player = activefrog;
        //move audiolistener to new frog (false ==> do not keep world position)
        //audioListener.transform.SetParent(activefrog.transform, false);
        audioListener.GetComponent<Audio>().SetPlayer(activefrog);

        //have Inputmanager change (kill) right frog
        eventManager.GetComponent<InputManager>().player = activefrog;

        //change active frog for all herons in the scene
        foreach (GameObject heron in areaHazards)
        {
            heron.GetComponent<AreaHazardScript>().player = activefrog;
        }
    }

}
