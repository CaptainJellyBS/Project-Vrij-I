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
    /// play jump animation when player jumps
    /// </summary>
    public void JumpAnimation()
    {
            Debug.Log("jomp");
            Kikker.ResetTrigger("SmallJump");
            Kikker.SetTrigger("SmallJump");
    }
}
