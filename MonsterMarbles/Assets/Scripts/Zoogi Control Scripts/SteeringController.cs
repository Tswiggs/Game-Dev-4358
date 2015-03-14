using UnityEngine;
using System.Collections;

public class SteeringController : MonoBehaviour {

	public Camera camera;
	private float stopSpeed=1f;
	public float smooth=10f;
	public float hopHeight=100f;
	private float stopBufferCount=1.5f;
	public float hopLandingYThreshold=.005f;
	private float timeToSpin=1.5f;

	private Vector3 tilt;
	public bool isRolling=true;
	private bool isHopping=false;
	private Quaternion standingUp;
	private float stopBuffer=0;
	private float hopStart= 0f;
	private float timeSpinning= 0f;


	public delegate void postRollAction();
	public static event postRollAction rollCompleted;

	void OnStart(){
		camera=Camera.main;
	}

	// Use this for initialization
	void OnEnable() {
		camera=Camera.main;
		tilt=new Vector3(0f, 0f, 0f);
		standingUp=new Quaternion();
		timeSpinning = 0;
		isRolling=true;
		isHopping = false;
		stopBuffer=0;
		hopStart = 0;
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	}

	
	// Update is called once per frame
	void Update () {
		//if velocity has fallen to near stop make a note of it. This delays the stop effect to allow for
		//better physics play.
		if(stopSpeed>=GetComponent<Rigidbody>().velocity.magnitude && isRolling)
		{
			stopBuffer+= Time.deltaTime;
		}

		// if velocity has fallen to near stop then stop movement and make character upright.
		if(stopBuffer>stopBufferCount && isRolling)
		{
			isRolling=false;
			isHopping=true;
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			standingUp.x=0f;
			standingUp.y=transform.rotation.y;
			standingUp.z=0f;
			if(Quaternion.Angle(transform.rotation, standingUp)>=0.003f)
			{
				GetComponent<Rigidbody>().AddForce(0f, hopHeight, 0f);
			}
			hopStart=transform.position.y+hopLandingYThreshold;
		}

		//If the ball has started its rotation correction "hop" then slerp the rotation
		if(isHopping){
			transform.rotation =Quaternion.Slerp(transform.rotation, standingUp, timeSpinning/timeToSpin);
			timeSpinning+=Time.deltaTime;

			//If the slerp is within 1 degree of completion activate protocol to complete the turn
			if(timeSpinning>timeToSpin){
				isHopping=false;
				
				endTurn();

			}

		}

		//set tilt to be a vector pointing either right or left of the character and scale the vector
		//by a public float steerStrength and by the current forward velocity.
		/*if(DeviceType.Handheld==SystemInfo.deviceType){
			tilt.x=Input.acceleration.x*camera.transform.TransformDirection(GetComponent<Rigidbody>().velocity).y* -steerStrength;
		}else{
			tilt.x=Input.GetAxis("Horizontal")*camera.transform.TransformDirection(GetComponent<Rigidbody>().velocity).y* -steerStrength/2;
		}
		GetComponent<Rigidbody>().AddForce(tilt);*/
	}
	public void forceEndTurn(){
		endTurn();
	}
	
	private void endTurn() {
		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		GetComponent<SteeringController>().enabled= false;
		if(rollCompleted != null)
		{
			rollCompleted();
		}
	}
}
