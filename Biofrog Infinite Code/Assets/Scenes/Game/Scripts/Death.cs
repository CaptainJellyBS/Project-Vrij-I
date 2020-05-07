using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Transform startPos;
    public GameObject splat;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("poison entered");
        if (other.gameObject.CompareTag("Hazard"))
        {
            Instantiate(splat, transform.position + (transform.rotation * new Vector3(0, -0.99f, 0)), transform.rotation);
            transform.position = startPos.transform.position;
        }
    }
}
