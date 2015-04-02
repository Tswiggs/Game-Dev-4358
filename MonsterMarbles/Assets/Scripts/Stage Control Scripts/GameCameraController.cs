using UnityEngine;
using System;
using System.Collections;

public class GameCameraController : MonoBehaviour {
	
	public Transform target;
	
	private static GameCameraController cameraController;
	
	public FollowCameraBehavior followCamera;
	
	public enum State{ ROTATE_ABOUT_CLOSE, ROTATE_ABOUT_FAR, FOLLOW_FAR, FOLLOW_CLOSE, BIRD_EYE_PAN, STATIC_POSITION};
	private State currentState;
	
	void Start(){
		cameraController = this;
		currentState = State.STATIC_POSITION;
		
		followCamera = gameObject.GetComponent<FollowCameraBehavior>();
		
		TurnFlowController.TurnBeginEvent += turnBegin;
	}

	void turnBegin(GameObject Zoogi)
	{
		setFocusTarget(Zoogi.transform.FindChild ("Character Root"));
		setCurrentState(State.FOLLOW_CLOSE);
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public static bool setCurrentState(State newState){
		
		if(cameraController.currentState == State.FOLLOW_CLOSE){
			cameraController.followCamera.enabled = false;
		}
		else if(cameraController.currentState == State.FOLLOW_FAR){
			cameraController.followCamera.enabled = false;
		}
		
		cameraController.currentState = newState;
		
		if(newState == State.FOLLOW_CLOSE){
			cameraController.followCamera.enabled = true;
			cameraController.followCamera.setCurrentState(FollowCameraBehavior.State.CLOSE);
		}
		else if(newState == State.FOLLOW_FAR){
			cameraController.followCamera.enabled = true;
			cameraController.followCamera.setCurrentState(FollowCameraBehavior.State.FAR);
		}
		
		return true;
	}
	
	public static bool setFocusTarget(Transform focusTarget){
		cameraController.target = focusTarget;
		cameraController.SendMessage("setFocusTarget", focusTarget, SendMessageOptions.DontRequireReceiver);
		return true;
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
}
