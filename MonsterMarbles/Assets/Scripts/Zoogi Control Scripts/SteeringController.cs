using UnityEngine;
using System.Collections;

public class SteeringController : MonoBehaviour {

	public Camera camera;
	public float steerStrength=1f;
	public float stopSpeed=1f;
	public float smooth=10f;
	public float hopHeight=100f;
	public float stopBufferCount=3f;
	public float hopLandingYThreshold=.005f;
	public float timeToSpin=1f;

	private Vector3 tilt;
	public bool isRolling=true;
	private bool isHopping=false;
	private Quaternion standingUp;
	private int stopBuffer=0;
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
	}

	
	// Update is called once per frame
	void Update () {
		//if velocity has fallen to near stop make a note of it. This delays the stop effect to allow for
		//better physics play.
		if(stopSpeed>=rigidbody.velocity.magnitude && isRolling)
		{
			stopBuffer++;
		}

		// if velocity has fallen to near stop then stop movement and make character upright.
		if(stopBuffer>stopBufferCount && isRolling)
		{
			isRolling=false;
			isHopping=true;
			rigidbody.Sleep();
			rigidbody.WakeUp();
			standingUp.x=0f;
			standingUp.y=transform.rotation.y;
			standingUp.z=0f;
			if(Quaternion.Angle(transform.rotation, standingUp)>=0.003f)
			{
				rigidbody.AddForce(0f, hopHeight, 0f);
			}
			hopStart=transform.position.y+hopLandingYThreshold;
		}

		//If the ball has started its rotation correction "hop" then slerp the rotation
		if(isHopping){
			transform.rotation =Quaternion.Slerp(transform.rotation, standingUp, timeSpinning/timeToSpin);
			timeSpinning+=Time.deltaTime;

			//If the slerp is within 1 degree of completion activate protocol to complete the turn
			if(timeSpinning>timeToSpin && transform.position.y<=hopStart && rigidbody.velocity.y<=0.01){
				isHopping=false;
				rigidbody.Sleep();
				GetComponent<SteeringController>().enabled= false;
				//GetComponent<LaunchController>().enabled = true;
				if(rollCompleted != null)
				{
					rollCompleted();
				}

			}

		}

		//set tilt to be a vector pointing either right or left of the character and scale the vector
		//by a public float steerStrength and by the current forward velocity.
		if(DeviceType.Handheld==SystemInfo.deviceType){
			tilt.x=Input.acceleration.x*camera.transform.TransformDirection(rigidbody.velocity).y* -steerStrength;
		}else{
			tilt.x=Input.GetAxis("Horizontal")*camera.transform.TransformDirection(rigidbody.velocity).y* -steerStrength/2;
		}
		rigidbody.AddForce(tilt);
	}
	public void forceEndTurn(){
		GetComponent<SteeringController>().enabled= false;
		if(rollCompleted != null){
			rollCompleted();
		}
	}
}
