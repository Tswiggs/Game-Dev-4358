﻿using UnityEngine;
using System.Collections;

public class LarsPower : ZoogiPower {


	public GameObject waterTrailParticles;
	
	public GameObject LarsBall;
	
	private bool isActivated = false;
	
	private float velocityIncrease = 2;
	// Use this for initialization
	void Start () {
		LarsBall = transform.FindChild("Ball").gameObject;
		//powerCharged = true;
	}
	
	
	// Update is called once per frame
	void Update () {
		powerCharged = true;
		if(Input.GetKeyDown("space")){
			deployPower();
		}

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