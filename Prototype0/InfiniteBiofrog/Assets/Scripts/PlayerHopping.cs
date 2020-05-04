using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHopping : MonoBehaviour
{

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits
    Rigidbody rb;
    bool grounded;
    [SerializeField]
    float jumpCharge = 2.5f;
    public Color color;

    public GameObject footstepObj;
    public GameObject splat;
    public GameObject newPlayer;

    float initialAngle = 45;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        SetColor();
    }

    public void SetColor()
    {
        foreach(Light l in GetComponentsInChildren<Light>())
        {
            l.color = color;
        }

        foreach (Light l in footstepObj.GetComponentsInChildren<Light>())
        {
            l.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the camera to the mouse cursor
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            // ...and if that object is the ground...
            if (cameraRayHit.transform.tag == "Floor" || cameraRayHit.transform.tag == "Object")
            {
                // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                transform.LookAt(targetPosition);
            }
        }

        if(Input.GetMouseButton(0) && grounded)
        {
            rb.AddForce((transform.forward + (Vector3.up*1.1f))*2.5f, ForceMode.Impulse);
            grounded = false;
            Instantiate(footstepObj, transform.position + (transform.rotation * new Vector3(0.25f, 0, 0)), transform.rotation);
            Instantiate(footstepObj, transform.position + (transform.rotation * new Vector3(-0.25f, 0, 0)), transform.rotation);
        }

        if (Input.GetMouseButton(1) && grounded)
        {
            jumpCharge = Mathf.Min(jumpCharge + 0.05f, 7.5f);
        }
        if (Input.GetMouseButtonUp(1) && grounded)
        {
            LongJump();
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            Die();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Floor")
        {
            grounded = true;
            rb.velocity = Vector3.zero;
            jumpCharge = 2.5f;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "Floor")
        { grounded = false; }
    }

    void Die()
    {
        GameObject t = Instantiate(splat, transform.position, Quaternion.identity);
        foreach (Light l in t.GetComponentsInChildren<Light>())
        {
            l.color = color;
        }

        GameObject np = Instantiate(newPlayer, new Vector3(0, 0.25f, 0), Quaternion.identity);
        Camera.main.GetComponent<CameraFollowScript>().player = np.GetComponent<PlayerHopping>();
        np.GetComponent<PlayerHopping>().color = randomColor();
        np.GetComponent<PlayerHopping>().SetColor();
        Destroy(this.gameObject);
    }

    Color randomColor()
    {
        int r = Random.Range(0, 5);
        Color[] c = { Color.red, Color.cyan, Color.green, Color.magenta, Color.yellow };
        return c[r];
    }

    void LongJump()
    {
        //    rb.AddForce((transform.forward + (Vector3.up * 1.5f)) * jumpCharge, ForceMode.Impulse);
        //    grounded = false;
        //    Instantiate(footstepObj, transform.position + (transform.rotation * new Vector3(0.25f, 0, 0)), transform.rotation);
        //    Instantiate(footstepObj, transform.position + (transform.rotation * new Vector3(-0.25f, 0, 0)), transform.rotation);
        Vector3 p;

        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            // ...and if that object is the ground...
            if (cameraRayHit.transform.tag == "Floor" || cameraRayHit.transform.tag == "Object")
            {
                // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                //Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                //transform.LookAt(targetPosition);
                p = cameraRayHit.point;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }

        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians

        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        //float distance = Vector3.Distance(transform.position, p);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - p.y;
        Debug.Log(yOffset);
        //float angle = initialAngle* Mathf.Deg2Rad;
        float angle = Mathf.Max(45,Mathf.Min(45 - yOffset*10, 80)) * Mathf.Deg2Rad;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.SignedAngle(Vector3.forward, planarTarget - planarPostion, Vector3.up);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        // Fire!
        //rb.velocity = finalVelocity;

        // Alternative way:
        rb.AddForce(finalVelocity * rb.mass, ForceMode.Impulse);
    }
}

//mouselook code stolen from: https://answers.unity.com/questions/805776/isometric-game-player-look-at-cursor.html