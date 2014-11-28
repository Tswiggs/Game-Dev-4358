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
	public GUITexture powerLevel;
	private bool pullbackInProgress;
	
	/// <summary>
	/// At what point will the pullback actually fire when released? 
	/// </summary>
	public float noFireFractionCutoff = .05f;
	
	/// <summary>
	/// Amount pullback is stretched back, out of total potential stretch. 
	/// </summary>
	private float pullbackFraction;
	
	public delegate void preLaunchAction(float powerFraction);
	public static event preLaunchAction launchActivated;
	public static event preLaunchAction launchInformation;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*for(int x  =0; x  < Input.touchCount; x++){
			if(Input.GetTouch(x).phase == TouchPhase.Began){
				Vector2 touchPosition = Input.GetTouch(x).position;
				Rect buttonBox = this.GetComponent<GUITexture>().pixelInset;
				buttonBox.x += Screen.width*transform.localPosition.x;
				buttonBox.y += Screen.height*transform.localPosition.y;
				if (buttonBox.Contains(touchPosition))
				{
					this.GetComponent<GUITexture>().guiTexture.color= Color.green;
					Debug.Log("WE PUSHED THAT BUTTON!");
				}
			}
		}*/
		if(Input.GetMouseButtonDown(0)) {
			if(pullbackButton.GetScreenRect().Contains(Input.mousePosition)){
				pullBackActivated();
			}
		}
		else if(Input.GetMouseButton(0)){
			if(pullbackInProgress){
				pullBackUpdate(Input.mousePosition);
			
			}
		}
		else{
			pullBackResolve();
		}
	}
	
	void pullBackActivated(){
		startPosition = pullbackButton.GetScreenRect().center;
		pullbackInProgress = true;
	}
	
	void pullBackUpdate(Vector2 position){
		float yDistance = startPosition.y - position.y;
		pullbackFraction = yDistance/(Screen.height * pullbackScreenProportion);
		float colorBleed = 1 - pullbackFraction;
		powerLevel.pixelInset = new Rect(powerLevel.pixelInset.x,powerLevel.pixelInset.y,powerLevel.pixelInset.width,-yDistance); 
		pullbackButton.color = new Color(colorBleed,colorBleed,colorBleed);
		launchInformation(pullbackFraction);
		
	}
	
	void pullBackResolve(){
		if(pullbackInProgress){
			pullbackButton.color = new Color(1,1,1);
			powerLevel.pixelInset = new Rect(powerLevel.pixelInset.x,powerLevel.pixelInset.y,powerLevel.pixelInset.width,0); 
			pullbackInProgress = false;
			if(pullbackFraction > noFireFractionCutoff){ 
				launchActivated(pullbackFraction);
			}
		}
		
	}
	
	private void OnEnable(){
		
		LaunchController.launchCompleted += disableGUI;
		
		pullbackButton = this.transform.FindChild("Pullback Button").GetComponent<GUITexture>();
		powerLevel = this.transform.FindChild ("Power Level").GetComponent<GUITexture>();
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
