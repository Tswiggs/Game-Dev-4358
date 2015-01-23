using UnityEngine;
using TouchScript.Gestures;
using System.Collections;
using System;

public class PullTestScript : MonoBehaviour {
	
	
	private Vector2 startPosition;
	
	/// <summary>
	/// Proportion of y screen space that makes up the active pullback area. 
	/// </summary>
	private float pullbackScreenProportion;
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
	
	public delegate void preLaunchAction(float powerFraction);
	public static event preLaunchAction launchActivated;
	public static event preLaunchAction pullbackInformation;
	
	public delegate void pullBackStatus();
	public static event pullBackStatus pullbackStarted;
	public static event pullBackStatus pullbackAborted;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(Input.GetMouseButton(0)){
			if(pullbackInProgress){
				pullBackUpdate(Input.mousePosition);
			
			}
		}
		else{
			if(pullbackInProgress){
				pullBackResolve();
			}
		}
	}
	
	void pullBackActivated(){
		startPosition = pullbackButton.GetScreenRect().center;
		if(pullbackStarted != null){
			pullbackStarted();
		}
		pullbackInProgress = true;
	}
	
	void pullBackUpdate(Vector2 position){
		float yDistance = startPosition.y - position.y;
		pullbackFraction = yDistance/(Screen.height * pullbackScreenProportion);
		if(pullbackInformation != null){
			pullbackInformation(pullbackFraction);
		}
		
	}
	
	void pullBackResolve(){
		if(pullbackInProgress){
			pullbackInProgress = false;
			if(pullbackFraction > noFireFractionCutoff){ 
				if(launchActivated != null){
					launchActivated(pullbackFraction);
				}
			}
			else{
				if(pullbackAborted != null){
					pullbackAborted();
				}
			}
		}
		else{
			if(pullbackAborted != null){
				pullbackAborted();
			}
		}
		
	}
	
	private void OnEnable(){
		
		LaunchController.launchCompleted += disableGUI;
		
		AimPlayerBall.pullbackButtonTapped += pullBackActivated;
		
		pullbackButton = this.transform.FindChild("Pullback Button").GetComponent<GUITexture>();
		pullbackScreenProportion = pullbackButton.GetScreenRect().center.y/Screen.height;

		pullbackInProgress = false;
	}
	
	private void disableGUI(){
		transform.gameObject.SetActive(false);
	}
	
	private void onDisable() {
		LaunchController.launchCompleted -= disableGUI;
	}
	
}
