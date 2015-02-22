﻿using UnityEngine;
using System.Collections;

public class FollowPositionAndRotation : MonoBehaviour {
	public Transform target;
	public float sleepVelocityMagnitude=1;

	
	private bool pullbackInProgress = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position;
		
		if(!pullbackInProgress){
			if (target.rigidbody.velocity.magnitude > sleepVelocityMagnitude)
			{
				transform.rotation=Quaternion.LookRotation(target.rigidbody.velocity);
			}
			else //if(Vector3.Angle(Vector3.up, target.up)<5f)
			{
				/*Vector3 forward=target.forward;
				forward.y=0f;
	
				transform.forward=forward;*/
				
				Vector2 forward1 = new Vector2(target.right.x,target.right.z);
				forward1 = forward1.normalized;
				
				Vector2 forward2 = new Vector2(forward1.y*-1f,forward1.x);
				
				forward1 = new Vector2(forward1.y,forward1.x*-1f);
				
				Vector2 currentForward = new Vector2(transform.forward.x,transform.forward.z);
				
				if(Vector2.Dot(currentForward,forward1) < Vector2.Dot(currentForward,forward2)){
					transform.forward = new Vector3(forward2.x, 0, forward2.y);
				}
				else{
					transform.forward = new Vector3(forward1.x, 0, forward1.y);
				}
			}
		}
	}
	
	void pullbackStarted() {
		pullbackInProgress = true;
	}
	
	void pullbackAborted() {
		pullbackInProgress = false;
	}
	
	void launchActivated( float pullbackFraction) {
		pullbackInProgress = false;
	}
	
	void OnEnable() {
		PullTestScript.pullbackStarted += pullbackStarted;
		PullTestScript.pullbackAborted += pullbackAborted;
		PullTestScript.launchActivated += launchActivated;
	}
	
	void OnDisable() {
		PullTestScript.pullbackStarted -= pullbackStarted;
		PullTestScript.pullbackAborted -= pullbackAborted;
		PullTestScript.launchActivated -= launchActivated;
	}
}
