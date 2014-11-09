using UnityEngine;
using TouchScript.Gestures;
using System;

public class LaunchController : MonoBehaviour {

	new public Camera camera;
	public AudioSource audioSource;
	public AudioClip explosionSound; 
	public GameObject characterGui;
	public GameObject root;
	public float speed=1;
	public float launchScalar=1;
	public float launchSpin=1;
	public float maxPower=100;
	public float powerFade=1;
	public delegate void postLaunchAction();
	public static event postLaunchAction launchCompleted;

	//Variables used for the correctional hop
	private Quaternion standingUp=new Quaternion();
	private bool isHopping =false;
	private float hopStart= 0f;
	private float timeSpinning= 0f;
	public float hopLandingYThreshold=.00005f;
	public float timeToSpin=1f;
	public float hopHeight=3000f;




	private bool shouldLaunch=false;
	private float power=0;
	// Use this for initialization
	void Start () {
		camera=Camera.main;
		audioSource=camera.GetComponent<AudioSource>();
	}


	// Update is called once per frame
	void Update () {
		power=Mathf.Clamp(power, 0f, maxPower);
		transform.Rotate(power,0f,0f);
		if(shouldLaunch)
		{
			Vector3 launchVector= new Vector3();
			launchVector=(transform.position-camera.transform.position)*power*launchScalar;
			launchVector.y=0f;
			rigidbody.AddForce(launchVector);
			rigidbody.AddRelativeTorque(power*launchSpin, 0f, 0f);
			audioSource.PlayOneShot(explosionSound);
			shouldLaunch=false;
			characterGui.SetActive(false);
			GetComponent<SteeringController>().enabled=true;
			GetComponent<LaunchController>().enabled=false;
			camera=Camera.main;

			if(launchCompleted != null)
			{
				launchCompleted();
			}
		}
		if(isHopping){
			transform.rotation =Quaternion.Slerp(transform.rotation, standingUp, timeSpinning/timeToSpin);
			timeSpinning+=Time.deltaTime;
			
			//If the slerp is within 1 degree of completion activate protocol to complete the turn
			if(timeSpinning>timeToSpin && transform.position.y<hopStart && rigidbody.velocity.y<=0){
				isHopping=false;
				characterGui.SetActive(true);
				rigidbody.Sleep();
				GetComponent<SteeringController>().enabled= false;
				GetComponent<LaunchController>().enabled = true;
							
			}
		}
	}

	public void hopUpright(){
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


	private void OnEnable()
	{
		if(transform.up!=root.transform.up){hopUpright();}
		else{characterGui.SetActive(true);}
		power=0f;
		GetComponent<TapGesture>().Tapped += pressHandler;
		GetComponent<FlickGesture>().Flicked += flickHandler;

	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		characterGui.SetActive(false);
		GetComponent<TapGesture>().Tapped -= pressHandler;
		GetComponent<FlickGesture>().Flicked -= flickHandler;
	}
	
	private void pressHandler(object sender, EventArgs e)
	{
	
		power= power + speed;
		//particleSystem.enableEmission=true;


	}


	private void flickHandler(object sender, EventArgs e)
	{
		shouldLaunch=true;
		//particleSystem.enableEmission=false;
	}

}
