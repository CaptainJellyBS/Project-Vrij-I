﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingHazard : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform currentPoint;
    public int ind;

    [SerializeField]
    [Range(0, 5)]
    float DistanceToPoint;

    [SerializeField]
    [Range(0, 500)]
    float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ind = 0;
        if (patrolPoints.Length > 0)
        {
            currentPoint = patrolPoints[ind];
            transform.LookAt(currentPoint.position);

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

        ind++;
        ind %= patrolPoints.Length;
        currentPoint = patrolPoints[ind];

        transform.LookAt(currentPoint.position);
    }
}
