using UnityEngine;
using System.Collections;
//Author: Hayden Munday.
//Description: Will allow multiple Lights to be switched from one button

public class LightSwitch : Button {

    public bool StartingState;
    public LightScript[] lights;


    private bool state;

    void start() {
        state = StartingState;
    }

    public override void DoAction() {
        toggleLights();
    }

    private void toggleLights() {
        Debug.Log("Called");
        foreach(LightScript L in lights) {
            L.SwitchLight();
        }
    }
}
