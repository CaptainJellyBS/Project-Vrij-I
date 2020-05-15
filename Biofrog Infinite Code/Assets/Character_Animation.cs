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
        if (Input.GetKey(KeyCode.Mouse0) && (PlayerMovement.isGrounded))
        {
            print("jomp");
            Kikker.ResetTrigger("SmallJump");
            Kikker.SetTrigger("SmallJump");
        }

      
    }
}
