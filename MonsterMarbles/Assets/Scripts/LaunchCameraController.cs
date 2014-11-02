using UnityEngine;
using System.Collections;

public class LaunchCameraController : MonoBehaviour {

	public float distance = 1f;
	public float height = 1f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;

	public Transform target;
	public string cameraState="launch";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		// Early out if we don't have a target
		if (!target)
			return;

		//determine which mode the camera is in


		switch (cameraState) {

			//camera is positioned for launching
		case "launch": 
						
			// Calculate the current rotation angles
			float wantedRotationAngle = target.eulerAngles.y;
			float wantedHeight = target.position.y + height;
			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			
			// Set the height of the camera
			transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
			
			// Always look at the target
			transform.LookAt (target);
			break;

		case "birdsEye":
			break;
		}
	}
}
