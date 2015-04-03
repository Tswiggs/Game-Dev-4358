using UnityEngine;
using System.Collections;

public class LarsPower : ZoogiPower {


	public GameObject waterTrailParticles;
	
	public GameObject LarsBall;
	
	public AudioClip larsJumpSound;
	
	private bool isActivated = false;
	
	private float velocityIncrease = 2;
	// Use this for initialization
	void Start () {
		LarsBall = transform.FindChild("Ball").gameObject;
		larsJumpSound = Resources.Load("lars_jump_sound") as AudioClip;
		TurnFlowController.TurnBeginEvent += turnStarted;
		//powerCharged = true;
	}
	
	public void turnStarted(GameObject zoogi){
		powerCharged = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<ZoogiController>().getCurrentState() == ZoogiController.State.ROLLING){
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
				if(powerCharged){
					leapUp();
				}
				powerCharged = false;
			}
		}

	}
	
	public void leapUp(){
		GameAudioController.playOneShotSound(larsJumpSound);
		LarsBall.GetComponent<Rigidbody>().AddForce(new Vector3(0,100f,0),ForceMode.Impulse);
	}
	
	override public bool deployPower(){
		if(powerCharged){
			powerCharged = false;
			readyToDeployPower = false;
			useSharkBite ();
			return true;
		}
		
		return false;
	}
	
	public bool useSharkBite(){
		if(LarsBall.GetComponent<SteeringController>().enabled){
			powerCharged = false;
			isActivated = true;
			waterTrailParticles.SetActive(true);
			MarbleCollisionHandler.playerHasCollided += sharkBite;
			SteeringController.rollCompleted += rollComplete;
			return true;
			
		}
		
		return false;
	}
	
	private void rollComplete()
	{
		waterTrailParticles.SetActive(false);
		isActivated = false;
		MarbleCollisionHandler.playerHasCollided -= sharkBite;
		SteeringController.rollCompleted -= rollComplete;
	}
	
	
	public void sharkBite(Collision collision, Rigidbody original){
		
		if(LarsBall.GetComponent<Rigidbody>() == original){
			if(collision.collider.GetComponent<Rigidbody>() != null){
				Vector3 colliderVelocity = collision.collider.GetComponent<Rigidbody>().velocity;
				
				Vector3 a = LarsBall.GetComponent<Rigidbody>().position;
				Vector3 b = collision.collider.GetComponent<Rigidbody>().position;
				
				collision.collider.GetComponent<Rigidbody>().velocity += (b-a).normalized * LarsBall.GetComponent<Rigidbody>().velocity.magnitude * velocityIncrease;
			}
		}
	}
}
