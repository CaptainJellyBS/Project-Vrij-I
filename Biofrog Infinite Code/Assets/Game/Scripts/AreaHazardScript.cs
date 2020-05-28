using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHazardScript : Hazard
{
    private GameObject[] herons;
    public GameObject player;
    public float timeOfDemise;

    public Material[] heronMaterials;

    // Start is called before the first frame update
    void Start()
    {
        herons = GameObject.FindGameObjectsWithTag("Heron");
        heronMaterials = herons[0].GetComponent<Renderer>().materials;
        heronMaterials[1].DisableKeyword("_EMISSION");

        foreach (GameObject heron in herons)
        {
            
            foreach (Light l in heron.GetComponentsInChildren<Light>(true))
            {
                l.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
          if (timeOfDemise != 0 && timeOfDemise < Time.time)
        {
            player.GetComponent<Death>().Die();
            timeOfDemise = 0;
            heronMaterials[1].DisableKeyword("_EMISSION");
            foreach (GameObject heron in herons)
            {
                foreach (Light l in heron.GetComponentsInChildren<Light>(true))
                {
                    l.enabled = false;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        { 
            heronMaterials[1].EnableKeyword("_EMISSION");
            foreach (GameObject heron in herons)
            {
                foreach (Light l in heron.GetComponentsInChildren<Light>(true))
                {
                    l.enabled = true;
                }
                timeOfDemise = Time.time + 3.0f;
            }
         }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            heronMaterials[1].DisableKeyword("_EMISSION");
            foreach (GameObject heron in herons)
            {
                foreach (Light l in heron.GetComponentsInChildren<Light>(true))
                {
                    l.enabled = false;
                }
            }
            timeOfDemise = 0;
        }
    }

}
