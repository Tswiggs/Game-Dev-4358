using UnityEngine;
using System.Collections;

public class RollABallPlayerController : MonoBehaviour {

	new public GameObject camera;
	public float speed;

	private Vector3 tilt;

	// Use this for initialization
	void Start () {
		tilt=new Vector3(0f,0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		tilt=camera.transform.TransformDirection(Time.deltaTime*Input.GetAxis("Horizontal"), 0f, 0f);
		tilt+=camera.transform.TransformDirection(0f, 0f, Time.deltaTime*Input.GetAxis("Vertical"));
		GetComponent<Rigidbody>().AddForce(tilt*speed);
	}
}
