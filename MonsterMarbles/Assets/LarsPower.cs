using UnityEngine;
using System.Collections;

public class LarsPower : ZoogiPower {


	public GameObject waterTrailParticles;
	
	public GameObject LarsBall;
	
	private bool isActivated = false;
	
	private float velocityIncrease = 15;
	// Use this for initialization
	void Start () {
		LarsBall = transform.FindChild("Ball").gameObject;
		powerReady = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("space")){
			activatePower ();
		}

	}
	
	override public bool activatePower(){
		if(powerReady){
			if(LarsBall.GetComponent<SteeringController>().enabled){
				powerReady = false;
				sharkBite();
				SteeringController.rollCompleted += rollComplete;
				return true;
			}
			
			
		}
		
		return false;
	}
	
	private void rollComplete()
	{
		if(isActivated){
			waterTrailParticles.SetActive(false);
			isActivated = false;
			SteeringController.rollCompleted -= rollComplete;
		}
	}
	
	public void sharkBite(){
		isActivated = true;
		waterTrailParticles.SetActive(true);
		LarsBall.rigidbody.velocity = LarsBall.rigidbody.velocity.normalized * (LarsBall.rigidbody.velocity.magnitude+velocityIncrease);
	}
}
