using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHazardScript : Hazard
{
    private GameObject[] herons;
    public GameObject player;
    public float timeOfDemise;
    public float timeOfBeak;
    public float timeOfAnimationEnd;
    public GameObject heronBeak;

    private Material[] heronMaterials;

    // Start is called before the first frame update
    void Start()
    {
        herons = GameObject.FindGameObjectsWithTag("Heron");

        foreach (MeshRenderer beak in heronBeak.GetComponentsInChildren<MeshRenderer>())
        {
            beak.enabled = false;
        }

        foreach (GameObject heron in herons)
        {
            heronMaterials = heron.GetComponent<Renderer>().materials;
            heronMaterials[1].DisableKeyword("_EMISSION");

            foreach (Light l in heron.GetComponentsInChildren<Light>(true))
            {
                l.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((timeOfAnimationEnd < Time.time && timeOfAnimationEnd != 0))
        {
            heronBeak.GetComponent<HeronAnimation>().ResetHeronBeak();
            timeOfAnimationEnd = 0;
        }

        if (timeOfDemise != 0)
        {
            Vector3 beakTarget = new Vector3(player.transform.position.x, heronBeak.transform.position.y, player.transform.position.z);
            heronBeak.transform.position = beakTarget;
        }

        if ((timeOfBeak < Time.time && timeOfBeak != 0))
        {
            heronBeak.GetComponent<HeronAnimation>().PlayHeronAnimation();
            timeOfBeak = 0;
        }

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

    public void ActivateHeron(GameObject other)
    {
        if (other == player)
        {
            heronMaterials[1].EnableKeyword("_EMISSION");
            foreach (GameObject heron in herons)
            {
                foreach (Light l in heron.GetComponentsInChildren<Light>(true))
                {
                    l.enabled = true;
                }
            }
            timeOfDemise = Time.time + 2.0f;
            timeOfBeak = Time.time + 0.5f;
            timeOfAnimationEnd = timeOfBeak + 2.5f;

            Vector3 beakTarget = new Vector3(player.transform.position.x, heronBeak.transform.position.y, player.transform.position.z);
            heronBeak.transform.position = beakTarget;

            foreach (MeshRenderer beak in heronBeak.GetComponentsInChildren<MeshRenderer>())
            {
                beak.enabled = true;
            }
        }
    }

    public void DeactivateHeron(GameObject other)
    {
        if (other == player)
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
