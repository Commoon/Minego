using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeadPlayerDisappear : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}
