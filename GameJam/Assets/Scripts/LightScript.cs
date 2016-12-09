using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: Handels Light state and turning off on functions

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(MeshRenderer))]

public class LightScript : MonoBehaviour {

    public Material Def, Lit;

    public bool statingState = false;

    public bool flickering = false;

    private float lastCheck, checkTimer = 0.3f;//1 second per flick checkers

    private MeshRenderer MR;
    private Light light;
    private bool state;
    // Use this for initialization
    void Start() {
        state = statingState;
        light = GetComponent<Light>();
        MR = GetComponent<MeshRenderer>();
        if (state) {//sets starting state
            turnOn();
        } else {
            turnOff();
        }
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - lastCheck > checkTimer) {
            lastCheck = Time.time;
            if (flickering && state) {
                if (Random.Range(0, 10) > 8) {
                    SwitchLight();
                }
            } else if (flickering) {
                if (Random.Range(0, 10) > 3) {
                    SwitchLight();
                }
            }
        }
    }

    public void SwitchLight() {//called by others to flick the switch
        if (state) {
            turnOff();
        } else {
            turnOn();
        }
    }
    private void turnOn() {
        Debug.Log(gameObject.name + "Toggled on");
        state = true;
        light.enabled = true;
        MR.material = Lit;
    }

    private void turnOff() {
        Debug.Log(gameObject.name + "Toggled off");
        state = false;
        light.enabled = false;
        MR.material = Def;
    }
}
