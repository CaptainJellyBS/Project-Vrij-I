using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowNew : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    bool turning;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z) + offset;

        HandleInput();
    }

    void HandleInput()
    {
        if (turning) { return; }
        if (Input.GetKeyDown(KeyCode.E))
        {   
            StartCoroutine(CameraRotation(90));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(CameraRotation(-90));
        }
    }

    IEnumerator CameraRotation(float target)
    {
        turning = true;
        float i = 0;
        float change = 5.0f;
        while(i<target)
        {
            i += change;
            transform.Rotate(Vector3.up, change, Space.World);
            offset = Quaternion.AngleAxis(change, Vector3.up) * offset;
            yield return null;
        }

        while (i > target)
        {
            i -= change;
            transform.Rotate(Vector3.up, -change, Space.World);
            offset = Quaternion.AngleAxis(-change, Vector3.up) * offset;
            yield return null;
        }
        turning = false;
    }
}
