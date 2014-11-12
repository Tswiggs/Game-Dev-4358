using UnityEngine;
using TouchScript.Gestures;
using System.Collections;
using System;
/// <summary>
/// Listens for the user to tap the screen, then activates the special ability.
/// </summary>

/**
 * This class should never be directly applied to a prefab. It is automatically mapped by the "AddZugiePower" script, which is what should be added to prefabs. 
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
		wolfGangBallOriginal = transform.FindChild("Ball").gameObject;
		wolfGangBallOriginal.GetComponent<TapGesture> ().Tapped += createGangOfWolves;
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
		if ((!isActivated) && (wolfGangBallOriginal.GetComponent<SteeringController>().isRolling == true)) {
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
