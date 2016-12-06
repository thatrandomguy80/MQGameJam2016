using UnityEngine;
using System.Collections;
//Author: Hayden Munday.
//Description: Applys custom gravity and if active then reverses that gravity

[RequireComponent(typeof(Rigidbody))]
public class GravityAffector : MonoBehaviour {
	public bool active = false;
	public bool isChild = false;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;//using my own gravity
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isChild) {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
			if (active) {
				rb.AddForce ((Physics.gravity * -1) * rb.mass);
			} else {
				rb.AddForce (Physics.gravity * rb.mass);
			}
		} else {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition;
		}
	}
}
