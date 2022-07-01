using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextCellTrigger : MonoBehaviour
{
    public CellScene scene;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" /*|| other.tag == "Hazmat"*/) {
            GameManager.instance.GetCurPage().NextCell(other.gameObject, scene.GetIndex());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Hazmat") {
            GameManager.instance.GetCurPage().NextCell(other.gameObject, scene.GetIndex());
        }
    }
}
