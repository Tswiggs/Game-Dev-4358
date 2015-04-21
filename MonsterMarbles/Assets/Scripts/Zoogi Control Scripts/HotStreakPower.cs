using UnityEngine;
using System.Collections;
using System;
using TouchScript.Gestures;

public class HotStreakPower : ZoogiPower {


	public GameObject HotStreakBall;
	
	public GameObject groundSlamParticles;
	
	public AudioClip groundSlamSound;
	
	public float slamRadius = 18f;
	public float slamPower = 100f;
	
	private bool isActivated = false;
	void Start () {
		HotStreakBall = transform.FindChild("Ball").gameObject;
		//HotStreakBall.GetComponent<TapGesture> ().Tapped += groundSlam;
		SteeringController.rollCompleted += rollComplete;
		groundSlamParticles = Resources.Load("Ground Slam Particles") as GameObject;
		groundSlamSound = Resources.Load ("Hot streak-heavy-blast") as AudioClip;
		
		TurnFlowController.TurnBeginEvent += turnStarted;
	}
	
	public void turnStarted(GameObject zoogi){
		powerCharged = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GetComponent<ZoogiController>().getCurrentState() == ZoogiController.State.ROLLING){
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
				if(powerCharged){
					groundSlam();
				}
				powerCharged = false;
			}
		}
	}
	
	void groundSlamAndStop(){

		isActivated = true;
		GameAudioController.playOneShotSound(groundSlamSound);
		Collider[] colliders = Physics.OverlapSphere (HotStreakBall.transform.position, slamRadius);
		if(groundSlamParticles != null){
			GameObject slamParticles = Instantiate(groundSlamParticles,HotStreakBall.transform.position,Quaternion.identity) as GameObject;
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
				else if (c.gameObject.CompareTag(Constants.TAG_MARBLE) || c.gameObject.CompareTag(Constants.TAG_PLAYER) || c.gameObject.CompareTag(Constants.TAG_ENEMY)){
					if(c.gameObject.GetComponent<SkybitBehavior>() != null){
						c.gameObject.GetComponent<SkybitBehavior>().setCurrentState(SkybitBehavior.State.FALLING);
					}
					c.GetComponent<Rigidbody>().AddExplosionForce (slamPower, HotStreakBall.transform.position, slamRadius*2, 0, ForceMode.Impulse);
				}	
			}
		}
		
		transform.FindChild ("Ball").gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
		isActivated = true;
		GameAudioController.playOneShotSound(groundSlamSound);
		Collider[] colliders = Physics.OverlapSphere (HotStreakBall.transform.position, slamRadius);
		if(groundSlamParticles != null){
			GameObject slamParticles = Instantiate(groundSlamParticles,HotStreakBall.transform.position,Quaternion.identity) as GameObject;
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
				else if (c.gameObject.CompareTag(Constants.TAG_MARBLE) || c.gameObject.CompareTag(Constants.TAG_PLAYER) || c.gameObject.CompareTag(Constants.TAG_ENEMY)){
					if(c.gameObject.GetComponent<SkybitBehavior>() != null){
						c.gameObject.GetComponent<SkybitBehavior>().setCurrentState(SkybitBehavior.State.FALLING);
					}
					c.GetComponent<Rigidbody>().AddExplosionForce (slamPower, HotStreakBall.transform.position, slamRadius*2, 0, ForceMode.Impulse);
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
