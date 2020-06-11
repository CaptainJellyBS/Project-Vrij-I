using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour
{
    public bool isOnPlayer;
    GameObject player;
    [SerializeField]
    Vector3 offset;
    Vector3 startPos;
    float direction = 0.5f;

    void Start()
    {
        startPos = transform.position;
    }


    void Update()
    {
        if(isOnPlayer)
        {
            transform.position = player.transform.position + offset;
        }
        else
        {
            transform.position += Vector3.up * direction * Time.deltaTime;
            if(Vector3.Distance(transform.position, startPos) > 0.3f) { direction *= -1; }
        }
        //This should crash on death but doesn't. Needs to be fixed anyway but s t r e s s
    }

    /// <summary>
    /// Collect the firefly
    /// </summary>
    /// <param name="p"></param>
    public void Collect(GameObject p)
    {
        isOnPlayer = true;
        player = p;
        GetComponent<SphereCollider>().enabled = false;

        Debug.Log("picked up");

    }

    /// <summary>
    /// When the player dies, go back to earlier state
    /// </summary>
    public void PlayerDeath()
    {
        Debug.Log("oof ouch owie");
        isOnPlayer = false;
        GetComponent<SphereCollider>().enabled = true;
        player = null;
    }
}
