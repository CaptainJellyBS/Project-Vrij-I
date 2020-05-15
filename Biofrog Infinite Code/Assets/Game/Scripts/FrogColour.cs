using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogColour : MonoBehaviour
{
    public Color color;

    public GameObject footsteps;
    public Material frogMat;

    // Start is called before the first frame update
    void Start()
    {
        color = randomColor();
        frogMat = GetComponent<Renderer>().material;
        SetColor(color);
    }

    public void SetColor(Color c)
    {
        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material.SetColor("_EmissionColor", c);
            r.material.SetColor("_Color", c);
        }

        foreach (Light l in GetComponentsInChildren<Light>())
        {
            l.color = c;
        }

        foreach (Light l in footsteps.GetComponentsInChildren<Light>())
        {
            l.color = c;
        }

        color = c;
    }

    public Color randomColor()
    {
        float r = Random.Range(0.3f, 0.7f);
        float g = Random.Range(0.3f, 0.7f);
        float b = Random.Range(0.3f, 0.7f);

        Color c = new Color(r,g,b,1);
        return c;
    }
}
