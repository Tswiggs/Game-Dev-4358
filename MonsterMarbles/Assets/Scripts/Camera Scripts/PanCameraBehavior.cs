using UnityEngine;
using System.Collections;

public class PanCameraBehavior : MonoBehaviour {
	
	private float distance = 0f;
	private float height = 8f;
	private float heightDamping = 1.5f;
	private float rotationDamping = 3.0f;
	private float positionDamping = 4.0f;
	
	private float keyPanSpeed = 15f;
	private float mousePanSpeed = 3f;
	private float touchPanSpeed = 15f;
	
	public const float CLOSE_DISTANCE = 0f;
	public const float CLOSE_HEIGHT = 14f;
	
	public const float FAR_DISTANCE = 0f;
	public const float FAR_HEIGHT = 20f;
	
	public Transform target;
	
	private Vector3 focusPosition;
	
	private Vector2 mousePanPreviousPosition;
	private Vector2 mousePanCurrentPosition;
	private bool beginMousePan = false;
	
	public enum State {CLOSE, FAR, CLOSE_NO_PAN};
	private State currentState;
	// Use this for initialization
	void Awake () {
		currentState = State.CLOSE;
	}
	
	public bool setFocusTarget(Transform focusTarget){
		target = focusTarget;
		focusPosition = target.position;
		return true;
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		
		if(currentState == State.CLOSE){
			
		}
		else if(currentState == State.FAR){
			
		}
		else if(currentState == State.CLOSE_NO_PAN){
			
		}
		
		currentState = newState;
		
		if(newState == State.CLOSE){
			distance = CLOSE_DISTANCE;
			height = CLOSE_HEIGHT;
			focusPosition = target.position;
		}
		else if(newState == State.FAR){
			distance = FAR_DISTANCE;
			height = FAR_HEIGHT;
			focusPosition = target.position;
		}
		else if(newState == State.CLOSE_NO_PAN){
			distance = CLOSE_DISTANCE;
			height = CLOSE_HEIGHT;
			focusPosition = target.position;
		}
		
		return true;
	}
	
	private void processInputs() {
		if(Input.GetMouseButtonDown(0)){
			if(!beginMousePan){
				beginMousePan = true;
				mousePanCurrentPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			}
			else{
				beginMousePan = false;
			}
		}
		else if(beginMousePan){
			mousePanPreviousPosition = new Vector2(mousePanCurrentPosition.x,mousePanCurrentPosition.y);
			mousePanCurrentPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			Vector2 offsetVector = new Vector2(mousePanPreviousPosition.x,mousePanPreviousPosition.y) - new Vector2(mousePanCurrentPosition.x,mousePanCurrentPosition.y);
			offsetVector.Scale(new Vector2(mousePanSpeed*Time.deltaTime,mousePanSpeed*Time.deltaTime));
			focusPosition = new Vector3(focusPosition.x+offsetVector.x, focusPosition.y, focusPosition.z+ offsetVector.y);
		}
		else{
			if(Input.GetKey(KeyCode.W)){
				focusPosition = new Vector3(focusPosition.x,focusPosition.y,focusPosition.z+keyPanSpeed*Time.fixedDeltaTime);
			}
			else if(Input.GetKey(KeyCode.S)){
				focusPosition = new Vector3(focusPosition.x,focusPosition.y,focusPosition.z-keyPanSpeed*Time.fixedDeltaTime);
			}
			
			if(Input.GetKey(KeyCode.D)){
				focusPosition = new Vector3(focusPosition.x+keyPanSpeed*Time.fixedDeltaTime,focusPosition.y,focusPosition.z);
			}
			else if(Input.GetKey(KeyCode.A)){
				focusPosition = new Vector3(focusPosition.x-keyPanSpeed*Time.fixedDeltaTime,focusPosition.y,focusPosition.z);
			}
		}
		
		if(!Input.GetMouseButton(0)){
			beginMousePan = false;
		}
	}
	
	void FixedUpdate() {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Early out if we don't have a target
		if (!target)
			return;
		if(getCurrentState() != State.CLOSE_NO_PAN){
			processInputs();
		}
		
		float wantedRotationAngle;
		float wantedHeight;
		float wantedDistance;
		float currentRotationAngle;
		float currentHeight;
		float currentDistance;
		
			
		// Calculate the current rotation angles
		wantedRotationAngle =  80f;
		wantedHeight = focusPosition.y + height;
		wantedDistance = distance;
		currentRotationAngle = transform.eulerAngles.x;
		currentHeight = transform.position.y;
		currentDistance = Vector2.Distance(new Vector2(focusPosition.x, focusPosition.z), new Vector2(transform.position.x, transform.position.z));
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		Quaternion currentRotation = Quaternion.Euler (currentRotationAngle,0, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = focusPosition;
		
		
		currentDistance = Mathf.Lerp (currentDistance,wantedDistance, positionDamping * Time.deltaTime);
		
		transform.position -= currentRotation * Vector3.forward * currentDistance;
		
		transform.rotation = currentRotation;
		//transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		transform.position = new Vector3(focusPosition.x, currentHeight, focusPosition.z);
			
	}
}
