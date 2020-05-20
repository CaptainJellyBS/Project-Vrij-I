using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject splat;
    public GameObject whichFrog;
    public bool hasDied;

    private void Start()
    {
        whichFrog = GameObject.Find("whichFrog");
        hasDied = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (hasDied)
        {
            return;
        }

        Debug.Log("poison entered");
        if (other.gameObject.CompareTag("Hazard"))
        {
            hasDied = true;

            whichFrog.GetComponent<MultipleFrogs>().NextFrog();
            Debug.Log("called NextFrog");

            GameObject splatt = Instantiate(splat, transform.position + (transform.rotation * new Vector3(0, -0.8f, 0)), transform.rotation);

            Color c = GetComponent<FrogColour>().color;

            splatt.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", c);
            splatt.GetComponentInChildren<Renderer>().material.SetColor("_Color", c);
            foreach (Light l in splatt.GetComponentsInChildren<Light>())
            {
                l.color = c;
            }

            splatt.SetActive(true);
            foreach (Light l in splat.GetComponentsInChildren<Light>())
            {
                Color color = GetComponent<Light>().color;
                l.color = color;
            }
        }
    }
}
