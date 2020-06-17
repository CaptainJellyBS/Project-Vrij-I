using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float cloudTimeMin, cloudTimeMax, cloudSpeedMin, cloudSpeedMax;
    public float maxVarianceX, maxVarianceY, maxVarianceZ;
    public Vector3 direction;
    public float maxDistance;
    public GameObject cloud;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCloudCycle());   
    }

    IEnumerator SpawnCloudCycle()
    {
        while(true)
        {
            SpawnCloud();
            yield return new WaitForSeconds(Random.Range(cloudTimeMin, cloudTimeMax));
        }
    }

    void SpawnCloud()
    {
        Vector3 position = transform.position + new Vector3(Random.Range(-maxVarianceX, maxVarianceX), Random.Range(-maxVarianceY, maxVarianceY), Random.Range(-maxVarianceZ, maxVarianceZ));
        GameObject newCloud = Instantiate(cloud, position, Quaternion.identity, transform);
        newCloud.GetComponent<CloudMovement>().SetValues(Random.Range(cloudSpeedMin, cloudSpeedMax), direction, maxDistance);
    }
}
