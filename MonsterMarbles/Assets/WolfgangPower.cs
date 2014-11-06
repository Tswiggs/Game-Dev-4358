using UnityEngine;
using TouchScript.Gestures;
using System.Collections;
using System;
/// <summary>
/// Listens for the user to tap the screen, then activates the special ability.
/// </summary>

/**
 * This class can be attached to any part of wolfgang, so put it wherever it will allow access to the 
 * touch event. After placing it on his prefab just make sure that the wolfGangBallOriginal variable is pointing to Wolfgang's "Ball" object.
 * This can be done through unity by dragging and dropping the "Ball" object into the wolfGangBallOriginal variable slot through
 * the editor. 
 *
 * The next step is to detect whether or not the touch occured, and if it has then call the method createGangOfWolves, and call the rollComplete method when the roll is 
 * complete. Delete this part of the comment whenever it's been finished.  
 **/ 
public class WolfgangPower : MonoBehaviour {

	/// <summary>
	/// the original wolf gang ball. Point this to Wolfgang's "Ball" object. 
	/// </summary>
	public GameObject wolfGangBallOriginal; 
	/// <summary>
	/// wolfgang's first duplicate. 
	/// </summary>
	private GameObject wolfGangBall2;
	/// <summary>
	/// wolfgang's second duplicate. 
	/// </summary>
	private GameObject wolfGangBall3;
	/// <summary>
	/// Is the ability activated yet? 
	/// </summary>
	private bool isActivated = false; 
	// Use this for initialization
	void Start () {
		wolfGangBallOriginal = gameObject;
		GetComponent<TapGesture> ().Tapped += createGangOfWolves;
		SteeringController.rollCompleted += rollComplete;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActivated) {
						wolfGangBall2.transform.rotation = wolfGangBallOriginal.transform.rotation; 
						wolfGangBall2.rigidbody.velocity = wolfGangBallOriginal.rigidbody.velocity;
						wolfGangBall3.transform.rotation = wolfGangBallOriginal.transform.rotation;
						wolfGangBall3.rigidbody.velocity = wolfGangBallOriginal.rigidbody.velocity;
				}
	}

	public void createGangOfWolves(object sender, EventArgs e)
	{
		if ((!isActivated) && (GetComponent<SteeringController>().isRolling == true)) {
						wolfGangBall2 = Instantiate (wolfGangBallOriginal, wolfGangBallOriginal.transform.position + new Vector3 (2, 0, -2), wolfGangBallOriginal.transform.rotation) as GameObject; 
			wolfGangBall2.GetComponent<WolfgangPower>().enabled = false; 
						wolfGangBall3 = Instantiate (wolfGangBallOriginal, wolfGangBallOriginal.transform.position + new Vector3 (-2, 0, 2), wolfGangBallOriginal.transform.rotation) as GameObject;
			wolfGangBall3.GetComponent<WolfgangPower>().enabled = false; 
		
			                             isActivated = true; 
				}

	}

	public void rollComplete()
	{
		Destroy (wolfGangBall2); 
		Destroy (wolfGangBall3);
		isActivated = false; 
	}
}
