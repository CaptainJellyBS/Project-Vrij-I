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
    private Vector3 raycastPoint;
    public RaycastHit hitCastFeetsy;

    //animate jump
    private Vector3 raycastPointLanding;
    private RaycastHit hitLandingGround;

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
            if (!GetComponentInChildren<Character_Animation>().InIdle())
            {
                GetComponentInChildren<Character_Animation>().Grounded();
            }
        }
        
        //make sure movement follows time, not framerate
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //check distance to ground when frog is falling
        if (characterController.velocity.y < 0)
        {
            ReadyLanding();
        }
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
            //Debug.Log(distance);

            // Distance along the y axis between objects
            float yOffset = transform.position.y - targetPosition.y;

            //Debug.Log(yOffset);

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
        
        /*
        //raycast to get normal vector of terrain (partly stolen from https://medium.com/thefloatingpoint/ground-hugging-vehicles-in-unity-3d-50115f421005)
        raycastPoint = transform;
        Physics.Raycast(raycastPoint.position, Vector3.down, out hitCastFeetsy);
        */

        GameObject footPrint = Instantiate(footsteps, transform.position + (transform.rotation * new Vector3(0, -0.7f, 0)), transform.rotation);

        /*
        // Rotate to align with terrain 
        float normalAngle = Vector3.Angle(footPrint.transform.up, hitCastFeetsy.normal);
        Debug.Log(normalAngle);
        //footPrint.transform.rotation = Quaternion.AngleAxis(normalAngle, Vector3.left);
        //footPrint.transform.rotation = transform.rotation;

        //footPrint.transform.rotation = Quaternion.Euler(footPrint.transform.rotation.x, footPrint.transform.rotation.y, normalAngle);
        */

        foreach (Transform feetsy in footPrint.GetComponentsInChildren<Transform>())
        {
            if (feetsy.GetComponent<Light>() == null)
            { 
            raycastPoint = feetsy.position + Vector3.up;
            Physics.Raycast(raycastPoint, Vector3.down, out hitCastFeetsy);
            feetsy.position = hitCastFeetsy.point;
            }

        }


        foreach (Renderer feetsyRend in footPrint.GetComponentsInChildren<Renderer>())
        {
            feetsyRend.material.SetColor("_EmissionColor", c);
            feetsyRend.material.SetColor("_Color", c);
        }

        foreach (Light l in footPrint.GetComponentsInChildren<Light>())
        {
            l.color = c;
        }

        footPrint.SetActive(true);
        FootstepSound();
    }

    /// <summary>
    /// Get ready for landing when the frog is close to the ground
    /// </summary>
    public void ReadyLanding()
    {
        float distance = 1;

        raycastPointLanding = transform.position;
        Physics.Raycast(raycastPointLanding, characterController.velocity, out hitLandingGround);

        //Debug.Log(characterController.velocity);

        //distance between frog and ground 
        distance = Vector3.Distance(transform.position, hitLandingGround.point);

        //Debug.Log(distance);

        //return to idle if the frog gets close enough to the ground
        if (distance < 5f)
        {
            GetComponentInChildren<Character_Animation>().Grounded();
        }
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
