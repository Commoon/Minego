using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRespawning : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.GetComponentInParent<Player>().Respawn();
    }
}
