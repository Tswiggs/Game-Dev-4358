using UnityEngine;
using System;

public class CameraBoomController : MonoBehaviour {
	//public GameObject target;

	void OnEnable(){
		LaunchController.launchCompleted += switchToFollow;
	}

	void OnDisable(){
		LaunchController.launchCompleted -= switchToFollow;
	}

	void switchToFollow()
	{
		gameObject.GetComponent<SmoothFollowCSharp>().enabled=true;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
