using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandeler : MonoBehaviour {
    public Image AmountCleanedImage;
    private int amountOfObjects;//remaining
    private int amountInLevel;
    private float fillAmount;//the fill amount we aim for 
	// Use this for initialization
	void Start () {
		amountInLevel = GameObject.FindGameObjectsWithTag("Objects").Length - GameState.AmountOfDebris; //amountInLevel = GameObject.FindGameObjectsWithTag("Objects").Length - 1 - 4 - (GameState.AmountOfDebris / 5) * 4; ;//minus 4 for every 5 small parts also in win and 4 for compenents to the panel.

        //need this for other non scripted section
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Water"), LayerMask.NameToLayer("Default"), true);
    }
	
	// Update is called once per frame
	void Update () {
        amountOfObjects = GameState.objectsRemaining;
        fillAmount = (float)amountOfObjects / (float)amountInLevel;
        AmountCleanedImage.fillAmount = Mathf.Lerp(AmountCleanedImage.fillAmount, fillAmount, Time.deltaTime); // more smoothly moves towards current amount left to clean
	}
}
