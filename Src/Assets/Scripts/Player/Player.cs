using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CellScene curScene;
    Animator animator;
    AnimatorStateInfo currectAnimatorState;
    [SerializeField] GroundBox groundBox;
    [SerializeField] float speed = 1f;
    Vector3 input;
    bool isDied = false;

    private void Awake() {
        animator = GetComponent<Animator>();
        groundBox.animator = animator;
    }

    private void Update() {
        input = Vector3.zero;
        if (GameManager.instance.GetGameState() != GameState.Player) return;
        input.x = Input.GetAxis("Horizontal");
        if (input != Vector3.zero) {
            animator.SetBool("isRun", true);
        }
        else {
            animator.SetBool("isRun", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && groundBox.isGround) {
            animator.SetTrigger("Jump");
            groundBox.SetIsGround(false);
            StartCoroutine(Jump());
        }
        //Debug.Log(groundBox.isGround);
    }

    private void LateUpdate() {
        if (isDied) return;
        if (GameManager.instance.GetGameState() != GameState.Player) return;
        UpdateState();
    }

    void UpdateState() {
        currectAnimatorState = animator.GetCurrentAnimatorStateInfo(0);
        if (currectAnimatorState.IsName("Idie")) {
            Turn();
        }
        else if (currectAnimatorState.IsName("Run") || currectAnimatorState.IsName("Jump")) {
            Move();
        }
    }

    void Turn() {
        Vector3 lookForward = curScene.camera.transform.position - transform.position;
        lookForward.y = 0;
        transform.rotation = Quaternion.LookRotation(lookForward.normalized, Vector3.up);
    }

    void Move() {
        transform.position += input * speed * Time.deltaTime;
        if (input != Vector3.zero) transform.rotation = Quaternion.LookRotation(input.normalized, Vector3.up);
    }

    IEnumerator Jump() {
        GetComponent<Rigidbody>().useGravity = false;
        transform.position += Vector3.up * 0.05f;
        yield return new WaitForSeconds(0.9f);
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void SetScene(CellScene scene) {
        this.curScene = scene;
    }

    public void Init() {
        isDied = false;
        animator.SetBool("isDied", isDied);
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void Die(Vector3 position) {
        Vector3 lookForward = position - transform.position;
        lookForward.y = 0;
        transform.rotation = Quaternion.LookRotation(lookForward.normalized, Vector3.up);
        isDied = true;
        animator.SetBool("isDied", isDied);
        StopAllCoroutines();
    }
}
