using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour {

    public float Speed = 50f;
    public float rotateSpeed = 4f;

    private CharacterController controller;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 movementDir = Vector3.zero;

        movementDir = transform.forward * Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0,Input.GetAxis("Horizontal") * rotateSpeed, 0));
        movementDir.Normalize();
        movementDir *= Time.deltaTime * Speed;
        controller.SimpleMove(movementDir);


        
	    
	}
}
