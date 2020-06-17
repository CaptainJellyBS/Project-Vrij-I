using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatSoundPerSecond : MonoBehaviour
{
    AudioSource audio;
    public float secondsBetweenAudio = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundPerSecond());
    }

    IEnumerator PlaySoundPerSecond()
    {
        while(true)
        {
            audio.Play();
            yield return new WaitForSeconds(secondsBetweenAudio);
        }
    }
}
