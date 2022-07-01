using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObj : EventTrigger {
    [SerializeField] int colorIndex;

    public override void Init(int index) {
        if (index == colorIndex) {
            this.gameObject.SetActive(false);
        }
    }
}
