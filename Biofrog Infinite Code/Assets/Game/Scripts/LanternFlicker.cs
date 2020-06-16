using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternFlicker : MonoBehaviour
{
    public Light lanternLight;
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LanternFlashing());
    }

    void Update()
    {
    }

    /// <summary>
    /// Every 15-25 of seconds, trigger a lightning strike
    /// </summary>
    /// <returns></returns>
    IEnumerator LanternFlashing()
    {

        while (true)
        {
            float delay = Random.Range(0.1f, 1.5f);
            yield return new WaitForSeconds(delay);

            //Flicker lightning every few seconds
            for (int i = Random.Range(1, 4); i > 0; i--)
            {
                lanternLight.intensity = 1.2f;
                yield return new WaitForSeconds(Random.Range(0.01f,1.3f));
                lanternLight.intensity = Random.Range(0.2f, 0.7f);
                yield return new WaitForSeconds(Random.Range(0.01f, 0.3f));
                lanternLight.intensity = 1.2f;
                yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));
                lanternLight.intensity = Random.Range(0.3f, 0.8f);
                yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));
            }
            //Fade out lightning negative thingy

            while (lanternLight.intensity < 1.0f)
            {
                lanternLight.intensity += (2.0f / fadeTime) * Time.deltaTime;
                yield return null;
            }
            lanternLight.intensity = 1.2f;

        }

    }
}
