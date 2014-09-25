using UnityEngine;
using TouchScript.Gestures;
using System;

public class LaunchController : MonoBehaviour {

	new public Camera camera;
	public AudioSource audioSource;
	public AudioClip explosionSound; 
	public float speed=1;
	public float launchScalar=1;
	public float launchSpin=1;
	public float maxPower=100;
	public float powerFade=1;
	public delegate void postLaunchAction();
	public static event postLaunchAction launchCompleted;

	private bool shouldLaunch=false;
	private float power=0;
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		power=Mathf.Clamp(power, 0f, maxPower);
		transform.Rotate(power,0f,0f);
		if(shouldLaunch)
		{
			Vector3 launchVector= new Vector3();
			launchVector=(transform.position-camera.transform.position).normalized*power*launchScalar;
			launchVector.y=0f;
			rigidbody.AddForce(launchVector);
			rigidbody.AddRelativeTorque(power*launchSpin, 0f, 0f);
			audioSource.PlayOneShot(explosionSound);
			shouldLaunch=false;
			GetComponent<SteeringController>().enabled=true;
			GetComponent<LaunchController>().enabled=false;

			if(launchCompleted != null)
			{
				launchCompleted();
			}
		}
	}


	private void OnEnable()
	{
		// subscribe to gesture's Panned event

		GetComponent<TapGesture>().Tapped += pressHandler;
		GetComponent<FlickGesture>().Flicked += flickHandler;
		power = 0;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
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
