using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {
    private void Update() {
        if (Check()) Trigger();
    }

    public virtual void Init(int index) { 
        
    }

    public virtual void Trigger() {

    }

    public virtual void IntoCell(Vector3 birthPos) {
        transform.position = birthPos;
    }


    public virtual bool Check() {
        return false;
    }
}
