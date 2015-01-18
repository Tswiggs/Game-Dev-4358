using UnityEngine;
using System.Collections;

public class LarsPower : ZoogiPower {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown("space")){
			activatePower ();
		}

	}
	
	override public bool activatePower(){
		if(powerReady){
			powerReady = false;
			
			
			return true;
		}
		else{
			return false;
		}
	}
}
