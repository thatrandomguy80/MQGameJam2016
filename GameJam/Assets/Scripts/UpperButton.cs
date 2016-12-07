using UnityEngine;
using System.Collections;


public class UpperButton : Button {//button for compactor
    public Compactor compScript;
    public override void DoAction() {
        Debug.Log("Called Compact");
        compScript.Compact();
    }
}