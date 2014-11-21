using UnityEngine;
using System.Collections;
using System;
using TouchScript.Gestures;

public class HotStreakPower : MonoBehaviour {

	/// <summary>
	/// the original wolf gang ball. Point this to Wolfgang's "Ball" object. 
	/// </summary>
	public GameObject HotStreakBall;
	
	public GameObject groundSlamParticles;
	
	public float slamRadius = 20f;
	public float slamPower = 75f;
	
	private bool isActivated = false;
	void Start () {
		HotStreakBall = transform.FindChild("Ball").gameObject;
		//HotStreakBall.GetComponent<TapGesture> ().Tapped += groundSlam;
		SteeringController.rollCompleted += rollComplete;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown("space")){
			groundSlam(this, new EventArgs());
		}
	
	}

	private void groundSlam(object sender, EventArgs e)
	{
		if (isActivated == false /*&& (GetComponent<SteeringController>().isRolling == true)*/) {
			isActivated = true;
						Collider[] colliders = Physics.OverlapSphere (HotStreakBall.transform.position, slamRadius);
						if(groundSlamParticles != null){
							GameObject slamParticles = Instantiate(groundSlamParticles,HotStreakBall.transform.position, HotStreakBall.transform.rotation) as GameObject;
							slamParticles.transform.parent = HotStreakBall.transform.FindChild ("Character Root");
							SphereCollider ballCollider = HotStreakBall.collider as SphereCollider;
							slamParticles.transform.localPosition = new Vector3(slamParticles.transform.localPosition.x, slamParticles.transform.localPosition.y - ballCollider.radius, slamParticles.transform.localPosition.z);
                    	}
						foreach (Collider c in colliders) {
								if (c.rigidbody == null) {
										continue; 
								} else {
									if(!c.gameObject.CompareTag(Constants.TAG_MARBLE) || c.gameObject.Equals(HotStreakBall)){
										continue;
									}
									else {
										c.rigidbody.AddExplosionForce (slamPower, HotStreakBall.transform.position, slamRadius*2, 0, ForceMode.Impulse);
									}
								}
						}
				}
	}
	
	void OnDrawGizmos(){
		if(true){
			Gizmos.DrawWireSphere(HotStreakBall.transform.position,slamRadius); 
		}
	}
	
	private void rollComplete()
	{
		isActivated = false; 
	}
}
