using UnityEngine;
using System.Collections;

public class FollowCameraBehavior : MonoBehaviour {
	
	private float distance = 5f;
	private float height = 3f;
	private float heightDamping = 1.5f;
	private float rotationDamping = 3.0f;
	private float positionDamping = 4.0f;
	
	public const float CLOSE_DISTANCE = 6f;
	public const float CLOSE_HEIGHT = 4f;
	
	public const float FAR_DISTANCE = 0f;
	public const float FAR_HEIGHT = 20f;
	
	public Transform target;
	
	public enum State {CLOSE, FAR};
	private State currentState;
	
	private bool lockPosition = false; //Forces camera to not move position while following target
	
	// Use this for initialization
	void Start () {
		currentState = State.CLOSE;
		setCurrentState(State.CLOSE);
	}
	
	public bool setFocusTarget(Transform focusTarget){
		target = focusTarget;
		return true;
	}
	
	public bool setPositionLock(bool value){
		lockPosition = value;
		return lockPosition;
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		
		if(currentState == State.CLOSE){
			
		}
		else if(currentState == State.FAR){
			
		}
		
		currentState = newState;
		
		if(newState == State.CLOSE){
			distance = CLOSE_DISTANCE;
			height = CLOSE_HEIGHT;
		}
		else if(newState == State.FAR){
			distance = FAR_DISTANCE;
			height = FAR_HEIGHT;
		}
		
		return true;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Early out if we don't have a target
		if (!target)
			return;
		
		float wantedRotationAngle;
		float wantedHeight;
		float wantedDistance;
		float currentRotationAngle;
		float currentHeight;
		float currentDistance;
			
		if(getCurrentState() == State.CLOSE || getCurrentState() == State.FAR){
			
			// Calculate the current rotation angles
			currentRotationAngle = transform.eulerAngles.y;
			currentHeight = transform.position.y;
			currentDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x,target.position.z));
			
			if(lockPosition){
				wantedHeight = currentHeight;
				wantedDistance = currentDistance;
				wantedRotationAngle = currentRotationAngle;
			}
			else{
				wantedHeight = target.position.y + height;
				wantedDistance = distance;
				wantedRotationAngle = target.eulerAngles.y;
			}
			
			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			
			// Damp the height
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			
			// Convert the angle into a rotation
			Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
			
			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			
			
			currentDistance = Mathf.Lerp (currentDistance,wantedDistance, positionDamping * Time.deltaTime);
			
			transform.position -= currentRotation * Vector3.forward * currentDistance;
			
			//transform.position -= currentRotation * Vector3.forward * distance;
			
			// Set the height of the camera
			transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
			
			// Always look at the target
			transform.LookAt (target);
		}
	}
}
