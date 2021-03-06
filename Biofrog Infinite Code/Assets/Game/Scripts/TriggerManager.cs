﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyInput();
    }

    void HandleKeyInput()
    {
        //quit when esc is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        //make squek sound when space is pressed
        if (Input.GetKeyDown("space"))
        {
            this.GetComponent<Movement>().Squeak();
        }

        //kill active frog
        if (Input.GetKeyDown("end"))
        {
            this.GetComponent<Death>().Die();
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            this.gameObject.GetComponent<Death>().Die();
        }

        if (other.gameObject.CompareTag("TextTrigger"))
        {
            other.gameObject.GetComponent<TriggerText>().ShowText(5.0f);
        }

        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.GetComponent<FireFly>().Collect(this.gameObject);
        }
        if (other.gameObject.CompareTag("AreaHazard"))
        {
            Debug.Log(this.gameObject);
            other.gameObject.GetComponent<AreaHazardScript>().ActivateHeron(this.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AreaHazard"))
        {
            other.gameObject.GetComponent<AreaHazardScript>().DeactivateHeron(this.gameObject);
        }
    }
}