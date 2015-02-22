using UnityEngine;
using System.Collections;

public class LaunchCameraController : MonoBehaviour {

	public float distance = 1f;
	public float height = 1f;
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public float positionDamping = 5.0f;

	public Transform target;
	
	public Transform birdEyeTarget;
	public float birdEyeZoom = 20.0f;
	
	
	public string cameraState="launch";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		// Early out if we don't have a target
		if (!target)
			return;

		float wantedRotationAngle;
		float wantedHeight;
		float currentRotationAngle;
		float currentHeight;

		//determine which mode the camera is in
		switch (cameraState) {
			
			//camera is positioned for launching
		case "launch": 
						
			// Calculate the current rotation angles
			wantedRotationAngle = target.eulerAngles.y;
			wantedHeight = target.position.y + height;
			currentRotationAngle = transform.eulerAngles.y;
			currentHeight = transform.position.y;
			
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
			
			//Camera is positioned far above the arena, giving a "bird's eye" view
		case "birdsEye":
		
			// Calculate the current rotation angles
			wantedHeight = birdEyeTarget.position.y + birdEyeZoom;
			currentHeight = transform.position.y;
			
			Vector2 wantedPosition = new Vector2 (birdEyeTarget.transform.position.x-1,birdEyeTarget.transform.position.z);
			Vector2 currentPosition = new Vector2 (transform.position.x,transform.position.z);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Damp the position movement
			currentPosition = new Vector2(Mathf.Lerp(currentPosition.x,wantedPosition.x,positionDamping*Time.deltaTime),Mathf.Lerp(currentPosition.y,wantedPosition.y,positionDamping*Time.deltaTime));
			
			// Set the position of the camera
			transform.position = new Vector3(currentPosition.x, currentHeight, currentPosition.y);
			
			
			// Always look at the target
			transform.LookAt (birdEyeTarget);
			break;
		}
	}
}
