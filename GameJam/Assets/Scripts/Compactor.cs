using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Author: Hayden Munday.
//Description: Tracks parts in the compactor and handels turning them into the bail.

public class Compactor : MonoBehaviour {


    public GameObject bail;
    public int AmountOfItems;
    public LightScript[] Lights;

    public GameObject door, doorOpenLoc, doorClosePos;

    public bool opening, closing;

    private List<GameObject> items = new List<GameObject>();
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (opening) {
            door.transform.position = Vector3.Lerp(door.transform.position, doorOpenLoc.transform.position, Time.deltaTime);
            if (Vector3.Distance(door.transform.position, doorOpenLoc.transform.position) < 0.05f) {//if close then stop
                opening = false;
            }
        } else if (closing) {
            door.transform.position = Vector3.Lerp(door.transform.position, doorClosePos.transform.position, Time.deltaTime);
            if (Vector3.Distance(door.transform.position, doorClosePos.transform.position) < 0.05f) {//if close then stop
                closing = false;
                StartCoroutine(bailParts());
            }
        }
    }

    public IEnumerator bailParts() {//enumerable allows wait timer it's a form of cuncurrency 
        //delete items
        foreach (GameObject g in items) {
            //items.Remove(g);
            Destroy(g);
        }
        //spawn bail
        Instantiate(bail, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        //reset lighst
        foreach (LightScript l in Lights) {
            l.SwitchLight();
        }
        opening = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Debris>() != null) {
            items.Add(other.gameObject);
            updateLights();//must do before adding
            AmountOfItems++;
        }
    }

    void OnTriggerExit(Collider other) {
        if (items.Contains(other.gameObject)) {
            items.Remove(other.gameObject);
            AmountOfItems--;
            updateLights(); //needs to be after --
        }
    }

    private void updateLights() {
        if (AmountOfItems <= 4)
            Lights[AmountOfItems].SwitchLight();
    }

    public void Compact() {
        //called by triggerScript
        if (AmountOfItems == 5) {
            //close door which will do the rest after it's finished
            closing = true;
        }
    }
}

