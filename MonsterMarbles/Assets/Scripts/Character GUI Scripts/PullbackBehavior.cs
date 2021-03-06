﻿using UnityEngine;
using TouchScript.Gestures;
using System.Collections;
using System;

public class PullbackBehavior : MonoBehaviour {
	
	private Vector2 startPosition;
	/// <summary>
	/// Proportion of y screen space that makes up the active pullback area. 
	/// </summary>
	private float pullbackScreenProportion;
	
	private const float activeClickZonePercentage = 0.1f; //Percentage of Screen height that is an active click zone around the ball's center
	private float maxPullbackZonePercentage = 0.4f; //Percentage of Screen height or width (whichever is lower) beyond which you reach max pullback
	
	private Transform ball;
	
	/// <summary>
	/// Proportion of screen space from the bottom that is not calculated 
	/// for pullback (i.e. setting it at 0.1 means pulling past the bottom ten percent of 
	/// the screen won't affect the pullbackFraction
	/// </summary>
	private float pullbackPercentageOffset = 0.1f;
	public GUITexture pullbackButton;
	private bool pullbackInProgress;
	
	/// <summary>
	/// At what point will the pullback actually fire when released? 
	/// </summary>
	public float noFireFractionCutoff = .05f;
	
	/// <summary>
	/// Amount pullback is stretched back from 0 to 1. 
	/// </summary>
	private float pullbackFraction;
	
	public delegate void pullInformation(float powerFraction);
	public static event pullInformation pullbackInformation;
	
	public delegate void pullBackStatus();
	public static event pullBackStatus pullbackStarted;
	public static event pullBackStatus pullbackAborted;
	public static event pullBackStatus pullbackReleased;
	
	private float previousRotation;
	private Vector2 previousMousePosition;
	private float aimSpeed = 40;
	
	private Quaternion lastLocalRotation;
	private Quaternion localRotationToGo;
	
	public enum State {INACTIVE, ACTIVE, PULLBACK};
	private State currentState;
	
	// Use this for initialization
	void Awake (){
		currentState = State.ACTIVE;
	}
	
	void Start () {
		TurnFlowController.TurnBeginEvent += setCurrentBall;
	}
	
