using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: Sets gun mode in this order grab>shrink>grow>grab
public class GunManager : MonoBehaviour {

	public GameObject GunModel;
    public GameObject auroa;
    public Grab grabber;

    public GunMode shrink, grow, grab;

    [HideInInspector]
    public GunMode currMode;

    public Transform defaultPos,RightLock;

	public Transform GunPos,RecoilPos;


	private bool recoil, recoilDir;
	private float recoilSpeed = 4;


    // Use this for initialization
    void Start() {
        grab.unlock();
        grab.nextMode = shrink;
        shrink.nextMode = grow;
        grow.nextMode = grab;
        //init
        currMode = grab;
        grab.setActive();
    }

    // Update is called once per frame
    void Update() {
        if (grabber.grabbing) {
            auroa.transform.position = RightLock.position;
        }else {
            auroa.transform.position = defaultPos.position;
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
		//recoil
		if (Input.GetMouseButtonDown (0) && currMode != grab && !recoil) {
			recoil = true;
		}
		if (recoil) {
			RecoilLoop();
		}
    }
	public void RecoilLoop(){
		if (!recoilDir) {//move backwards
			GunModel.transform.position = Vector3.Lerp (GunModel.transform.position, RecoilPos.position, Time.deltaTime * recoilSpeed);
			if (Vector3.Distance (GunModel.transform.position, RecoilPos.position) < 0.1f) {
				recoilDir = !recoilDir;
			}
		} else {
			GunModel.transform.position = Vector3.Lerp (GunModel.transform.position,GunPos.position, Time.deltaTime * recoilSpeed);
			if (Vector3.Distance (GunModel.transform.position, GunPos.position) < 0.1f) {
				recoilDir = !recoilDir;
				recoil = false;
			}
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
            nextMode.setActive();
    }
}
