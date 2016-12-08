using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: Sets gun mode in this order grab>shrink>grow>grab
public class GunManager : MonoBehaviour {

    public GameObject auroa;
    public Grab grabber;

    public GunMode shrink, grow, grab;

    [HideInInspector]
    public GunMode currMode;

    private Vector3 defaultPos;


    // Use this for initialization
    void Start() {
        grab.unlock();
        grab.nextMode = shrink;
        shrink.nextMode = grow;
        grow.nextMode = grab;
        //init
        currMode = grab;
        grab.setActive();
        defaultPos = auroa.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (grabber.grabbing) {
            auroa.transform.position = defaultPos + transform.right;
        }else {
            auroa.transform.position = defaultPos;
        }
        //could be more efficent here but meh
        if (currMode == grab) {
            auroa.SetActive(true);
        } else {
            auroa.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && (!(currMode == grab && grabber.grabbing))) {
            currMode.ChangeMode();
            currMode = currMode.nextMode;
        }
    }


}

[System.Serializable]
public class GunMode {
    public string ID;
    private bool unLocked = false;
    public MeshRenderer meshR;
    public Light light;
    public Material mat, defaultMat;
    public GunMode nextMode;

    public void unlock() {
        unLocked = true;
    }
    public void setActive() {
        //turn on light and color the object.
        light.enabled = true;
        meshR.material = mat;
    }

    public void setInActive() {
        //turn off light and object color.
        light.enabled = false;
        meshR.material = defaultMat;
    }

    public void ChangeMode() {
        setInActive();
        if (nextMode.unLocked)
            nextMode.setActive();
        else if (nextMode.nextMode.unLocked)
            nextMode.nextMode.setActive();
        else
            setActive();
    }
}
