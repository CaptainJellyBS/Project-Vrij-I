using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowNew : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;

    //change camera height
    private float height;
    private Vector3 heightVector;

    //rotation
    public float cameraRotateSpeed = 2.5f;
    bool turning;

    private void Start()
    {
        height = player.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z) + offset;
        //transform.position = player.transform.position + offset;

        //if the player is grounded get its y position 
        if (player.GetComponent<CharacterController>().isGrounded) 
        {
            heightVector = new Vector3(transform.position.x, (player.transform.position.y - 1.08f), transform.position.z); // player y at groundlevel is 1.08
        }
    }

    void FixedUpdate()
    {
        HandleInput();
        //change the y offset to match player height
        offset.y = Vector3.Lerp(transform.position, heightVector, 0.02f).y + 0.8f; // y offset at groundlevel is 40, 0.8/0.02 = 40
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
