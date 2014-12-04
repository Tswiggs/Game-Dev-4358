using UnityEngine;
using System;
using System.Collections;

public class CameraBoomController : MonoBehaviour {
	public Transform target;
	public Transform defaultTarget;
	public SmoothFollowCSharp followScript;
	public LaunchCameraController launchScript;
	
	public enum CameraSetting{
		ROTATE_STATE,PULLBACK_STATE,FOLLOW_BALL_STATE,WAIT_STATE
	};
	
	public CameraSetting cameraState;

	void Start(){
		//followScript=gameObject.GetComponent<SmoothFollowCSharp>();
		//launchScript=gameObject.GetComponent<LaunchCameraController>();
	}

	void OnEnable(){
		PullTestScript.pullbackStarted += switchToPullback;
		PullTestScript.pullbackAborted += switchToRotate;
		LaunchController.launchCompleted += switchToFollow;
		SteeringController.rollCompleted += rollCompleteAction;
	}

	void OnDisable(){
		PullTestScript.pullbackStarted -= switchToPullback;
		LaunchController.launchCompleted -= switchToFollow;
		SteeringController.rollCompleted -= rollCompleteAction;
	}
	
	void changeCameraState(CameraSetting setting){
		cameraState = setting;
		
		switch(cameraState){
			case CameraSetting.ROTATE_STATE:
				launchScript.enabled=true;
				followScript.enabled=false;
				break;
			
			case CameraSetting.PULLBACK_STATE:
				launchScript.enabled=false;
				followScript.enabled=true;
				followScript.target=target;
				break;
			
			case CameraSetting.FOLLOW_BALL_STATE:
				launchScript.enabled=false;
				followScript.enabled=true;
				followScript.target=target;
				break;
			
			case CameraSetting.WAIT_STATE:
				break;
		
		}
	}

	public void switchToFollow()
	{
		changeCameraState(CameraSetting.FOLLOW_BALL_STATE);
	}
	
	public void switchToRotate(){
		changeCameraState(CameraSetting.ROTATE_STATE);
	}
	
	public void switchToPullback(){
		changeCameraState(CameraSetting.PULLBACK_STATE);
	}
	
	public void rollCompleteAction()
	{
		followScript.enabled=false;
		changeCameraState(CameraSetting.WAIT_STATE);
		StartCoroutine(delayEndOfTurn());
		waitForTurn();
	}
	IEnumerator delayEndOfTurn(){
		yield return new WaitForSeconds(3);
	}

	void waitForTurn(){
		launchScript.target=defaultTarget;
		launchScript.enabled=true;
	}
	public void startOfTurn(Transform target){
		this.target=target;
		launchScript.target=target;
		changeCameraState(CameraSetting.ROTATE_STATE);
	}


	// Update is called once per frame
	void Update () {
		
	}


}
