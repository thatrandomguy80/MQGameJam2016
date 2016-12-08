using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

//Author: Hayden Munday.
//Description: Simple script to allow gravity to be changed provided the player sources the parts needed.

public class GravityChanger : MonoBehaviour {

    public GameObject[] partsNeeded;
    public List<Transform> snapPoints;
    public float partsCollected = 0;
    public LightScript light;

    public Grab g; // grab script;

    private GameObject Player;

    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void buttonCall() {//the panel button will call this.
        if (partsNeeded.Length == partsCollected) {
            GameState.GravOn = !GameState.GravOn;//toggle grav
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (isPart(other.gameObject.GetInstanceID())) {//is this a required part? 
            //force drop
            other.gameObject.GetComponent<GravityAffector>().isChild = false;
            other.gameObject.transform.parent = null;//changed to this from detach -hm
            g.heldObject = null;
            g.grabbing = false;
            g.stopMove = false;

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
            if (partsNeeded.Length == partsCollected) {
                light.SwitchLight();
            }
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
