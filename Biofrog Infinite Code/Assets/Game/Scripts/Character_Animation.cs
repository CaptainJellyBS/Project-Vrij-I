using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animation : MonoBehaviour
{
    public Animator Kikker;
    public CharacterController PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //play jump animation when player jumps
        if ((Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) && (PlayerMovement.isGrounded))
        {
            Debug.Log("jomp");
            Kikker.ResetTrigger("SmallJump");
            Kikker.SetTrigger("SmallJump");
        }      
    }
}
