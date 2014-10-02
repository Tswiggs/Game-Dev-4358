using System;
using TouchScript.Gestures;
using TouchScript.Gestures.Simple;
using TouchScript.Utils;
using UnityEngine;


public class AimPlayerBall : MonoBehaviour {

	public float aimSpeed=1;

	private Quaternion lastLocalRotation;
	private Quaternion localRotationToGo;

	// Use this for initialization
	void Awake () {
		localRotationToGo = lastLocalRotation = transform.localRotation;
	}

	private void OnEnable()
	{
		if (GetComponent<SimplePanGesture>() != null) GetComponent<SimplePanGesture>().StateChanged += panStateChanged;
	}
	
	private void OnDisable()
	{
		if (GetComponent<SimplePanGesture>() != null) GetComponent<SimplePanGesture>().StateChanged -= panStateChanged;
	}

	// Update is called once per frame
	void Update () {
		var fraction = aimSpeed*Time.deltaTime;
		if (transform.localRotation != lastLocalRotation)
		{
			localRotationToGo = transform.localRotation;
		}
		transform.localRotation = lastLocalRotation = Quaternion.Lerp(transform.localRotation, localRotationToGo, fraction);
	}

	private void panStateChanged(object sender, GestureStateChangeEventArgs e)
	{
		switch (e.State)
		{
		case Gesture.GestureState.Began:
		case Gesture.GestureState.Changed:
			var gesture = (SimplePanGesture)sender;
			
			float deltaPos = gesture.LocalDeltaPosition.x;
			if (deltaPos != 0) localRotationToGo.x += deltaPos;
			break;
		}
	}
}
