using UnityEngine;
using System.Collections;
using System;
using TouchScript.Gestures;

public class HotStreakPower : ZoogiPower {


	public GameObject HotStreakBall;
	
	public GameObject groundSlamParticles;
	
	public AudioClip groundSlamSound;
	
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
		powerCharged = true;
	}
	
	override public bool deployPower(){
		if(powerCharged){
			powerCharged = false;
			readyToDeployPower = false;
			MarbleCollisionHandler.playerHasCollided += checkForGroundSlam;
			
			return true;
		}
		return false;
	}
	
	private void checkForGroundSlam(Collision collision, Rigidbody original){
		if(HotStreakBall.GetComponent<Rigidbody>() == original){
			if(collision.collider.GetComponent<Rigidbody>() != null){
				groundSlam();
				isActivated = false;
				MarbleCollisionHandler.playerHasCollided -= checkForGroundSlam;
			}
		}
	}
	
	private void groundSlam()
	{
		if (isActivated == false /*&& (GetComponent<SteeringController>().isRolling == true)*/) {
			isActivated = true;
						HotStreakBall.GetComponent<AudioSource>().PlayOneShot(groundSlamSound);
						Collider[] colliders = Physics.OverlapSphere (HotStreakBall.transform.position, slamRadius);
						if(groundSlamParticles != null){
							GameObject slamParticles = Instantiate(groundSlamParticles,HotStreakBall.transform.position, HotStreakBall.transform.rotation) as GameObject;
							slamParticles.transform.parent = HotStreakBall.transform.FindChild ("Character Root");
							SphereCollider ballCollider = HotStreakBall.GetComponent<Collider>() as SphereCollider;
							slamParticles.transform.localPosition = new Vector3(slamParticles.transform.localPosition.x, slamParticles.transform.localPosition.y - ballCollider.radius, slamParticles.transform.localPosition.z);
                    	}
						foreach (Collider c in colliders) {
								if (c.GetComponent<Rigidbody>() == null) {
										continue; 
								} else {
									if( c.gameObject.Equals(HotStreakBall)){
										continue;
									}
									else if (c.gameObject.CompareTag(Constants.TAG_MARBLE) || c.gameObject.CompareTag(Constants.TAG_PLAYER)){
										c.GetComponent<Rigidbody>().AddExplosionForce (slamPower, HotStreakBall.transform.position, slamRadius*2, 0, ForceMode.Impulse);
									}	
								}
						}
				}
	}
	
	void OnDrawGizmos(){
		
	}
	
	private void rollComplete()
	{
		isActivated = false;
		MarbleCollisionHandler.playerHasCollided -= checkForGroundSlam; 
	}
}
