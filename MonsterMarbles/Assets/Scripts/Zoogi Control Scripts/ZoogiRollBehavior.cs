using UnityEngine;
using System.Collections;

public class ZoogiRollBehavior : MonoBehaviour {
	
	private float noDragVelocity = 10f;
	private float maxDragVelocity = 3f;
	private float minimumDragPercentage = 0.1f;
	
	private float normalDrag;
	
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
		normalDrag = rigidBody.drag;
		setCurrentState(State.INACTIVE);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		if(getCurrentState() == State.ROLLING){
			
			setDragBasedOnVelocity();
			
			if(stopVelocityThreshold >= rigidBody.velocity.magnitude){
				if(stopTimer >= stopVelocityTime){
					rigidBody.velocity = Vector3.zero;
					rigidBody.angularVelocity = Vector3.zero;
					setCurrentState(State.STOPPED);
				}
				else{
					stopTimer += Time.fixedDeltaTime;
				}
			}
			else{
				stopTimer = 0;
			}
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag(Constants.TAG_PLAYER) || collision.gameObject.CompareTag(Constants.TAG_MARBLE) || collision.gameObject.CompareTag(Constants.TAG_BUMPER)){
			setCurrentState(State.ROLLING);
		}
	}
	
	void OnDisable(){
		setCurrentState(State.INACTIVE);
	}
	
	private void setDragBasedOnVelocity(){
		if(rigidBody.velocity.magnitude >= noDragVelocity){
			rigidBody.drag = normalDrag*minimumDragPercentage;
		}
		else if(rigidBody.velocity.magnitude <= maxDragVelocity){
			rigidBody.drag = normalDrag;
		}
		else{
			float percentage = Mathf.Pow(1f-(rigidBody.velocity.magnitude-maxDragVelocity),3f);
			if(percentage < minimumDragPercentage){
				percentage = minimumDragPercentage;
			}
			rigidBody.drag = normalDrag*percentage;
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if( currentState == State.INACTIVE){
		
		}
		else if(currentState == State.ROLLING){
			rigidBody.drag = normalDrag;
		}
		else if (currentState == State.STOPPED){
			rigidBody.constraints = RigidbodyConstraints.None;
		}
		
		currentState = newState;
		
		if(newState == State.ROLLING){
			normalDrag = rigidBody.drag;
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
