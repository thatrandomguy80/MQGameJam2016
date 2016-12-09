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
	public string objectsTagName2 = "Part";
    public GunManager gunManager;//added -hm
    private GameObject firstobject;
    public GameObject snapPoint;
	public float speed = 5f;
	public bool stopMove = false;

     //get current object
    //can add a list of all gameObjects that have Entered and remove the gameobjects from the list as they leave and only pick up the first gameobejct in the list
    void OnTriggerEnter(Collider other) {

		if (grabbing == false  && currentObject == null && CheckTag(other)) {
            currentObject = other.gameObject;
        }

    }
    void OnTriggerExit(Collider other) {
        //if the currentobject leaves set to null
		if (currentObject != null  && currentObject.GetInstanceID() == other.gameObject.GetInstanceID() && CheckTag(other)) {
            currentObject = null;

        }
    }
	public bool CheckTag(Collider Tag){
		if (Tag.tag == objectsTagName || Tag.tag == objectsTagName2)
			return true;
		else
			return false;
	}
	public bool CheckTag2(GameObject Tag){
		if (Tag.tag == objectsTagName || Tag.tag == objectsTagName2)
			return true;
		else
			return false;
	}
    // Update is called once per frame
    void Update() {
		//grab
		/*  press e and currently grabbing is false and current object !null
            *      currentobject.transform.parent = this.gameObject(player)
                *       set the position of the object(pickup) to a "lock on spot"
                *       set currently grabbing flag.*/
		//added to fix parts not clearing bug-hm
        if (grabbing == false && currentObject != null ) {
			if (!CheckTag2(currentObject)) {
				currentObject = null;
				heldObject = null;
				stopMove = false;
			}
        }
		// implement a movetowards in the update and an objectlook when grabbng, remove these when object leaves trigger - remove the remove collider
		//Vector3.distance - distance between 2 points
		if (grabbing == true && heldObject != null) {
			//addes the object to parent snapPoint
			//currentObject.transform.parent = snapPoint.transform;
			float step = speed * Time.deltaTime;
			if(Vector3.Distance(heldObject.transform.position,snapPoint.transform.position) > 0.2f && stopMove == false)
				heldObject.transform.position = Vector3.MoveTowards(heldObject.transform.position, snapPoint.transform.position, step);
			//snap to point when within a certain distance
			if (Vector3.Distance (heldObject.transform.position, snapPoint.transform.position) < 0.2f) {
				stopMove = true;
				//heldObject.transform.position = snapPoint.transform.position;
				//attach the currentObject(child) to this gameObject(parent)
				heldObject.transform.parent = snapPoint.transform;
			}
		}
           
            if (Input.GetMouseButtonDown(0) && gunManager.currMode.ID == "grab") {//changed this for new control scheme-hm
			//attach the currentobject
	            if (grabbing == false && currentObject != null) {
	                //set the position of the child
	                GravityAffector ga = currentObject.GetComponent<GravityAffector>();
				if (ga != null)
					ga.isChild = true;
				else
					print ("can't find GravityAffector scirpt");

					heldObject = currentObject;
					//disable the box collider
					//heldObject.GetComponent<BoxCollider>().enabled = false;
	                grabbing = true;


	            } else if (grabbing && heldObject != null) {
	                //heldObject.GetComponent<BoxCollider>().enabled = true;
	                heldObject.GetComponent<GravityAffector>().isChild = false;
	                heldObject.transform.parent = null;//changed to this from detach -hm
					heldObject = null;
	                grabbing = false;
					stopMove = false;
	            }
        }

		//rotation
		if (Input.GetKey(KeyCode.Q) && grabbing) {
            //These might need to be in world space.
            currentObject.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0));
            currentObject.transform.Rotate(new Vector3(Input.GetAxis("MouseVertical"), 0, 0));
        }
    }
}
