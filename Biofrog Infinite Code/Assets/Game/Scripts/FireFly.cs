using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFly : MonoBehaviour
{
    public bool isOnPlayer;
    GameObject player;
    [SerializeField]
    Vector3 offset;
    void Update()
    {
        if(isOnPlayer)
        {
            transform.position = player.transform.position + offset;
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
        GetComponent<BoxCollider>().enabled = false;

        Debug.Log("picked up");

    }

    /// <summary>
    /// Unused (yet)
    /// </summary>
    public void PlayerDeath()
    {
        Destroy(this.gameObject);
    }
}
