using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject splat;
    public GameObject whichFrog;
    public bool hasDied;

    [SerializeField][Range(0,10.0f)]
    float deathCameraDelay = 2.0f;

    private void Start()
    {
        whichFrog = GameObject.Find("whichFrog");
        hasDied = false;
    }

    /// <summary>
    /// Does all the stuff that should be done upon death of a frog
    /// </summary>
    public void Die()
    {
        if (hasDied)
        {
            return;
        }

        hasDied = true;

        whichFrog.GetComponent<MultipleFrogs>().MakeCurrentFrogInactive();
        Invoke("NextFrog", deathCameraDelay);
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

    void NextFrog()
    {
        whichFrog.GetComponent<MultipleFrogs>().NextFrog();
    }
}
