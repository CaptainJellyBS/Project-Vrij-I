using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Light lightning, lightningFade;
    public float fadeTime;
    public AudioSource lightningStrike, thunderRumble;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightningFlashing());   
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L)) { StartCoroutine(LightningFlashingTest()); } //DEBUG
    }

    /// <summary>
    /// Every 15-25 of seconds, trigger a lightning strike
    /// </summary>
    /// <returns></returns>
    IEnumerator LightningFlashing()
    {       

        while(true)
        {
            float timey = Random.Range(15.0f, 25.0f);
            float delay = Random.Range(2.0f, 5.0f);
            //Flicker lightning every few seconds
            yield return new WaitForSeconds(delay);
            thunderRumble.Play();
            yield return new WaitForSeconds(timey - delay);


            lightningFade.intensity = 1.5f;

            //Flicker lightning every few seconds
            for (int i = Random.Range(1, 4); i > 0; i--)
            {
                lightning.intensity = 3;
                lightningStrike.Play();
                yield return new WaitForSeconds(0.1f);
                lightning.intensity = 0;
                yield return new WaitForSeconds(0.1f);
            }
            //Fade out lightning negative thingy

            while (lightningFade.intensity > 0.0f)
            {
                lightningFade.intensity -= (2.0f / fadeTime) * Time.deltaTime;
                yield return null;
            }
            lightningFade.intensity = 0.0f;

        }

    }

    /// <summary>
    /// Debug test Coroutine so you can manually trigger Lightning
    /// </summary>
    /// <returns></returns>
    IEnumerator LightningFlashingTest()
    {
        float timey = Random.Range(15.0f, 25.0f);
        float delay = Random.Range(2.0f, 5.0f);
        //Flicker lightning every few seconds

        //yield return new WaitForSeconds(timey - delay);


        lightningFade.intensity = 1.5f;

        //Flicker lightning every few seconds
        for (int i = Random.Range(1, 4); i > 0; i--)
        {
            lightning.intensity = 3;
            lightningStrike.Play();
            yield return new WaitForSeconds(0.1f);
            lightning.intensity = 0;
            yield return new WaitForSeconds(0.1f);
        }
        //Fade out lightning negative thingy

        while (lightningFade.intensity > 0.0f)
        {
            lightningFade.intensity -= (2.0f / fadeTime) * Time.deltaTime;
            yield return null;
        }
        lightningFade.intensity = 0.0f;
        yield return new WaitForSeconds(delay);
        thunderRumble.Play();

    }
}
