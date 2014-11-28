using System;
using AssemblyCSharp;
using TouchScript.Gestures;
using TouchScript.Gestures.Simple;
using TouchScript.Utils;
using UnityEngine;


public class AimPlayerBall : MonoBehaviour {
	public float aimSpeed=1;
	public float aimSnappiness=1;
	public float keyAimSpeed=1;
	public SimplePanGesture panGesture;
	public TapGesture reverseButtonTap;
	public PlayerBall playerBall;


	private Quaternion lastLocalRotation;
	private Quaternion localRotationToGo;

	// Use this for initialization
	void Awake () {
		localRotationToGo = lastLocalRotation = transform.localRotation;
	}

	private void OnEnable()
	{
		if (panGesture != null) panGesture.StateChanged += panStateChanged;
		if (reverseButtonTap != null) reverseButtonTap.Tapped += reverseButtonTapped;
		//if (camera.GetComponent<SimplePanGesture>() != null) GetComponent<SimplePanGesture>().StateChanged += panStateChanged;
	}

	private void OnDisable()
	{
		if (panGesture != null) panGesture.StateChanged -= panStateChanged;
		if (reverseButtonTap != null) reverseButtonTap.Tapped -= reverseButtonTapped;
	}

	// Update is called once per frame
	void Update () {
		var fraction = aimSpeed*Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.A)){
			localRotationToGo=Quaternion.AngleAxis(-keyAimSpeed,Vector3.up)*localRotationToGo;
		}
		if(Input.GetKeyDown(KeyCode.D)){
			localRotationToGo=Quaternion.AngleAxis(keyAimSpeed,Vector3.up)*localRotationToGo;
		}
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

			if (Math.Abs(gesture.LocalDeltaPosition.x) > 0.01)
			{
				if (transform.parent == null)
				{
					localRotationToGo = Quaternion.AngleAxis(gesture.LocalDeltaPosition.x * aimSpeed, Vector3.up) * localRotationToGo ;
				} else
				{
					localRotationToGo = Quaternion.AngleAxis(gesture.LocalDeltaPosition.x * aimSpeed, transform.parent.InverseTransformDirection(Vector3.up)) * localRotationToGo;
				}
			}

//			float deltaPos = gesture.LocalDeltaPosition.x;
//			if (deltaPos != 0) localRotationToGo.y += deltaPos;
			break;
		}
	}

	private void reverseButtonTapped(object sender, EventArgs e)
	{
		if (transform.parent == null)
		{
			localRotationToGo = Quaternion.AngleAxis(179.999f, Vector3.up) * localRotationToGo ;
		} else
		{
			localRotationToGo = Quaternion.AngleAxis(179.999f, transform.parent.InverseTransformDirection(Vector3.up)) * localRotationToGo;
		}
	}
}
