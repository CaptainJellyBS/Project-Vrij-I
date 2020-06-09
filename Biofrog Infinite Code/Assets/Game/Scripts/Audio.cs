using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    GameObject currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    /// <summary>
    /// play a squeak (or other sound given through the parameter, might change to more general funtion later)
    /// </summary>
    /// <param name="squeak"></param>
    public void PlaySound(AudioSource squeak)
    {
        //Check to see if the squeak should be played
        if (!squeak.isPlaying)
        {
            //Play the squeak
            squeak.Play();
        }
    } 

    /// <summary>
    /// Set player that the audio listener follows
    /// </summary>
    /// <param name="frog"></param>
    public void SetPlayer(GameObject frog)
    {
        currentPlayer = frog;
    }

    /// <summary>
    /// Set location to the location of the player, but base the rotation on the rotation of the camera
    /// </summary>
    void FollowPlayer()
    {
        transform.position = currentPlayer.transform.position;
        transform.rotation = Camera.main.transform.rotation;
    }
}
