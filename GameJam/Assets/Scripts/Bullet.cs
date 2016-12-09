using UnityEngine;
using System.Collections;
//Author: Hayden Munday.
//Description: Bullet pushes itself and rotates then destroys itself after x amount of time

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    public float TTL;
    public float TimeSpawned;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * 5000);
        TimeSpawned = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(1000f * -1 * Time.deltaTime, 0, 0);
	    if(Time.time - TimeSpawned > TTL)
        {
            Destroy(this.gameObject);
        }
    }


}
