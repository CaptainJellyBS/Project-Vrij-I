using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingHazard : Hazard
{

    public Transform[] patrolPoints;
    public Transform currentPoint;
    public int nextPoint;

    [SerializeField]
    [Range(0, 5)]
    float DistanceToPoint;

    [SerializeField]
    [Range(0, 2)]
    float turnSpeed;

    [SerializeField]
    [Range(0, 500)]
    float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        nextPoint = 0;
        if (patrolPoints.Length > 0)
        {
            currentPoint = patrolPoints[nextPoint];
            //transform.LookAt(currentPoint.position);
            StartCoroutine(SlowTurn(currentPoint.position));

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(patrolPoints.Length>0)
        {
            Movement();
            HasArrived();
        }
    }

    /// <summary>
    /// Ugly movement method that just moves the thing directly to the next patrol point. Should probably use navmesh or some fixed route but designer life ;_;
    /// </summary>
    void Movement()
    { 
        //possibly useful codesnippet to maybe use later
        //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * speed);

        transform.position += (currentPoint.position - transform.position).normalized * MoveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// If arrived at the patrol point, go to the next one
    /// </summary>
    void HasArrived()
    {
        if(!(Vector3.Distance(transform.position, currentPoint.position) < DistanceToPoint))
        {
            return;
        }

        nextPoint++;
        nextPoint %= patrolPoints.Length;
        currentPoint = patrolPoints[nextPoint];

        StartCoroutine(SlowTurn(currentPoint.position));
        //transform.LookAt(currentPoint.position); 
    }

    IEnumerator SlowTurn(Vector3 direction)
    {
        float oldSpeed = MoveSpeed;
        Quaternion rot = transform.rotation;
        MoveSpeed = 0;

        float t = 0;
        while (t < 1.0f)
        {
            t += Time.deltaTime * turnSpeed;

            transform.rotation = Quaternion.Lerp(rot, Quaternion.LookRotation(direction - transform.position, Vector3.up), t);
            yield return null;
        }

        MoveSpeed = oldSpeed;
    }
}