	// Update is called once per frame
	void Update () {
		if(getCurrentState() == State.ACTIVE){
			if(Input.GetMouseButtonDown(0)){
				Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(ball.position);
				int pixelHalfSpace = (int)((Screen.height*activeClickZonePercentage)/2);
				Rect activeClickZone = new Rect(ballScreenPosition.x-pixelHalfSpace, ballScreenPosition.y-pixelHalfSpace, pixelHalfSpace*2,pixelHalfSpace*2);
				if(activeClickZone.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
					setCurrentState(State.PULLBACK);
					previousRotation = 0f;
					previousMousePosition = Input.mousePosition;
				}
			}
		}
		else if (getCurrentState() == State.PULLBACK){
			Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(ball.position);
			int pixelHalfSpace = (int)((Screen.height*activeClickZonePercentage)/2);
			Rect activeClickZone = new Rect(ballScreenPosition.x-pixelHalfSpace, ballScreenPosition.y-pixelHalfSpace, pixelHalfSpace*2,pixelHalfSpace*2);
			
			if(Input.GetMouseButton(0)){
				if(!activeClickZone.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
					/*Vector3 directionVector = ballScreenPosition - Input.mousePosition;
					ball.forward = new Vector3(directionVector.x,ball.forward.z,directionVector.y);
					float distanceFromBall = Vector2.Distance(new Vector2(ballScreenPosition.x,ballScreenPosition.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y));
					float maxPullbackRadius;
					if(Screen.height <= Screen.width){
						maxPullbackRadius = Screen.height * maxPullbackZonePercentage;
					}
					else{
						maxPullbackRadius = Screen.width * maxPullbackZonePercentage;
					}
					
					if(distanceFromBall > maxPullbackRadius){
						distanceFromBall = maxPullbackRadius;
					}
					
					if(pullbackInformation != null){
						pullbackInformation(distanceFromBall/maxPullbackRadius);
					}*/
					/*Vector3 directionVector = ballScreenPosition - Input.mousePosition;
					directionVector = Vector3.Project(directionVector,new Vector3(0,-1,0));
					float distanceFromBall = Vector2.Distance(new Vector2(ballScreenPosition.x,ballScreenPosition.y), new Vector2(directionVector.x, directionVector.y));
					float maxPullbackRadius;
					if(Screen.height <= Screen.width){
						maxPullbackRadius = Screen.height * maxPullbackZonePercentage;
					}
					else{
						maxPullbackRadius = Screen.width * maxPullbackZonePercentage;
					}
					
					if(distanceFromBall > maxPullbackRadius){
						distanceFromBall = maxPullbackRadius;
					}
					
					if(pullbackInformation != null){
						pullbackInformation(distanceFromBall/maxPullbackRadius);
					}*/
					
					pullbackFraction = getPullbackFraction(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
					setRotation(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
					if(pullbackInformation != null){
						pullbackInformation(pullbackFraction);
					}
					if(!Input.GetMouseButton(0)){
						if(pullbackFraction > noFireFractionCutoff){
							if(pullbackReleased != null){
								pullbackReleased();
							}
						}
						else{
							setCurrentState (State.ACTIVE);
							if(pullbackAborted != null){
								pullbackAborted();
							}
						}
					}
				}
			}
			else{
				if(activeClickZone.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){

				}
				else{
					if(pullbackReleased != null){
						pullbackReleased();
					}
				}
				setCurrentState(State.ACTIVE);
			}
		
			/*
			pullbackFraction = getPullbackFraction(new Vector2(Input.mousePosition.x,Input.mousePosition.y));
			if(pullbackInformation != null){
				pullbackInformation(pullbackFraction);
			}
			if(!Input.GetMouseButton(0)){
				if(pullbackFraction > noFireFractionCutoff){
					if(pullbackReleased != null){
						pullbackReleased();
					}
				}
				else{
					setCurrentState (State.ACTIVE);
					if(pullbackAborted != null){
						pullbackAborted();
					}
				}
			}
			*/
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.INACTIVE){
			
		}
		else if (currentState == State.ACTIVE){
			
		}
		else if (currentState == State.PULLBACK){
			
		}
		
		currentState = newState;
		
		if(newState == State.INACTIVE){
			
		}
		else if( newState == State.ACTIVE){

		}
		else if( newState == State.PULLBACK){
			GameCameraController.setCurrentState(GameCameraController.State.STATIC_POSITION);
			if (pullbackStarted != null){
				pullbackStarted();
			}
			startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			pullbackScreenProportion = Input.mousePosition.y/Screen.height;
		}
		
		return true;
	}
	
	void setRotation(Vector2 position){
		/*float rotation = Vector2.Angle(new Vector2(0,-1), startPosition-position);
		Quaternion newRotation = Quaternion.AngleAxis(rotation-previousRotation,Vector3.up);
		//ball.transform.RotateAround(ball.transform.position,Vector3.up,ball.transform.rotation.eulerAngles.z+rotation-previousRotation);
		//ball.transform.Rotate(new Vector3(ball.transform.rotation.eulerAngles.x,ball.transform.rotation.eulerAngles.y,ball.transform.rotation.eulerAngles.z));
		ball.transform.rotation *= newRotation;
		previousRotation = rotation;*/
		
		if(Input.GetMouseButtonDown(0)){
			previousMousePosition = position;
		}
		else if(Input.GetMouseButton(0)){
			float deltaX = previousMousePosition.x - Input.mousePosition.x;
			localRotationToGo=Quaternion.AngleAxis(aimSpeed*deltaX*Time.deltaTime,Vector3.up)*localRotationToGo;
			previousMousePosition = Input.mousePosition;
		}
		
		var fraction = aimSpeed*Time.deltaTime;
		if (ball.transform.localRotation != lastLocalRotation)
		{
			localRotationToGo = ball.transform.localRotation;
		}
		ball.transform.localRotation = lastLocalRotation = Quaternion.Lerp(ball.transform.localRotation, localRotationToGo, fraction);
	}
	
	float getPullbackFraction(Vector2 position){
		float yDistance = startPosition.y - position.y;
		float bottomPullbackAreaOffset = Screen.height * pullbackPercentageOffset;
		float fraction = yDistance/(Screen.height * pullbackScreenProportion - bottomPullbackAreaOffset);
		if (fraction > 1){
			fraction = 1;
		}
		return fraction;
		
	}
	
	public void setCurrentBall(GameObject zoogi){
		ball = zoogi.transform.FindChild("Ball");
	}
	
	public void pullbackButtonTapped(){
		if(getCurrentState() == State.ACTIVE){
			setCurrentState(State.PULLBACK);
		}
	}
	
	void OnEnable() {
		AimPlayerBall.pullbackButtonTapped += pullbackButtonTapped;
		
		//pullbackButton = this.transform.FindChild("Pullback Button").GetComponent<GUITexture>();

	}
	
	void OnDisable() {
		AimPlayerBall.pullbackButtonTapped -= pullbackButtonTapped;
		setCurrentState(State.INACTIVE);
	}
}
