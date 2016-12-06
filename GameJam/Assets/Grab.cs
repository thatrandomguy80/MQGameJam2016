using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//creator: Emily Cochrane
//Description: 
/*trigger track object within
     * Gameobject currentobject
     * if current is null and grabbing flag is null
     *  set current object to colliding 
     *  
     *  press e and currently grabbing is flase and current object !null
     *      currentobject.transform.parent = this.gameObject(player)
     *       set the position of the object(pickup) to a "lock on spot"
     *       set currently grabbing flag.
     *       
     *   press e and currently grabbing is true
     *      undo above changes
     */
//public GameObject currentObject = null;
public bool grabbing = false;

public class Grab : MonoBehaviour {
	//get current object
	void OnTriggerEnter(Collider other){
		if(grabbing = false)
		{
		
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
