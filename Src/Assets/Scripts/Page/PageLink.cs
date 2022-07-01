using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageLink : MonoBehaviour
{
    [SerializeField] GameObject scenes;

    private void OnEnable() {
        if (this.scenes != null) this.scenes.SetActive(true);
    }

    private void OnDisable() {
        if (this.scenes != null) this.scenes.SetActive(false);
    }
}
