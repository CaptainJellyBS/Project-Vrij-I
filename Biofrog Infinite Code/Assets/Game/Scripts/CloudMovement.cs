using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 0.0f;
    public Vector3 direction = Vector3.zero;
    float maxDistance = float.PositiveInfinity;
    float distanceFlown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float dist = speed * Time.deltaTime;
        transform.position += direction * dist;
        distanceFlown += dist;

        if (distanceFlown >= maxDistance) { Destroy(gameObject); }
    }

    public void SetValues(float spd, Vector3 dir, float maxDis)
    {
        speed = spd; direction = dir; maxDistance = maxDis;
    }
}
