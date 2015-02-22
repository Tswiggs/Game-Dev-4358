﻿using UnityEngine;
using System.Collections;

public class ArrowPullbackController : MonoBehaviour {
	
	public float zScaleFactor;
	public float xScaleFactor;
	
	public GameObject directionalArrow;
	
	
	private bool pullbackInProgress = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void pullbackStarted(){
		pullbackInProgress = true;
		
		PullTestScript.pullbackInformation += pullbackInformation;
		PullTestScript.pullbackAborted += pullbackAborted;
		PullTestScript.pullbackStarted -= pullbackStarted;
		PullTestScript.launchActivated += pullbackSucceeded;
		
	}
	
	void pullbackInformation(float pullbackFraction){
		this.transform.localScale = new Vector3(1f+pullbackFraction*xScaleFactor,this.transform.localScale.y,1f+pullbackFraction*zScaleFactor);
		directionalArrow.renderer.material.color = new Color(1f,1f-pullbackFraction,1f-pullbackFraction,1f);
	}
	
	void pullbackSucceeded(float pullbackFraction) {
	
		pullbackInProgress = false;
		
		this.transform.localScale = new Vector3(1,this.transform.localScale.y,1);
		
		directionalArrow.renderer.material.color = Color.white;
		
		PullTestScript.pullbackInformation -= pullbackInformation;
		PullTestScript.pullbackAborted -= pullbackAborted;
		PullTestScript.pullbackStarted += pullbackStarted;
		PullTestScript.launchActivated -= pullbackSucceeded;
	}
	
	void pullbackAborted(){
		
		pullbackInProgress = false;
		
		this.transform.localScale = new Vector3(1,this.transform.localScale.y,1);
		
		directionalArrow.renderer.material.color = Color.white;
	
		PullTestScript.pullbackInformation -= pullbackInformation;
		PullTestScript.pullbackAborted -= pullbackAborted;
		PullTestScript.pullbackStarted += pullbackStarted;
		PullTestScript.launchActivated -= pullbackSucceeded;
	}
	
	private void OnEnable(){
		
		PullTestScript.pullbackStarted += pullbackStarted;
		
		
	}
	
	private void OnDisable(){
		
		PullTestScript.pullbackAborted -= pullbackAborted;
	}
}
