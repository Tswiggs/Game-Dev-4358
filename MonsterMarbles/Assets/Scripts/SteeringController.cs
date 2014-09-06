using UnityEngine;
using System.Collections;

public class SteeringController : MonoBehaviour {

	new public Camera camera;
	public float steerStrength=1;

	private Vector3 tilt;
	// Use this for initialization
	void Start () {
		tilt=new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		//set tilt to be a vector pointing either right or left of the character and scale the vector
		//by a public float steerStrength and by the current forward velocity.
		if(DeviceType.Handheld==SystemInfo.deviceType){
			tilt.x=Input.acceleration.x*camera.transform.TransformDirection(rigidbody.velocity).y* -steerStrength;
		}else{
			tilt.x=Input.GetAxis("Horizontal")*camera.transform.TransformDirection(rigidbody.velocity).y* -steerStrength;
		}
		rigidbody.AddForce(tilt);
	}
}
