using UnityEngine;
using TouchScript.Gestures;
using System.Collections;
using System;

public class PullbackBehavior : MonoBehaviour {
	
	private Vector2 startPosition;
	/// <summary>
	/// Proportion of y screen space that makes up the active pullback area. 
	/// </summary>
	private float pullbackScreenProportion;
	
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
	
	public enum State {INACTIVE, ACTIVE, PULLBACK};
	private State currentState;
	
	// Use this for initialization
	void Awake (){
		currentState = State.ACTIVE;
	}
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(getCurrentState() == State.ACTIVE){
			
		}
		else if (getCurrentState() == State.PULLBACK){
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
			if (pullbackStarted != null){
				pullbackStarted();
			}
			startPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			pullbackScreenProportion = Input.mousePosition.y/Screen.height;
		}
		
		return true;
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
	}
}
