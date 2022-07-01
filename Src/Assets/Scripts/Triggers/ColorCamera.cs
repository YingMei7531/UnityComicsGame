using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCamera : EventTrigger {
    public override void Init(int index) {
        EdgeDetection edgeDetection = GetComponent<EdgeDetection>();
        if (index == 0) {
            edgeDetection.enabled = false;
        }
        else if (index == 1) {
            edgeDetection.backgroundColor = Color.red;
            edgeDetection.enabled = true;
        }
        else if (index == 2) {
            edgeDetection.backgroundColor = Color.blue;
            edgeDetection.enabled = true;
        }
        else if (index == 3) {
            edgeDetection.backgroundColor = Color.green;
            edgeDetection.enabled = true;
        }
    }
}
