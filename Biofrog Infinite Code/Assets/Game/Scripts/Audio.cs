using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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

}
