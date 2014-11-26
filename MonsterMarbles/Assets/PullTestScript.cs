using UnityEngine;
using TouchScript.Gestures;
using System.Collections;
using System;

public class PullTestScript : MonoBehaviour {
	
	
	private Vector2 startPosition;
	
	private GUIText debugText;
	
	// Use this for initialization
	void Start () {
		startPosition = new Vector2(0,0);
		GetComponent<LongPressGesture>().LongPressed += pressHandler;
		GetComponent<PanGesture>().PanStarted += panStartedHandler;
		GetComponent<PanGesture>().Panned += pannedHandler;
		GetComponent<PanGesture>().PanCompleted += panCompletedHandler;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnEnable(){
		GetComponent<LongPressGesture>().LongPressed += pressHandler;
		GetComponent<PanGesture>().PanStarted += panStartedHandler;
		GetComponent<PanGesture>().Panned += pannedHandler;
		GetComponent<PanGesture>().PanCompleted += panCompletedHandler;
	}
	
	private void onDisable() {
		
	}
	
	private void pressHandler(object sender, EventArgs e)
	{
			this.transform.guiText.text = "Pressed Motherfucker";
			Destroy(this);
	}
	
	private void panStartedHandler(object sender, EventArgs e)
	{
		startPosition = GetComponent<PanGesture>().ScreenPosition;
		Debug.Log(startPosition.y);
	}
	
	private void pannedHandler(object sender, EventArgs e)
	{
		
	}
	
	private void panCompletedHandler(object sender, EventArgs e)
	{
		
	}
}
