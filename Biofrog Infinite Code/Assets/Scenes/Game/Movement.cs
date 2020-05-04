﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Class partly taken from Unity characterController scripting API
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            Move();
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Move()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;

        if ((Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") != 0))
        {
            moveDirection.y = jumpSpeed;
        }

        if (Input.GetButton("Jump"))
        {
            moveDirection.y = 2 * jumpSpeed;
        }
    }
}