using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject player;
    public Transform startPos;

    void Start()
    {
        player = GameObject.Find("tempPlayer");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("poison entered");
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.position = startPos.transform.position;
        }
    }
}
