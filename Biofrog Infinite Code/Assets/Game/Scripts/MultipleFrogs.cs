using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleFrogs : MonoBehaviour
{
    //array for froggies
    public GameObject[] frogs;
    //which frog is active
    public int frogNumber;

    public GameObject followingCamera;
    public GameObject audioListener;
    //array for herons (and any other areahazards that are added)
    public GameObject[] areaHazards;

    void Start()
    {
        followingCamera = GameObject.Find("Main Camera");
        audioListener = GameObject.Find("audioListener");
        areaHazards = GameObject.FindGameObjectsWithTag("AreaHazard");

        //arrays start at 0
        frogNumber = 0;
        //put the playable frogs in the array
        frogs = GameObject.FindGameObjectsWithTag("Player");

        //give each frog a different random colour
        foreach (GameObject frog in frogs)
        {
            Color c = frog.GetComponent<FrogColour>().randomColor();
            frog.GetComponent<FrogColour>().SetColor(c);
        }

        //enable the movement script on the first frog
        frogs[frogNumber].GetComponent<Movement>().enabled = true;
        //let the camera follow the active frog
        followingCamera.GetComponent<CameraFollowNew>().player = frogs[frogNumber];
        //move audiolistener to frog (false ==> do not keep world position)
        audioListener.transform.SetParent(frogs[frogNumber].transform, false);
        //change active frog for all herons in the scene
        foreach (GameObject heron in areaHazards)
        {
            heron.GetComponent<AreaHazardScript>().player = frogs[frogNumber];
        }
    }

    /// <summary>
    /// Goes to next frog.
    /// </summary>
    public void NextFrog()
    {
        Debug.Log(frogs[frogNumber]);
        //if there are still frogs alive
        if (frogNumber < frogs.Length - 1)
        {
            //disable the movement script on the current frog and deactivate it

            //enable the movement script on the next frog
            frogs[frogNumber].GetComponent<Movement>().enabled = true;

            //let the camera follow the active frog
            followingCamera.GetComponent<CameraFollowNew>().player = frogs[frogNumber];
            //move audiolistener to new frog (false ==> do not keep world position)
            audioListener.transform.SetParent(frogs[frogNumber].transform, false);
            //change active frog for all herons in the scene
            foreach (GameObject heron in areaHazards)
            {
                heron.GetComponent<AreaHazardScript>().player = frogs[frogNumber];
            }
        }
        else
        {
            GameObject.Find("finish").GetComponent<Finish>().finishTextFuckYouUnity.SetActive(true);
        }
    }

    /// <summary>
    /// Make the current frog inactive. This had to be separated from the NextFrog method to achieve the delay in moving the camera.
    /// </summary>
    public void MakeCurrentFrogInactive()
    {
        frogs[frogNumber].GetComponent<Movement>().enabled = false;
        frogs[frogNumber].SetActive(false);
        frogNumber++;
    }
}
