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
    Vector3 targetPosition;

    public AudioClip squeakClip, footstepClip;
    AudioSource audSource;


    //characterController movement
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    //leave footprints
    public GameObject footsteps;
    private RaycastHit hit;
    public Transform raycastPoint;
    RaycastHit hitCast;

    //Listen to sound
    public GameObject audioListener;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioListener = GameObject.Find("audioListener");
        audSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //if the character is on the ground
        if (characterController.isGrounded)
        {
            Turn();
            Move();
        }

        //make sure movement follows time, not framerate
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
            
            LeaveFootprint();

            GetComponentInChildren<Character_Animation>().SmallJumpAnimation();
        }
        //if right mouse button is pressed
        else if (Input.GetMouseButton(1))
            {
                //make big jump in the direction of the cursor 
                LongJump();
            }
                //if nothing is pressed
                else
                {
                    //stop moving
                    moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
                }

    }

    /// <summary>
    /// turn the frog towards the mouse
    /// </summary>
    private void Turn()
    {
        // Cast a ray from the camera to the mouse cursor
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            //if the object is a frog
            if (cameraRayHit.transform.tag == "Player")
            {
                return;
            }
            // else, if that object is the ground or anything you can jump on...
            else if (cameraRayHit.transform.tag == "Ground" || cameraRayHit.transform.tag == "Obstacle" || cameraRayHit.transform.tag == "Finish" || cameraRayHit.transform.tag == "Hazard")
            {
                // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }
    }

    void LongJump()
    {
        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(targetPosition.x, 0, targetPosition.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);

        // if the distance to be jumped isn't too small
        if (distance > 4.5)
        {
            Debug.Log(distance);

            // Distance along the y axis between objects
            float yOffset = transform.position.y - targetPosition.y;

            Debug.Log(yOffset);

            //fancy math magic
            float angle = Mathf.Max(45, Mathf.Min(45 - yOffset * 10, 80)) * Mathf.Deg2Rad;

            float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

            Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

            // Rotate our velocity to match the direction between the two objects
            float angleBetweenObjects = Vector3.SignedAngle(Vector3.forward, planarTarget - planarPostion, Vector3.up);
            moveDirection = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

            LeaveFootprint();

            GetComponentInChildren<Character_Animation>().LargeJumpAnimation();
        }
        else
        {
            //stop moving
            moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    /// <summary>
    /// leave a footprint
    /// </summary>
    public void LeaveFootprint()
    {
        Color c = GetComponent<FrogColour>().color;



        GameObject footPrint = Instantiate(footsteps, transform.position + (transform.rotation * new Vector3(0, -0.4f, 0)), transform.rotation);

        // Rotate to align with terrain (stolen from https://medium.com/thefloatingpoint/ground-hugging-vehicles-in-unity-3d-50115f421005)
        raycastPoint = footPrint.transform;
        Physics.Raycast(raycastPoint.position, Vector3.down, out hitCast);

        footPrint.transform.up -= (transform.up - hitCast.normal);
        footPrint.transform.rotation = transform.rotation;


        footPrint.GetComponentInChildren<Renderer>().material.SetColor("_EmissionColor", c);
        footPrint.GetComponentInChildren<Renderer>().material.SetColor("_Color", c);

        foreach (Light l in footPrint.GetComponentsInChildren<Light>())
        {
            l.color = c;
        }

        footPrint.SetActive(true);
        FootstepSound();
    }

    /// <summary>
    /// play footstep sound
    /// </summary>
    public void FootstepSound()
    {
        audSource.volume = 0.1f;
        audSource.clip = footstepClip;
        audSource.Play();
    }

    /// <summary>
    /// play squeak
    /// </summary>
    public void Squeak()
    {
        audSource.volume = 1.0f;
        audSource.clip = squeakClip;
        audSource.Play();
    }

}
