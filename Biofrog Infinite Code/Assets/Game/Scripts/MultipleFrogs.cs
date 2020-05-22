﻿using System.Collections;
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

    void Start()
    {
        followingCamera = GameObject.Find("Main Camera");
        audioListener = GameObject.Find("audioListener");

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
        followingCamera.GetComponent<CameraFollow>().player = frogs[frogNumber];
        //move audiolistener to new frog (false ==> do not keep world position)
        audioListener.transform.SetParent(frogs[frogNumber].transform, false); 
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
            frogs[frogNumber].GetComponent<Movement>().enabled = false;
            frogs[frogNumber].SetActive(false);
            frogNumber++;
            //enable the movement script on the next frog
            frogs[frogNumber].GetComponent<Movement>().enabled = true;
            //let the camera follow the active frog
            followingCamera.GetComponent<CameraFollow>().player = frogs[frogNumber];
        }
        else
        {
            GameObject.Find("finish").GetComponent<Finish>().finishTextFuckYouUnity.SetActive(true);
        }
    }
}
