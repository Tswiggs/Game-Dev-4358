using UnityEngine;
using TouchScript.Gestures;
using System;

public class LaunchController : MonoBehaviour {

	new public Camera camera;
	public AudioSource audioSource;
	public AudioClip explosionSound;
	public AudioSource pullbackSource;
	public GameObject characterGui;
	public GameObject root;
	public float speed=1;
	private float launchScalar=1;
	public float launchSpin=1;
	public float maxPower=100;
	public float powerFade=1;
	public delegate void postLaunchAction();
	public static event postLaunchAction launchCompleted;
	public delegate void launchInformation(GameObject ball, Vector3 launchVector, float xTorque, Vector3 position);
	public static event launchInformation sendLaunchInformation;
	public delegate void launchEnabled(GameObject associatedObject);
	public static event launchEnabled LaunchControllerEnabled;
	
	private static float LAUNCH_SCALE = 1800;
	
	//Variables used for the correctional hop
	private Quaternion standingUp=new Quaternion();
	private bool isHopping =false;
	private float hopStart= 0f;
	private float timeSpinning= 0f;
	private float hopLandingYThreshold=.5f;
	public float timeToSpin=1f;
	public float hopHeight=3000f;




	private bool shouldLaunch=false;
	private float power=0;
	// Use this for initialization
	void Start () {
		camera=Camera.main;
		audioSource=camera.GetComponent<AudioSource>();
		launchScalar = LAUNCH_SCALE;
	}


	// Update is called once per frame
	void Update () {
		
		
		power=Mathf.Clamp(power, 0f, maxPower);
		transform.Rotate(power,0f,0f);
		if(isHopping){
			transform.rotation =Quaternion.Slerp(transform.rotation, standingUp, timeSpinning/timeToSpin);
			timeSpinning+=Time.deltaTime;

			//If the slerp is within 1 degree of completion activate protocol to complete the turn
			if((timeSpinning>=timeToSpin) && (transform.position.y<=hopStart) && (rigidbody.velocity.y<=0.1f)){
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
		hopStart=transform.position.y+hopLandingYThreshold;
		if(Quaternion.Angle(transform.rotation, standingUp)>=0.003f)
		{
			rigidbody.AddForce(0f, hopHeight, 0f);
		}
	}
	
	private void performLaunch(float powerFraction){
		Vector3 launchVector= new Vector3();
		//launchVector=(transform.position-camera.transform.position)*maxPower*powerFraction*launchScalar;
		launchVector=transform.parent.FindChild("Character Root").forward*maxPower*powerFraction*launchScalar;
		launchVector.y=0f;
		rigidbody.AddForce(launchVector);
		rigidbody.AddRelativeTorque(maxPower*powerFraction*launchSpin, 0f, 0f);
		
		if(explosionSound != null){
			audioSource.PlayOneShot(explosionSound);
		}
		shouldLaunch=false;
		characterGui.SetActive(false);
		GetComponent<SteeringController>().enabled=true;
		GetComponent<LaunchController>().enabled=false;
		camera=Camera.main;
		
		pullbackSource.Stop();
		
		
		
		if(launchCompleted != null)
		{
			launchCompleted();
		}
		if(sendLaunchInformation != null){
			sendLaunchInformation(this.gameObject,launchVector, maxPower*powerFraction*launchSpin, transform.position);
		}
	}
	
	private void updateLaunchInformation(float powerFraction){
		
		if(!pullbackSource.isPlaying){
			pullbackSource.Play();
		}
		power = maxPower*powerFraction;
	}


	private void OnEnable()
	{
		if(LaunchControllerEnabled != null){
			LaunchControllerEnabled(this.gameObject);
		}
		
		if(transform.up!=root.transform.up){hopUpright();}
		else{characterGui.SetActive(true);}
		power=0f;
		//GetComponent<LongPressGesture>().LongPressed += pressHandler;
		GetComponent<FlickGesture>().Flicked += flickHandler;
		
		PullTestScript.launchActivated += performLaunch;
		PullTestScript.pullbackInformation += updateLaunchInformation;

	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		characterGui.SetActive(false);
		//GetComponent<LongPressGesture>().LongPressed -= pressHandler;
		GetComponent<FlickGesture>().Flicked -= flickHandler;
		
		PullTestScript.launchActivated -= performLaunch;
		PullTestScript.pullbackInformation -= updateLaunchInformation;
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
