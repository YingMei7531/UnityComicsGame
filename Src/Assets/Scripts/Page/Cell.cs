using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {
    [SerializeField] GameObject selectedFrame;
    [SerializeField] CellScene scene;

    public void Init(int index) {
        SetState(false);
        if (scene!=null) scene.Init(index);
    }

    public void SetState(bool selected) {
        if (selected) selectedFrame.SetActive(true);
        else selectedFrame.SetActive(false);
    }

    public void IntoCell(GameObject obj) {
        obj.transform.position = scene.birthPos.position;
        if (obj.tag == "Player") obj.GetComponent<Player>().SetScene(scene);
        else if (obj.tag == "Hazmat") obj.GetComponent<EventTrigger>().IntoCell(scene.birthPos.position);
    }
}
