using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animation : MonoBehaviour
{
    public Animator Kikker;
    //public CharacterController PlayerMovement;

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// play small jump animation when player hops
    /// </summary>
    public void SmallJumpAnimation()
    {
            Kikker.SetBool("isGrounded", false);
            Kikker.SetTrigger("smallJump");
    }
    /// <summary>
    /// play large  jump animation when player jumps
    /// </summary>
    public void LargeJumpAnimation()
    {
            Kikker.SetBool("isGrounded", false);
            Kikker.SetTrigger("largeJump");
    }

    /// <summary>
    /// transition back to idle animation when frog is on the ground
    /// </summary>
    public void Grounded()
    {
            Kikker.SetBool("isGrounded", true);
            Kikker.ResetTrigger("smallJump");
            Kikker.ResetTrigger("largeJump");
    }

    public bool InIdle()
    {
        return Kikker.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle1");
    }
}
