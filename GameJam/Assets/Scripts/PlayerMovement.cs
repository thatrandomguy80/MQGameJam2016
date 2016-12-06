using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: controller for playermovement and rotation

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 300f;
    public float rotateSpeed = 4f;



    private bool MouseLock = false;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        checkStopping();
    }

    //trying to stop player from sliding after user stops input
    private void checkStopping()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Strafe") == 0)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    //gets inputs from mouse and w,s to controll basic movement
    private void Movement()
    {
        Vector3 movementDir = Vector3.zero;
        //forward movement
        movementDir = transform.forward * Input.GetAxis("Vertical");

        //turning horizontal was changed to mouse movement in the editor
        if (!MouseLock)
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed, 0), Space.World);
            //locking the up down rotation
            if ((!withinXUnits(transform.rotation.eulerAngles.x,300f,5f) && Input.GetAxis("MouseVertical") > 0) || (!withinXUnits(transform.rotation.eulerAngles.x, 55f , 5f) && Input.GetAxis("MouseVertical") < 0 ))
            {
                transform.Rotate(new Vector3(Input.GetAxis("MouseVertical") * rotateSpeed * -1, 0, 0));
            }
        }



        movementDir *= Time.deltaTime * Speed;
        controller.SimpleMove(movementDir);

        //strafe
        controller.SimpleMove(Input.GetAxis("Strafe") * transform.right * (Time.deltaTime * (0.75f * Speed)));
    }


    // is pos within x units of other
    public bool withinXUnits(float pos, float other, float x)
    {
        if (pos > other)
        {
            if (pos - x < other)
                return true;
        }
        else
            if (pos + x > other)
            return true;

        return false;
    }
    public void LockMouse()
    {
        MouseLock = !MouseLock;//apllies mouse lock during object roation from grabobject script.
    }
}
