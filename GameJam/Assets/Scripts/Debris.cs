using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GravityAffector))]
public class Debris : MonoBehaviour {

	// Called b4 start to help total left calc
	void Awake () {
        GameState.AmountOfDebris++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
