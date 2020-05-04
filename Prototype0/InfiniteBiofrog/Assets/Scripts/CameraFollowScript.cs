using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public PlayerHopping player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(new Vector3(player.transform.position.x, 0, player.transform.position.z), new Vector3(transform.position.x+4, 0, transform.position.z+4)) > 2.0)
        {
            transform.position += (new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x+4, 0, transform.position.z+4)) * Time.deltaTime;
        }
    }
}
