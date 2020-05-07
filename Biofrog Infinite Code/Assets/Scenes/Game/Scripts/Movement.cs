using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Class partly taken from Unity characterController scripting API and https://answers.unity.com/questions/805776/isometric-game-player-look-at-cursor.html
    CharacterController characterController;

    //turn towards mouse position
    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits

    //characterController movement
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    //leave footprints
    public GameObject footsteps;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //if the character is on the ground
        if (characterController.isGrounded)
        {
            Turn();
            Move();
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Move()
    {
        //if left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            //jump in the direction of the cursor
            moveDirection = transform.TransformDirection(Vector3.forward * speed);
            moveDirection.y = jumpSpeed;
            //leave footprint
            Instantiate(footsteps, transform.position + (transform.rotation * new Vector3(0, -0.99f, 0)), transform.rotation);
        }
            //if right mouse button is pressed
            else if (Input.GetMouseButton(1))
            {
                //make big jump in the direction of the cursor 
                moveDirection = transform.TransformDirection(Vector3.forward * 2*speed);
                moveDirection.y = 2*jumpSpeed;
                //leave footprint
                Instantiate(footsteps, transform.position + (transform.rotation * new Vector3(0, -0.99f, 0)), transform.rotation);
            }
                //if nothing is pressed
                else
                {
                    //stop moving
                    moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
                }
    }

    private void Turn()
    {
        // Cast a ray from the camera to the mouse cursor
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            // ...and if that object is the ground...
            if (cameraRayHit.transform.tag == "Ground" || cameraRayHit.transform.tag == "Obstacle" || cameraRayHit.transform.tag == "Finish" || cameraRayHit.transform.tag == "Hazard")
            {
                // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
    }
}
