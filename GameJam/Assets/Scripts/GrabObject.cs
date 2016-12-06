using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {
	public GameObject Test; // to test grab rotation
	private PlayerMovement PM;
	// Use this for initialization
	void Start () {
		PM = GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Q)){
			PM.LockMouse ();//stops mouse from rotating the player.

			//These might need to be in world space.
			Test.transform.Rotate (new Vector3 (0, Input.GetAxis ("Horizontal"), 0));
			Test.transform.Rotate (new Vector3 (Input.GetAxis("MouseVertical"), 0, 0));


			PM.LockMouse ();
		}
		
	}
}
