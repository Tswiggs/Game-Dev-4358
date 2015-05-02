using UnityEngine;
using System.Collections;

public class ZoogiLaunchBehavior : MonoBehaviour {
	
	
	public delegate void launchUpdate(GameObject associatedObject);
	public static event launchUpdate launchCompleted;
	public static event launchUpdate launchStarted;
	
	public delegate void launchInformation(GameObject ball, Vector3 launchVector, float xTorque, Vector3 position);
	public static event launchInformation sendLaunchInformation;
	
	public enum State {INACTIVE, IDLE, SPIN_UP, LAUNCH};
	private State currentState;
	
	public const float MAX_SPIN_ROTATION = 30f;
	public const float MAX_LAUNCH_POWER = 300f;
	private const float activeClickZonePercentage = 0.1f; //Percentage of Screen height that is an active click zone around the ball's center
	private float maxPullbackZonePercentage = 0.3f; //Percentage of Screen height or width (whichever is lower) beyond which you reach max pullback
	
	public AudioClip launchSound;
	
	private float powerFraction; //Total fraction of LAUNCH POWER to be used
	
	private Transform characterRoot;
	private Transform ball;
	
	
	// Use this for initialization
	void Awake(){
		currentState = State.IDLE;
		powerFraction = 0f;
	}
	
	void Start () {
		characterRoot = transform.parent.FindChild("Character Root");
		ball = transform.parent.FindChild ("Ball");
		setCurrentState(State.IDLE);
	}
	
	// Update is called once per frame
	void Update () {
		/*if(getCurrentState() == State.IDLE){
			if(Input.GetMouseButtonDown(0)){
				Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(ball.position);
				int pixelHalfSpace = (int)((Screen.height*activeClickZonePercentage)/2);
				Rect activeClickZone = new Rect(ballScreenPosition.x-pixelHalfSpace, ballScreenPosition.y-pixelHalfSpace, pixelHalfSpace*2,pixelHalfSpace*2);
				if(activeClickZone.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
					GameAudioController.playOneShotSound(launchSound);
					setCurrentState(State.SPIN_UP);
				}
			}
			
		}
		else if (getCurrentState() == State.SPIN_UP){
		
			Vector3 ballScreenPosition = Camera.main.WorldToScreenPoint(ball.position);
			int pixelHalfSpace = (int)((Screen.height*activeClickZonePercentage)/2);
			Rect activeClickZone = new Rect(ballScreenPosition.x-pixelHalfSpace, ballScreenPosition.y-pixelHalfSpace, pixelHalfSpace*2,pixelHalfSpace*2);
			
			if(Input.GetMouseButton(0)){
				if(!activeClickZone.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
					Vector3 directionVector = ballScreenPosition - Input.mousePosition;
					ball.forward = new Vector3(directionVector.x,ball.forward.z,directionVector.y);
					float distanceFromBall = Vector2.Distance(new Vector2(ballScreenPosition.x,ballScreenPosition.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y));
					float maxPullbackRadius;
					if(Screen.height <= Screen.width){
						maxPullbackRadius = Screen.height * maxPullbackZonePercentage;
					}
					else{
						maxPullbackRadius = Screen.width * maxPullbackZonePercentage;
					}
					setPowerFraction(distanceFromBall/maxPullbackRadius);
				}
			}
			else{
				if(activeClickZone.Contains(new Vector2(Input.mousePosition.x,Input.mousePosition.y))){
					setCurrentState(State.IDLE);
				}
				else{
					setCurrentState(State.LAUNCH);
				}
			}
		}
		else*/ if (getCurrentState() == State.LAUNCH){
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
			
			if(GetComponent<AudioSource>().isPlaying){
				GetComponent<AudioSource>().Stop();
			}
			
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
			
			if(!GetComponent<AudioSource>().isPlaying){
				GetComponent<AudioSource>().Play();
			}
			
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
		
		GameAudioController.playOneShotSound(launchSound, 0.3f);
		
		if(launchCompleted != null){
			launchCompleted(transform.parent.gameObject);
		}
	}
	
	void OnDestroy(){
		setCurrentState(State.INACTIVE);
	}
	
	public Vector3 calculateLaunchVector(){
		Vector3 launchVector= new Vector3();
		launchVector = transform.parent.FindChild("Character Root").forward*MAX_LAUNCH_POWER*powerFraction;
		launchVector.y = 0f;
		return launchVector;
	}
}
