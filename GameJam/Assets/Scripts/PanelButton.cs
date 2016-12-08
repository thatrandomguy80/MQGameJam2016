using UnityEngine;
using System.Collections;

public class PanelButton : Button {//grav machine panel button

    public GravityChanger gravScript;
    public override void DoAction() {
        Debug.Log("Called GravPanel");
        gravScript.buttonCall();
    }
}
