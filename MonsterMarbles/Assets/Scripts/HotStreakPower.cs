using UnityEngine;
using System.Collections;
using System;
using TouchScript.Gestures;

public class HotStreakPower : MonoBehaviour {

	/// <summary>
	/// the original wolf gang ball. Point this to Wolfgang's "Ball" object. 
	/// </summary>
	public GameObject HotStreakBall; 

	private bool isActivated = false;
	void Start () {
		HotStreakBall = transform.FindChild("Ball").gameObject;
		HotStreakBall.GetComponent<TapGesture> ().Tapped += groundSlam;
		SteeringController.rollCompleted += rollComplete;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void groundSlam(object sender, EventArgs e)
	{
		if (isActivated == false && (GetComponent<SteeringController>().isRolling == true)) {
			isActivated = true;
						Collider[] colliders = Physics.OverlapSphere (transform.position, 20f); 
						foreach (Collider c in colliders) {
								if (c.rigidbody == null) {
										continue; 
								} else {
										c.rigidbody.AddExplosionForce (50f, transform.position, 30, 1, ForceMode.Impulse);
								}
						}
				}
	}
	private void rollComplete()
	{
		isActivated = false; 
	}
}
