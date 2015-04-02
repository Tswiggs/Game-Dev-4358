using UnityEngine;
using System.Collections;

public class ZoogiRollBehavior : MonoBehaviour {
	
	private float stopVelocityThreshold = 0.5f;
	private float stopVelocityTime = 1.75f;
	private float stopTimer = 0;
	
	public enum State {INACTIVE, ROLLING, STOPPED};
	private State currentState;
	
	private GameObject ball;
	private Rigidbody rigidBody;
	
	public delegate void rollAction(GameObject zoogi);
	public static event rollAction RollHasStopped;
	
	// Use this for initialization
	void Awake(){
		currentState = State.INACTIVE;
	}
	
	void Start () {
		ball = this.gameObject;
		rigidBody = ball.GetComponent<Rigidbody>();
		setCurrentState(State.INACTIVE);
	}
	
	// Update is called once per frame
	void Update () {
		if(getCurrentState() == State.ROLLING){
			if(stopVelocityThreshold >= rigidBody.velocity.magnitude){
				if(stopTimer >= stopVelocityTime){
					rigidBody.velocity = Vector3.zero;
					rigidBody.angularVelocity = Vector3.zero;
					setCurrentState(State.STOPPED);
				}
				else{
					stopTimer += Time.deltaTime;
				}
			}
			else{
				stopTimer = 0;
			}
		}
	}
	
	void OnDisable(){
		setCurrentState(State.INACTIVE);
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if( currentState == State.INACTIVE){
		
		}
		else if(currentState == State.ROLLING){
			
		}
		else if (currentState == State.STOPPED){
			rigidBody.constraints = RigidbodyConstraints.None;
		}
		
		currentState = newState;
		
		if(newState == State.ROLLING){
			stopTimer = 0;
		}
		else if (newState == State.STOPPED){
			rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
			//this.transform.parent.SendMessage("rollHasStopped");
			if( RollHasStopped != null){
				RollHasStopped(ball.transform.parent.gameObject);
			}
		}

		return true;
	}
}
