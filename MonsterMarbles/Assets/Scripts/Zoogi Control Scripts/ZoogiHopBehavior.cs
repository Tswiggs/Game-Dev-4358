using UnityEngine;
using System.Collections;

public class ZoogiHopBehavior : MonoBehaviour {
	
	private GameObject ball;
	private Rigidbody rigidBody;
	
	public delegate void hopAction(GameObject zoogi);
	public static event hopAction hopComplete;
	
	private Quaternion standingUpRotation;
	private float hopTime = 2f;
	private float hopForce = 75f;
	private float spinTime = 0.7f;
	
	private float hopTimer = 0f;
	private float spinTimer = 0f;
	
	public enum State {INACTIVE, HOPPING, HOP_FINISHED};
	private State currentState;
	
	// Use this for initialization
	void Awake() {
		currentState = State.INACTIVE;
	}
	
	void Start () {
		ball = this.gameObject;
		rigidBody = ball.GetComponent<Rigidbody>();
		standingUpRotation = new Quaternion();
		setCurrentState(State.INACTIVE);
	}
	
	// Update is called once per frame
	void Update () {
		if(getCurrentState() == State.HOPPING){
		
			transform.rotation = Quaternion.Slerp(transform.rotation, standingUpRotation, spinTimer/spinTime);
			spinTimer += Time.deltaTime;
			
			
			if(hopTimer >= hopTime){
				if(ball.GetComponent<Rigidbody>().velocity.y < 0.1f && ball.GetComponent<Rigidbody>().velocity.y > -0.1f){
					setCurrentState(State.HOP_FINISHED);
				}
			}
			else{
				hopTimer += Time.deltaTime;
			}
			
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if( currentState == State.INACTIVE){
			
		}
		else if(currentState == State.HOPPING){
			GameCameraController.setPositionLock(false);
			rigidBody.constraints = RigidbodyConstraints.None;
		}
		else if (currentState == State.HOP_FINISHED){
			rigidBody.constraints = RigidbodyConstraints.None;
		}
		
		currentState = newState;
		
		if(newState == State.INACTIVE){
			
		}
		else if (newState == State.HOPPING){
			hopTimer = 0;
			spinTimer = 0;
			GameCameraController.setPositionLock(true);
			rigidBody.constraints = RigidbodyConstraints.None;
			rigidBody.AddForce(new Vector3(0, hopForce, 0), ForceMode.Impulse);
			standingUpRotation.y = transform.rotation.y;
			rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		}
		else if (newState == State.HOP_FINISHED){
			rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			//this.transform.parent.SendMessage("hopComplete");
			if(hopComplete != null){
				hopComplete(this.transform.parent.gameObject);
			}
		}
		
		return true;
	}
	
	void OnDisable(){
		setCurrentState(State.INACTIVE);
	}
}
