using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light light;
    public float maxIntensity;
    public float minIntensity;
    
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while(true)
        {
            light.intensity = minIntensity;
            yield return new WaitForSeconds(Random.Range(1.0f, 5.0f));
            light.intensity = maxIntensity;

            for (int r = Random.Range(2, 5); r >= 0; r--)
            {
                yield return new WaitForSeconds(Random.Range(0.03f, 0.15f));
                light.intensity = minIntensity;
                yield return new WaitForSeconds(Random.Range(0.03f, 0.15f));
                light.intensity = maxIntensity;
            }
        }
    }
}
