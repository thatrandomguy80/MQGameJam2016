using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: Base class for buttons in the game.

public class Button : MonoBehaviour {

    public Material defaultMat;
    public GameObject Player;
    [Header("This is how much the button will move when pressed")]
    public Vector3 MoveVector;
    [Header("Always set to button and set Layer to button")]
    public LayerMask LM;
    private bool tick = true, animating, reverse;
    private Vector3 calcPos, OGPos;
    // Use this for initialization
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void DoAction() {

    }
    private void AnimationLoop() {//starts moving back
        calcPos = transform.position + MoveVector;
        OGPos = transform.position;
        animating = true;
    }

    private void animations() {
        if (!reverse) {//move backward
            transform.position = Vector3.Lerp(transform.position, calcPos, Time.deltaTime * 3);
            if (Vector3.Distance(transform.position, calcPos) < 0.01f) {
                reverse = !reverse;
            }
        } else {
            transform.position = Vector3.Lerp(transform.position, OGPos, Time.deltaTime * 3);
            if (Vector3.Distance(transform.position, OGPos) < 0.01f) {
                reverse = !reverse;
                animating = !animating;//stop animating and reset reverse.
                Debug.Log("StopAnimation");
            }
        }
    }


    // Update is called once per frame
    void Update() {
        if (animating) {
            animations();
        } else tick = true;
        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.DrawRay(Player.transform.position, Player.transform.forward * 5, Color.blue, 10);
            Ray ray = new Ray(Player.transform.position, Player.transform.forward);
            RaycastHit hit;


            if (Physics.SphereCast(ray, 1.5f, out hit, 0.5f, LM)) {
                if (hit.collider.gameObject.transform == transform) {

                    if (tick) {//is the button finished animation?
                               //mr.material = highListMat;
                        AnimationLoop();
                        DoAction();
                        tick = !tick;
                    }
                }
            }
        }
    }
}
