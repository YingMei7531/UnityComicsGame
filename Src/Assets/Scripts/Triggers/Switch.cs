using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : EventTrigger {
    [SerializeField] GameObject text;
    [SerializeField] GameObject door;
    bool isTrigger;
    bool isPlayer;
    Vector3 doorPos;
    Coroutine openDoor;

    private void Awake() {
        doorPos = door.transform.position;
    }

    public override void Init(int index) {
        isTrigger = isPlayer = false;
        text.SetActive(false);
        door.transform.position = doorPos;
        if(openDoor!=null) StopCoroutine(openDoor);
    }

    public override void Trigger() {
        text.SetActive(false);
        openDoor = StartCoroutine(OpenDoor());
    }

    public override bool Check() {
        if (Input.GetKeyDown(KeyCode.E) && isPlayer && !isTrigger) {
            isTrigger = true;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other) {
        if (door.activeSelf == false) return;
        if (isTrigger) return;
        if (other.tag == "Player") {
            text.SetActive(true);
            isPlayer = true;
        }
        else if (other.tag == "Rejector") {
            isTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Rejector") {
            isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (door.activeSelf == false) return;
        if (isTrigger) return;
        if (other.tag == "Player") {
            text.SetActive(false);
            isPlayer = false;
        }
    }

    IEnumerator OpenDoor() {
        for (int i = 0; i < 100; i++) {
            door.transform.position += Vector3.up * 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
