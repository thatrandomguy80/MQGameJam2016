using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Emily Cochrane
//Descrition: detects when an Object is inside the Trigger and if all objects are inside then the Game Win's
public class Win : MonoBehaviour {
    //public GameObject[] List;
	public string objectsTagName;
	private int objectsOnLevel;//only set once and can not be changed
	public int objectsInTrigger = 0;
	public int objectsRemainingInLevel;

	// Use this for initialization
	void Start () {
        //List =  GameObject.FindGameObjectsWithTag("Objects");
        //find the number of Objects in level
		objectsOnLevel = GameState.AmountOfObjects - ((GameState.AmountOfDebris / 5) * 4); // objectsOnLevel = GameObject.FindGameObjectsWithTag("Objects").Length -1 - 4 - (GameState.AmountOfDebris/5)*4;//minus 4 for every 5 small parts also in UIHandeler - hm
        //running this once at start for UI init - hm
        objectsRemainingInLevel = objectsOnLevel - objectsInTrigger;
        GameState.objectsRemaining = objectsRemainingInLevel;
    }
	//detects if an object is in the Trigger
	void OnTriggerEnter(Collider other){
		if (other.tag == objectsTagName) {
			objectsInTrigger++;
			objectsRemainingInLevel = objectsOnLevel - objectsInTrigger;
			GameState.objectsRemaining = objectsRemainingInLevel;
            Debug.Log(objectsRemainingInLevel);
			//win check
			if (objectsRemainingInLevel == 0) {
				GameState.hasWon = true;
			}
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == objectsTagName) {
			objectsInTrigger--;
			objectsRemainingInLevel = objectsOnLevel - objectsInTrigger;
			GameState.objectsRemaining = objectsRemainingInLevel;
		}
	}
}
