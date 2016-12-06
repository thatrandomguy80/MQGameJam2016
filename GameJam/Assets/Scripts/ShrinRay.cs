using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: A Gun that skrinks objects that it hits or grows them
[RequireComponent(typeof(GunManager))]
public class ShrinRay : MonoBehaviour {

    public GameObject BulletPrefab;
    public float shrinkAmount;
    public bool grow = false, shrink = false;//if the scale change is positive or neg

    private GunManager gunManager;
    // Use this for initialization
    void Start() {
        gunManager = GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {

            grow = gunManager.currMode.ID == "grow"; // are we in one of these modes
            shrink = gunManager.currMode.ID == "shrink";
            if (grow || shrink) {
               

                Ray ray = new Ray(transform.position, transform.parent.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 5)) {
                    Debug.Log("hit" + hit.collider.gameObject.name);

                    if (hit.collider.gameObject.tag == "Objects")//if the ray hits a tagged object then apply scale changes
                    {
                        applyScaling(hit.collider.gameObject);
                    }
                }
                Debug.DrawLine(transform.position, transform.position + (transform.parent.forward), Color.red, 20);
                Instantiate(BulletPrefab, transform.position + transform.parent.forward, transform.parent.rotation);
            }
        }
    }

    public void applyScaling(GameObject GO) {
        if (grow) {
            GO.transform.localScale += new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
        } else if (shrink) {
            GO.transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
        }
    }
}
