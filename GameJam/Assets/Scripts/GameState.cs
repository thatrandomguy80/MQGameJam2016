using UnityEngine;
using System.Collections;


//no longer mono
public static class GameState {

    public static int objectsRemaining;

    public static bool hasWon = false;

    public static bool GravOn = false;
    
	// Update is called once per frame
	//void Update () {
	  //  if (hasWon) {
            //do winning stuff
            //UI
            //nextscene?
            //replay? using unity scene manager.
            //reset var? 
      //  }
	//}

    /*trigger track object within
     * Gameobject currentobject
     * if current is null and grabbing flag is null
     *  set current object to colliding 
     *  
     *  press e and currently grabbing is flase and current object !null
     *      currentobject.transform.parent = this.gameObject(player)
     *       set the position of the object(pickup) to a "lock on spot"
     *       set currently grabbing flag.
     *       
     *   press e and currently grabbing is true
     *      undo above changes
     */

}
