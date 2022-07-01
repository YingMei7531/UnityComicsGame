using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { 
    Player,
    Editor,
    Run,
    Start,
    End
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    [SerializeField] Transform restPos;
    [SerializeField] GameObject cover, backCover;
    GameState curGameState;
    [SerializeField] Page[] pages;
    int pageIndex;
    public Player player;

    private void Awake() {
        instance = this;
        curGameState = GameState.Start;
        pageIndex = -1;
        for(int i = 0;i<pages.Length;i++) { 
            pages[i].gameObject.SetActive(false);
            //pages[i].SetScenesActive(false);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (curGameState == GameState.Player) {
                //StartCoroutine(PageDown());//test
                SwitchEditorMode();
            }
            else if (curGameState == GameState.Editor) {
                SwitchPlayerMode();
            }
            else if (curGameState == GameState.Start) {
                StartCoroutine(PageDown());
            }
            else if (curGameState == GameState.End) {
                StartCoroutine(Restart());
            }
        }
    }

    public GameState GetGameState() {
        return curGameState;
    }

    public Page GetCurPage() {
        return pages[pageIndex];
    }

    public void NextPage() {
        StartCoroutine(PageDown());
    }

    void SwitchPlayerMode() {
        if (pageIndex >= pages.Length) return;
        pages[pageIndex].Init();
        player.Init();
        pages[pageIndex].NextCell(player.gameObject, -1);
        curGameState = GameState.Player;
    }
    void SwitchEditorMode() {
        if (pageIndex >= pages.Length) return;
        pages[pageIndex].Init();
        player.Init();
        player.transform.position = restPos.position;
        curGameState = GameState.Editor;
    }

    IEnumerator PageDown() {
        curGameState = GameState.Run;
        if (pageIndex < 0) {
            StartCoroutine(Slide(cover, -Vector3.right * 50, 100, false));
        }
        else {
            StartCoroutine(Slide(pages[pageIndex].gameObject, -Vector3.right * 50, 100, false));
        }
        pageIndex++;
        if (pageIndex < pages.Length) {
            pages[pageIndex].gameObject.SetActive(true);
            //pages[pageIndex].SetScenesActive(true);
            pages[pageIndex].Init();
            yield return new WaitForSeconds(1f);
            SwitchPlayerMode();
        }
        else {
            player.transform.position = restPos.position;
            yield return new WaitForSeconds(0.5f);
            curGameState = GameState.End;
        }
    }

    IEnumerator Restart() {
        player.transform.position = Camera.main.transform.position + new Vector3(0f, 0f, -10f);
        curGameState = GameState.Run;
        for (int i = pages.Length - 1; i >= 0; i--) {
            pages[i].gameObject.SetActive(true);
            //pages[i].SetScenesActive(true);
            pages[i].Shuffle();
            StartCoroutine(Slide(pages[i].gameObject, Vector3.right * 50, 50));
            yield return new WaitForSeconds(0.4f);
        }
        cover.gameObject.SetActive(true);
        StartCoroutine(Slide(cover, Vector3.right * 50, 50));
        pageIndex = -1;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < pages.Length; i++) {
            pages[i].gameObject.SetActive(false);
            //pages[i].SetScenesActive(false);
        }
        curGameState = GameState.Start;
    }

    IEnumerator Slide(GameObject obj, Vector3 offset, int time = 100, bool isActive = true) {
        float deltaTime = 0.01f;
        Vector3 deltaOffset = offset / time;
        for (int i = 0; i < time; i++) {
            obj.transform.position += deltaOffset;
            yield return new WaitForSeconds(deltaTime);
        }
        if (!isActive) {
            obj.SetActive(false);
        }
    }
}