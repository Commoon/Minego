using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLayingEgg : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<PlayerController>().CompleteLayingEgg();
    }
}
