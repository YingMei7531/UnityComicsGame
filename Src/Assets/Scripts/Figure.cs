using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : StateMachineBehaviour {
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetInteger("State", Random.Range(0, 4));
    }
}
