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

    /// <summary>
    /// play small jump animation when player hops
    /// </summary>
    public void SmallJumpAnimation()
    {
            Debug.Log("jomp");
            Kikker.ResetTrigger("SmallJump");
            Kikker.SetTrigger("SmallJump");
    }
    /// <summary>
    /// play large  jump animation when player jumps
    /// </summary>
    public void LargeJumpAnimation()
    {
            Debug.Log("jomp");
            Kikker.ResetTrigger("LargeJump");
            Kikker.SetTrigger("LargeJump");
    }
}
