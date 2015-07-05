using System;
using AssemblyCSharp;
using TouchScript.Gestures;
using TouchScript.Gestures.Simple;
using TouchScript.Utils;
using UnityEngine;


public class AimPlayerBall : MonoBehaviour {
	public float aimSpeed=1;
	public float aimSnappiness=4;
	public float keyAimSpeed=1;
	public SimplePanGesture panGesture;
	public PressGesture beginPullbackPress;
	public PlayerBall playerBall;
	
	public delegate void buttonTapped();
	public static event buttonTapped pullbackButtonTapped;
	public static event buttonTapped rotateButtonTapped;
	public static event buttonTapped rotateButtonReleased;

	public Vector3 previousMousePosition;

	private Quaternion lastLocalRotation;
	private Quaternion localRotationToGo;

	// Use this for initialization
	void Awake () {
		localRotationToGo = lastLocalRotation = transform.localRotation;
		previousMousePosition = new Vector3();
	}

	private void OnEnable()
	{
		if (panGesture != null) panGesture.StateChanged += panStateChanged;
		if (beginPullbackPress != null) beginPullbackPress.Pressed += beginPullbackTapped;
		//if (camera.GetComponent<SimplePanGesture>() != null) GetComponent<SimplePanGesture>().StateChanged += panStateChanged;
	}

	private void OnDisable()
	{
		if (panGesture != null) panGesture.StateChanged -= panStateChanged;
		if (beginPullbackPress != null) beginPullbackPress.Pressed -= beginPullbackTapped;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			previousMousePosition = Input.mousePosition;
		}
		else if(Input.GetMouseButton(0)){
			float deltaX = Input.mousePosition.x - previousMousePosition.x;
			localRotationToGo=Quaternion.AngleAxis(aimSpeed*deltaX*Time.deltaTime,Vector3.up)*localRotationToGo;
			previousMousePosition = Input.mousePosition;
		}
	
		var fraction = aimSpeed*Time.deltaTime;
		if(Input.GetKey(KeyCode.A)){
			localRotationToGo=Quaternion.AngleAxis(-keyAimSpeed*Time.deltaTime,Vector3.up)*localRotationToGo;
		}
		if(Input.GetKey(KeyCode.D)){
			localRotationToGo=Quaternion.AngleAxis(keyAimSpeed*Time.deltaTime,Vector3.up)*localRotationToGo;
		}
		if (transform.localRotation != lastLocalRotation)
		{
			localRotationToGo = transform.localRotation;
		}
		transform.localRotation = lastLocalRotation = Quaternion.Lerp(transform.localRotation, localRotationToGo, fraction);
	}
	
	void FixedUpdate(){
		if(Input.GetKey(KeyCode.A)){
			localRotationToGo=Quaternion.AngleAxis(-keyAimSpeed,Vector3.up)*localRotationToGo;
		}
		if(Input.GetKey(KeyCode.D)){
			localRotationToGo=Quaternion.AngleAxis(keyAimSpeed,Vector3.up)*localRotationToGo;
		}
	}

	private void panStateChanged(object sender, GestureStateChangeEventArgs e)
	{
		switch (e.State)
		{
		case Gesture.GestureState.Began:
			if(rotateButtonTapped != null){
				rotateButtonTapped();
			}
			break;
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
		case Gesture.GestureState.Ended:
			if(rotateButtonReleased != null){
				rotateButtonReleased();
			}
			break;
		}
	}

	private void beginPullbackTapped(object sender, EventArgs e)
	{
		if(pullbackButtonTapped != null){
			pullbackButtonTapped();
		}
	}
}
