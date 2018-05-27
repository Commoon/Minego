using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEggExplosion : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<Egg>().CompleteExplosion();
    }
}
