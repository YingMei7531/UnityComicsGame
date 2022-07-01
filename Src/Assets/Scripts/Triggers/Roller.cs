using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : EventTrigger {
    [SerializeField] Vector3 offset;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject dialog;

    private void Update() {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public override void Init(int index) {
        dialog.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public override void IntoCell(Vector3 birthPos) {
        base.IntoCell(birthPos);
        transform.position += offset;
    }

    void Boom() {
        dialog.gameObject.SetActive(true);
        dialog.transform.position = transform.position + Vector3.forward + Vector3.up;
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().Die(transform.position);
            Boom();
        }
        else if (collision.gameObject.tag == "Rejector") {
            collision.gameObject.SetActive(false);
            Boom();
        }
    }
}
