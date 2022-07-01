using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerTrigger : EventTrigger {
    [SerializeField] GameObject[] rejectors;
    [SerializeField] Roller roller;
    Vector3 rollerPos;
    bool isTrigger;

    private void Awake() {
        rollerPos = roller.gameObject.transform.position;
    }

    public override void Init(int index) {
        isTrigger = false;
        foreach (GameObject rejector in rejectors) {
            rejector.SetActive(true);
        }
        roller.gameObject.transform.position = rollerPos;
        roller.gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other) {
        if (isTrigger) return;
        if (other.tag == "Player") {
            roller.gameObject.SetActive(true);
            isTrigger = true;
        }
    }
}
