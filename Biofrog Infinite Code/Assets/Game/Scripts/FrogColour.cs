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
        frogMat = GetComponent<Renderer>().material;
    }

    /// <summary>
    /// Make the color, emission and the point lights in the children (footsteps & splat) the color that is given through the parameter
    /// </summary>
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

        color = c;
    }

    /// <summary>
    /// generate a random (opague) color
    /// </summary>
    /// <returns></returns>
    public Color randomColor()
    {
        float r;
        float g;
        float b;

        switch (Random.Range(0,5))
        {
            case 0: r = Random.Range(0.5f, 0.85f); g = Random.Range(0.1f, 0.85f); b = Random.Range(0.1f, 0.2f); break; //Red-Orange leaning frog
            case 1: r = Random.Range(0.1f, 0.2f); g = Random.Range(0.2f, 0.85f); b = Random.Range(0.5f, 0.85f); break; //Blue-Turquoise leaning frog
            case 2: r = Random.Range(0.1f, 0.85f); g = Random.Range(0.5f, 0.85f); b = Random.Range(0.1f, 0.2f); break; //Green-Yellow leaning frog
            case 3: r = Random.Range(0.5f, 0.85f); g = Random.Range(0.1f, 0.2f); b = Random.Range(0.1f, 0.85f); break; //Red-Purple leaning frog
            case 4: r = Random.Range(0.2f, 0.85f); g = Random.Range(0.1f, 0.2f); b = Random.Range(0.5f, 0.85f); break; //Blue-Purple leaning frog
            case 5: r = Random.Range(0.1f, 0.2f); g = Random.Range(0.5f, 0.85f); b = Random.Range(0.1f, 0.85f); break; //Green-blue leaning frog
            default: r = Random.Range(0.5f, 0.85f); g = Random.Range(0.1f, 0.85f); b = Random.Range(0.1f, 0.2f); break; //Should never happen
        }


        Color c = new Color(r,g,b,1);
        return c;
    }
}
