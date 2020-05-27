using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowNew : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z) + offset;
    }
}
