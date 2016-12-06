using UnityEngine;
using System.Collections;
//Author: Hayden Munday.
//Description: changes gravity for all objects within the field area
public class GravityField : MonoBehaviour
{


    //if an object enters then check if it's gravity is normal and if so flip it.
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Objects" || other.gameObject.tag == "Player")
        {
            GravityAffector otherGA = other.gameObject.GetComponent<GravityAffector>();
            if (!otherGA.active)
            {
                otherGA.active = true;
            }
        }
    }

    //if the object is active set to inactive
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Objects")
        {
            GravityAffector otherGA = other.gameObject.GetComponent<GravityAffector>();
            if (otherGA.active)
            {
                otherGA.active = false;
            }
        }
    }
}
