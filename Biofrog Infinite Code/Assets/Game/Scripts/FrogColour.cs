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
        frogMat = GetComponent<Renderer>().sharedMaterial;
        SetColor(color);
    }

    public void SetColor(Color c)
    {
        frogMat.SetColor("_EmissionColor", c);
        foreach (Light l in GetComponentsInChildren<Light>())
        {
            l.color = c;
        }

        foreach (Light l in footsteps.GetComponentsInChildren<Light>())
        {
            l.color = c;
        }
    }

    public Color randomColor()
    {
        float r = Random.Range(0.3f, 0.8f);
        float g = Random.Range(0.3f, 0.8f);
        float b = Random.Range(0.3f, 0.8f);

        Color c = new Color(r,g,b,1);
        return c;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
