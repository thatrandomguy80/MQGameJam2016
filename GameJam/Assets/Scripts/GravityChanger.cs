using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Author: Hayden Munday.
//Description: Simple script to allow gravity to be changed provided the player sources the parts needed.

public class GravityChanger : MonoBehaviour {

    public GameObject[] partsNeeded;
    public List<Transform> snapPoints;
    public float partsCollected = 0;

    private bool withinTrigger = false;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (withinTrigger && Input.GetKeyDown(KeyCode.F) && partsNeeded.Length == partsCollected) {//this should probably be a raycast to the button but gameJam
            Debug.Log("Grav tog");
            GameState.GravOn = !GameState.GravOn;//toggle grav
        }

    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger");
        if (other.gameObject.tag == "Player") {
            withinTrigger = true;
            Debug.Log("PlayerTRig");
        }
        if (isPart(other.gameObject.GetInstanceID())) {//is this a required part?
            //delete colliders on part
            BoxCollider noNull = other.gameObject.GetComponent<BoxCollider>();
            if (noNull != null) { Destroy(noNull); }

            CapsuleCollider notNull = other.gameObject.GetComponent<CapsuleCollider>();
            if (notNull != null) { Destroy(noNull); }

            //lock in and remove panel meshrenderer
            Transform temp = snapPoints[0];
            other.gameObject.transform.position = temp.position;
            other.gameObject.transform.rotation = temp.rotation;
            Destroy(temp.gameObject.GetComponent<MeshRenderer>());
            snapPoints.Remove(temp);

            
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//lock in and freeze to first snap point then remove that snap point.
            partsCollected++;
            other.gameObject.tag = "nothing";//can't be grabbed now
            Destroy(other.gameObject.GetComponent<GravityAffector>());

           

        }
    }
    public void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            Debug.Log("Playerleft");
            withinTrigger = false;
        }
    }

    private bool isPart(int instance) {
        foreach (GameObject x in partsNeeded) {
            if (instance == x.GetInstanceID())
                return true;
        }
        return false;
    }


}
