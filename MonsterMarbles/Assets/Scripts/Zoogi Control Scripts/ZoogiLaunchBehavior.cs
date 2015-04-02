using UnityEngine;
using System.Collections;

public class ZoogiLaunchBehavior : MonoBehaviour {
	
	
	public delegate void launchUpdate(GameObject associatedObject);
	public static event launchUpdate launchCompleted;
	public static event launchUpdate launchStarted;
	
	public delegate void launchInformation(GameObject ball, Vector3 launchVector, float xTorque, Vector3 position);
	public static event launchInformation sendLaunchInformation;
	
	public enum State {IDLE, SPIN_UP, LAUNCH};
	private State currentState;
	
	public const float MAX_SPIN_ROTATION = 30f;
	public const float MAX_LAUNCH_POWER = 450f;
	
	private float powerFraction; //Total fraction of LAUNCH POWER to be used
	
	private Transform characterRoot;
	
	
	// Use this for initialization
	void Awake(){
		currentState = State.IDLE;
		powerFraction = 0f;
	}
	
	void Start () {
		characterRoot = transform.parent.FindChild("Character Root");
		setCurrentState(State.IDLE);
	}
	
	// Update is called once per frame
	void Update () {
		if(getCurrentState() == State.SPIN_UP){
			
		}
		else if (getCurrentState() == State.LAUNCH){
			performLaunch();
			setCurrentState(State.IDLE);
		}
	}
	
	void FixedUpdate(){
		if(getCurrentState() == State.SPIN_UP){
			transform.Rotate(powerFraction*MAX_SPIN_ROTATION,0f,0f);
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.IDLE){
			PullbackBehavior.pullbackStarted -= pullbackStarted;
		}
		else if (currentState == State.SPIN_UP){
			PullbackBehavior.pullbackReleased -= pullbackReleased;
			PullbackBehavior.pullbackAborted -= pullbackAborted;
			PullbackBehavior.pullbackInformation -= pullbackInformation;
			
		}
		else if( currentState == State.LAUNCH){
			
		}
		
		currentState = newState;
		
		if(newState == State.IDLE){
			PullbackBehavior.pullbackStarted += pullbackStarted;
		}
		else if( newState == State.SPIN_UP){
			PullbackBehavior.pullbackReleased += pullbackReleased;
			PullbackBehavior.pullbackAborted += pullbackAborted;
			PullbackBehavior.pullbackInformation += pullbackInformation;
			
			if( launchStarted != null){
				launchStarted(this.transform.parent.gameObject);
			}
		}
		else if( newState == State.LAUNCH){
			
		}

		return true;
	}
	
	public void setPowerFraction(float fraction){
		if(fraction > 1){
			powerFraction = 1;
		}
		else if (fraction < 0){
			powerFraction = 0;
		}
		else {
			powerFraction = fraction;
		}
	}
	
	public float getPowerFraction(){
		return powerFraction;
	}
	
	public void pullbackStarted(){
		setCurrentState(State.SPIN_UP);
	}
	
	public void pullbackReleased(){
		setCurrentState(State.LAUNCH);
	}
	
	public void pullbackAborted(){
		setCurrentState(State.IDLE);
	}
	
	public void pullbackInformation(float fraction){
		powerFraction = fraction;
	}
	
	public void performLaunch(){
		Vector3 launchVector = calculateLaunchVector();
		GetComponent<Rigidbody>().AddForce(launchVector, ForceMode.Impulse);
		GetComponent<Rigidbody>().AddRelativeTorque(new Vector3(MAX_LAUNCH_POWER*powerFraction,0,0),ForceMode.Impulse);
		//transform.parent.gameObject.SendMessage("launchCompleted");
		if(launchCompleted != null){
			launchCompleted(transform.parent.gameObject);
		}
	}
	
	public Vector3 calculateLaunchVector(){
		Vector3 launchVector= new Vector3();
		launchVector = transform.parent.FindChild("Character Root").forward*MAX_LAUNCH_POWER*powerFraction;
		launchVector.y = 0f;
		return launchVector;
	}
}
