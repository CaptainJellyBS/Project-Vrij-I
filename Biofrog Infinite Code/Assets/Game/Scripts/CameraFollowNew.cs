using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowNew : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    public float cameraRotateSpeed = 2.5f;
    bool turning;

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z) + offset;

    }

    void FixedUpdate()
    {
        HandleInput();

    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, cameraRotateSpeed * Time.deltaTime, Space.World);
            offset = Quaternion.AngleAxis(cameraRotateSpeed * Time.deltaTime, Vector3.up) * offset;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -cameraRotateSpeed * Time.deltaTime, Space.World);
            offset = Quaternion.AngleAxis(-cameraRotateSpeed * Time.deltaTime, Vector3.up) * offset;
        }
    }

    //IEnumerator CameraRotation(float target)
    //{
    //    turning = true;
    //    float i = 0;
    //    float change = 2.5f;
    //    while(i<target)
    //    {
    //        i += change;
    //        transform.Rotate(Vector3.up, change, Space.World);
    //        offset = Quaternion.AngleAxis(change, Vector3.up) * offset;
    //        yield return null;
    //    }

    //    while (i > target)
    //    {
    //        i -= change;
    //        transform.Rotate(Vector3.up, -change, Space.World);
    //        offset = Quaternion.AngleAxis(-change, Vector3.up) * offset;
    //        yield return null;
    //    }
    //    turning = false;
    //}
}
