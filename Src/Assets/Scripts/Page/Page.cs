using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Page : MonoBehaviour {
    public Cell[] priCells;
    Vector3[] cellPositions;
    Cell[] cells;
    Cell selectedCell;

    private void Awake() {
        cells = new Cell[priCells.Length];
        cellPositions = new Vector3[priCells.Length];
        for (int i = 0; i < priCells.Length; i++) cellPositions[i] = priCells[i].transform.localPosition;
        Shuffle();
    }

    private void Update() {
        if (GameManager.instance.GetGameState() != GameState.Editor) return;
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                Cell cell = hit.collider.gameObject.GetComponent<Cell>();
                if (cell != null) {
                    if (selectedCell == null) {
                        selectedCell = cell;
                        selectedCell.SetState(true);
                    }
                    else {
                        SwitchCell(cell);
                    }
                }
            }
        }
    }

    void SwitchCell(Cell cell) {
        int a, b;
        a = b = 0;
        for (int i = 0; i < cells.Length; i++) {
            if (cells[i] == selectedCell) {
                a = i;
                break;
            }
        }
        for (int i = 0; i < cells.Length; i++) {
            if (cells[i] == cell) {
                b = i;
                break;
            }
        }
        Cell tmp = cells[a];
        Vector3 tmpPos = tmp.transform.position;
        cells[a].transform.position = cells[b].transform.position;
        cells[b].transform.position = tmpPos;
        cells[a] = cells[b];
        cells[b] = tmp;
        selectedCell.SetState(false);
        selectedCell = null;
        Init();
    }

    public void Init() {
        //cellIndex = 0;
        selectedCell = null;
        for (int i = 0; i < cells.Length; i++) cells[i].Init(i);
    }

    public void Shuffle() {
        for (int i = 0; i < priCells.Length; i++) {
            cells[i] = priCells[i];
            cells[i].transform.localPosition = cellPositions[i];
        }
    }

    public void NextCell(GameObject obj,int index) {
        index++;
        if (index == cells.Length) {
            if (obj.tag == "Player") {
                GameManager.instance.NextPage();
            }
            else if (obj.tag == "Hazmat") {
                obj.SetActive(false);
            }
        }
        else {
            cells[index].IntoCell(obj);
        }
        /*cellIndex++;
        if (cellIndex == cells.Length) return null;
        else return cells[cellIndex];*/
    }
}
