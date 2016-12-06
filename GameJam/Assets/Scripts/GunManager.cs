using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: Sets gun mode in this order grab>shrink>grow>grab
public class GunManager : MonoBehaviour {

    public GunMode shrink,grow,grab;

    [HideInInspector]
    public GunMode currMode;

    // Use this for initialization
    void Start() {
        grab.nextMode = shrink;
        shrink.nextMode = grow;
        grow.nextMode = grab;
        //init
        currMode = grab;
        grab.setActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currMode.ChangeMode();
            currMode = currMode.nextMode;
        }
    }

    
}

[System.Serializable]
public class GunMode
{
    public string ID;
    public MeshRenderer meshR;
    public Light light;
    public Material mat, defaultMat;
    public GunMode nextMode;


    public void setActive()
    {
        //turn on light and color the object.
        light.enabled = true;
        meshR.material = mat;
    }

    public void setInActive()
    {
        //turn off light and object color.
        light.enabled = false;
        meshR.material = defaultMat;
    }

    public void ChangeMode()
    {
        setInActive();
        nextMode.setActive();
    }
}
