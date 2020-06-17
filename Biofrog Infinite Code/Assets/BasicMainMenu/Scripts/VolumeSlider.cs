using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Text volText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Update the volume with a float that goes from 0 to 1.0f
    /// </summary>
    /// <param name="volume"></param>
    public void UpdateVolume(float volume)
    {
        AudioListener.volume = volume;
        volText.text = ((int)(volume * 100)).ToString();
    }

    /// <summary>
    /// Update the volume with an int that goes from 0 to 100
    /// </summary>
    /// <param name="volume"></param>
    public void UpdateVolume(int volume)
    {
        UpdateVolume(((float)volume) / 100);
    }
}
