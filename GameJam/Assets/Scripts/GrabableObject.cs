using UnityEngine;
using System.Collections;

//Author: Hayden Munday.
//Description: The basics that each object will need as well as some setting for scale modifiers/gun

[RequireComponent(typeof(GravityAffector))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class GrabableObject : MonoBehaviour {
    [Header("Can they Scale? only 1 at a time please")]
    public bool ScaleUp, ScaleDown;//can they scale?
    [Header("by how much")]
    public float ScaleUpAmount=0, ScaleDownAmount=0;

    private Rigidbody rb;
    void Awake()
    {
        GameState.AmountOfObjects++;
    }

    // Use this for initialization
    void Start () {
        gameObject.tag = "Objects";
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(Random.Range(0,0.5f),
			Random.Range(0,0.5f),Random.Range(0,0.5f)));//add random force at start.
    }
	
	// Update is called once per frame
	void Update () {
       if(rb.velocity.magnitude > 5) {
            rb.AddForce(rb.velocity * -0.5f);//slow down the object
        }

	}
}
