using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeronAnimation : MonoBehaviour
{
    public Animator heronAnimator;
    private bool playingAnimation;

    public void PlayHeronAnimation()
    {
        //Debug.Log("play animation");
        heronAnimator.SetTrigger("biteTrigger");
        playingAnimation = true;
        //Debug.Log(heronAnimator.GetCurrentAnimatorStateInfo(0).tagHash);
    }


    public void ResetHeronBeak()
    {
            //Debug.Log("disable mesh");
            foreach (MeshRenderer beak in GetComponentsInChildren<MeshRenderer>())
            {
                beak.enabled = false;
            }
            heronAnimator.ResetTrigger("biteTrigger");
            playingAnimation = false;
    }
}
