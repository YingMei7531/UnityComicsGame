using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cabinet : EventTrigger {
    [SerializeField] Transform HiddenPos, TriggerPos;
    [SerializeField] GameObject dialog;
    [SerializeField] Text text;
    bool isTrigger;
    bool isPlayer;

    public override void Init(int index) {
        isTrigger = isPlayer = false;
        text.text = "按E藏入";
        dialog.SetActive(false);
    }

    public override void Trigger() {
        if (isTrigger) {
            GameManager.instance.player.transform.position = TriggerPos.position;
            text.text = "按E藏入";
            isTrigger = false;
        }
        else {
            GameManager.instance.player.transform.position = HiddenPos.position;
            text.text = "按E出来";
            isTrigger = true;
        }
    }

    public override bool Check() {
        if (Input.GetKeyDown(KeyCode.E) && isPlayer) {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other) {
        if (isTrigger) return;
        if (other.tag == "Player") {
            dialog.SetActive(true);
            isPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (isTrigger) return;
        if (other.tag == "Player") {
            dialog.SetActive(false);
            isPlayer = false;
        }
    }
}
