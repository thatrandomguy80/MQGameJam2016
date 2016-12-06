using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: A Gun that skrinks objects that it hits

public class ShrinRay : MonoBehaviour {

    public float shrinkAmount;
    public bool pos = true;//if the scale change is positive or neg


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            pos = Input.GetMouseButtonDown(0); // should be true (or pos) is mouse 1 and neg if mouse 2
            Ray ray = new Ray(transform.position, transform.parent.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5))
            {
                if (hit.collider.gameObject.tag == "Objects")//if the ray hits a tagged object then apply scale changes
                {
                    applyScaling(hit.collider.gameObject);
                }
            }
            Debug.DrawLine(transform.position, transform.position + (transform.parent.forward * 5), Color.red, 20);
        }
	}

    public void applyScaling(GameObject GO)
    {
        if (pos)
        {
            GO.transform.localScale += new Vector3(shrinkAmount,shrinkAmount,shrinkAmount);
        }else
        {
            GO.transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);
        }
    }
}
