using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: Base class for buttons in the game.

public class Button : MonoBehaviour {

    public Material defaultMat, highListMat;
    public GameObject Player;

    private bool tick = true;
    // Use this for initialization
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void DoAction() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.DrawRay(Player.transform.position, Player.transform.forward * 5, Color.blue, 10);
            Ray ray = new Ray(Player.transform.position, Player.transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 1.5f, out hit, 0.5f)) {
                if (hit.collider.gameObject.transform == transform) {


                    MeshRenderer mr = GetComponent<MeshRenderer>();
                    if (mr != null) {
                        if (tick) {
                            mr.material = highListMat;
                            tick = !tick;
                        } else {
                            mr.material = defaultMat;
                            tick = !tick;
                        }
                    } else Debug.LogError("NoMR Found");


                    DoAction();
                }
            }
        }
    }
}
