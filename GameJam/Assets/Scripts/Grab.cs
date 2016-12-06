using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//creator: Emily Cochrane
//Modifyer: Hayden Munday. - rotation
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



public class Grab : MonoBehaviour {
	public GameObject currentObject = null;
	public GameObject heldObject = null;
	public bool grabbing = false;
	public string objectsTagName = "Objects";
    public GunManager gunManager;//added -hm
    private GameObject firstobject;
	public GameObject snapPoint;
	//rotation
	//public GameObject Test; // to test grab rotation
	//public PlayerMovement PM;
	// Use this for initialization
	void Start () {
		//PM = transform.parent.parent.GetComponent<PlayerMovement> ();
	}

	//get current object
	//can add a list of all gameObjects that have Entered and remove the gameobjects from the list as they leave and only pick up the first gameobejct in the list
	void OnTriggerEnter(Collider other){
		
		if(grabbing == false && other.tag == objectsTagName && currentObject == null)
		{
			currentObject = other.gameObject;
			print ("CurrentObejct entering");
		}

	}
	void OnTriggerExit(Collider other){
		//if the currentobject leaves set to null
		if (currentObject != null && other.tag == objectsTagName && currentObject.GetInstanceID() == other.gameObject.GetInstanceID()) {
			currentObject = null; 
			print ("CurrentObejct leaving");

		}
	}

	// Update is called once per frame
	void Update () {
		//grab
		/*  press e and currently grabbing is false and current object !null
		*      currentobject.transform.parent = this.gameObject(player)
			*       set the position of the object(pickup) to a "lock on spot"
			*       set currently grabbing flag.*/
		// implement a movetowards in the update and an objectlook when grabbng, remove these when object leaves trigger
		if(Input.GetMouseButtonDown(0) && gunManager.currMode.ID == "grab"){//changed this for new control scheme-hm
			
			if (grabbing == false && currentObject != null) {
				//print ("testing key press");
				//attach the currentObject(child) to this gameObject(parent)
				//currentObject.transform.parent = this.transform;
				//set the position of the child
				GravityAffector ga = currentObject.GetComponent<GravityAffector>();
				if (ga != null) {
					//print ("testing!");

					ga.isChild = true;
					//print (currentObject.name + ga.isChild);
				} else {
					print ("can't find GravityAffector scirpt");
				}
				//set the child's position
				currentObject.transform.position = snapPoint.transform.position;
				//disable the box collider
				currentObject.GetComponent<BoxCollider>().enabled = false;
				print ("attach");
				grabbing = true;
			

			}
			else if (grabbing && currentObject != null) {
				currentObject.GetComponent<BoxCollider>().enabled = true;
				currentObject.GetComponent<GravityAffector>().isChild = false;
                currentObject.transform.parent = null;
				print ("Detach");
				grabbing = false;
			}
		}
		if (grabbing == true && currentObject != null) {
			currentObject.transform.parent = snapPoint.transform;
		}
		//rotation
		if(Input.GetKey(KeyCode.Q)){
			//PM.LockMouse ();//stops mouse from rotating the player.

			//These might need to be in world space.
			currentObject.transform.Rotate (new Vector3 (0, Input.GetAxis ("Horizontal"), 0));
			currentObject.transform.Rotate (new Vector3 (Input.GetAxis("MouseVertical"), 0, 0));


			//PM.LockMouse ();
		}
}
}
