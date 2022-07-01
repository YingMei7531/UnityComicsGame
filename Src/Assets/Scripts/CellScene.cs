using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScene : MonoBehaviour {
    public Camera camera;
    public Transform birthPos;
    public EventTrigger[] eventTriggers;
    int sceneIndex;

    public void Init(int index) {
        sceneIndex = index;
        foreach (EventTrigger trigger in eventTriggers) {
            trigger.gameObject.SetActive(true);
            trigger.Init(sceneIndex);
        }
    }

    public int GetIndex() {
        return sceneIndex;
    }
}
