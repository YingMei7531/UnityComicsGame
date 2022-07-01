using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBox : MonoBehaviour {
    public Animator animator;
    public bool isGround = false;
    int planes = 0;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Plane") {
            if(planes<=0) SetIsGround(true);
            planes++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Plane") {
            planes--;
            if(planes<=0) SetIsGround(false);
        }
    }

    public void SetIsGround(bool isGround) {
        this.isGround = isGround;
        animator.SetBool("isGround", isGround);
    }
}
