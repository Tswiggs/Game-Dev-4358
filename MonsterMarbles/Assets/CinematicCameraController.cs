using UnityEngine;
using System.Collections;

public class CinematicCameraController : MonoBehaviour {

	public float startDistance;
	public float endDistance;
	public float startHeight;
	public float endHeight;
	public float zoomSpeed;
	public float downSpeed;
	public float spinSpeed;
	//public float heightDamping;
	//public float rotationDamping;
	public Transform target;

	Vector3 displacement;
	float wantedRotationAngle;


	bool action;

	// Use this for initialization
	void Start () {
		action=false;
		displacement=target.forward;
		displacement=displacement*startDistance;
		displacement.y=displacement.y+startHeight;
		//displacement= Quaternion.Euler(0,30,0)*displacement;
		gameObject.transform.position=target.transform.position+displacement;
		gameObject.transform.LookAt(target);


	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.X)){this.Start();}
		if(Input.GetKeyDown(KeyCode.C)){ action=!action;}
		if(action){
			   
			transform.LookAt(target);
			if(Mathf.Abs((target.position-gameObject.transform.position).magnitude)>endDistance)
			{
				print((target.position-gameObject.transform.position).magnitude);
				Vector3 forward=transform.forward;
				forward.y=0;
				transform.Translate(forward * Time.deltaTime * zoomSpeed);
			}
			if(Mathf.Abs(target.position.y-gameObject.transform.position.y)>endHeight)
			//if(false)
			{
				//print(Mathf.Abs(target.position.y-gameObject.transform.position.y));
				transform.Translate(-Vector3.up * Time.deltaTime * downSpeed);
			}
			transform.RotateAround(target.position, Vector3.up,20*Time.deltaTime*spinSpeed);

//			float currentRotationAngle = transform.eulerAngles.y;
//			float currentHeight = transform.position.y;
//
//			// Damp the rotation around the y-axis
//			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
//			
//			// Damp the height
//			currentHeight = Mathf.Lerp (currentHeight, endHeight, heightDamping * Time.deltaTime);
//			
//			// Convert the angle into a rotation
//			Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
//			
//			// Set the position of the camera on the x-z plane to:
//			// distance meters behind the target
//			transform.position = target.position;
//			transform.position -= currentRotation * Vector3.forward * endDistance;
//			
//			// Set the height of the camera
//			transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
		}

	}
}
