using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Light lightning, lightningFade;
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LightningFlashing());   
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.L)) { StartCoroutine(LightningFlashingTest()); } //DEBUG
    }

    IEnumerator LightningFlashing()
    {       

        while(true)
        {
            //Flicker lightning every few seconds
            yield return new WaitForSeconds(Random.Range(15.0f, 50.0f));

            lightningFade.intensity = 1.5f;

            //Flicker lightning every few seconds
            for (int i = Random.Range(2, 3); i > 0; i--)
            {
                lightning.intensity = 3;
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

    IEnumerator LightningFlashingTest()
    {
        lightningFade.intensity = 1.5f;

        //Flicker lightning every few seconds
        for (int i = Random.Range(2,3); i > 0; i--)
        {
            lightning.intensity = 3;
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
