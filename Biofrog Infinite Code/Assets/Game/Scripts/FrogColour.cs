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
        float r = Random.Range(0.3f, 0.7f);
        float g = Random.Range(0.3f, 0.7f);
        float b = Random.Range(0.3f, 0.7f);

        Color c = new Color(r,g,b,1);
        return c;
    }
}
