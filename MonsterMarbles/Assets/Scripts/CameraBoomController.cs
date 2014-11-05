using UnityEngine;
using System;
using System.Collections;

public class CameraBoomController : MonoBehaviour {
	public Transform target;
	public Transform defaultTarget;
	public SmoothFollowCSharp followScript;
	public LaunchCameraController launchScript;

	void OnStart(){
		//followScript=gameObject.GetComponent<SmoothFollowCSharp>();
		//launchScript=gameObject.GetComponent<LaunchCameraController>();
	}

	void OnEnable(){
		LaunchController.launchCompleted += switchToFollow;
		SteeringController.rollCompleted += rollCompleteAction;
	}

	void OnDisable(){
		LaunchController.launchCompleted -= switchToFollow;
		SteeringController.rollCompleted += rollCompleteAction;
	}

	void switchToFollow()
	{
		launchScript.enabled=false;
		followScript.enabled=true;
		followScript.target=target;
	}
	void rollCompleteAction()
	{
		followScript.enabled=false;
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
		launchScript.enabled=true;
	}


	// Update is called once per frame
	void Update () {
		
	}


}
