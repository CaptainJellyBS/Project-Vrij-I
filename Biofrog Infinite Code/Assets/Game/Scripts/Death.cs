using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Transform startPos;
    public GameObject splat;
    public GameObject player;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("poison entered");
        if (other.gameObject.CompareTag("Hazard"))
        {
            GameObject splatt = Instantiate(splat, transform.position + (transform.rotation * new Vector3(0, -0.8f, 0)), transform.rotation);
            splatt.SetActive(true);
            foreach (Light l in splat.GetComponentsInChildren<Light>())
            {
                Color color = player.GetComponent<Light>().color;
                l.color = color;
            }

            Color c = player.GetComponent<FrogColour>().randomColor();
            player.GetComponent<FrogColour>().SetColor(c);
            transform.position = startPos.transform.position;
        }
    }
}
