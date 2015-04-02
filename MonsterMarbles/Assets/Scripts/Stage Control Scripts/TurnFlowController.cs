using UnityEngine;
using System.Collections;

public class TurnFlowController : MonoBehaviour {
	
	GameObject selectedZoogi; //Current zoogi whose turn it is
	ZoogiController selectedZoogiController;
	
	public enum State {LAUNCH, ROLL, CLEANUP, MAP_VIEW, TURN_BEGIN, TURN_END, INACTIVE };
	private State currentState;
	
	public delegate void stateChange(GameObject Zoogi);
	public static event stateChange TurnBeginEvent;
	public static event stateChange TurnEndEvent;
	
	
	// Use this for initialization
	void Awake() {
		currentState = State.INACTIVE;
	}
	
	void OnEnable(){
		
	}
	
	void OnDisable(){
	
	}
	
	// Update is called once per frame
	void Update() {
		if(getCurrentState() == State.TURN_BEGIN){
			setCurrentState (State.LAUNCH);
		}
	}
	
	public void takeTurn(GameObject zoogi){
		selectedZoogi = zoogi;
		selectedZoogiController = selectedZoogi.GetComponent<ZoogiController>();
		setCurrentState(State.TURN_BEGIN);
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.LAUNCH){
			selectedZoogiController.setCurrentState (ZoogiController.State.INACTIVE);
			ZoogiLaunchBehavior.launchCompleted -= launchCompleted;
		}
		else if(currentState == State.ROLL){
			selectedZoogiController.setCurrentState (ZoogiController.State.INACTIVE);
			ZoogiRollBehavior.RollHasStopped -= rollCompleted;
		}
		else if(currentState == State.CLEANUP){
			selectedZoogiController.setCurrentState (ZoogiController.State.INACTIVE);
			ZoogiHopBehavior.hopComplete -= hopComplete;
		}
		
		currentState = newState;
		
		if(newState == State.TURN_BEGIN){
			OutOfBoundsHandler.playerOutOfBounds += playerOutOfBounds;
			if(TurnBeginEvent != null){
				TurnBeginEvent(selectedZoogi);
			}
		}
		else if(newState == State.LAUNCH){
			selectedZoogiController.setCurrentState (ZoogiController.State.CONTROLS_ACTIVE);
			ZoogiLaunchBehavior.launchCompleted += launchCompleted;
		}
		else if(newState == State.ROLL){
			selectedZoogiController.setCurrentState (ZoogiController.State.ROLLING);
			ZoogiRollBehavior.RollHasStopped += rollCompleted;
		}
		else if(newState == State.CLEANUP){
			selectedZoogiController.setCurrentState(ZoogiController.State.HOPPING);
			ZoogiHopBehavior.hopComplete += hopComplete;
		}
		else if(newState == State.TURN_END){
			OutOfBoundsHandler.playerOutOfBounds -= playerOutOfBounds;
			if(TurnEndEvent != null){
				TurnEndEvent(selectedZoogi);
			}
		}
		
		return true;
	}
	
	public void rollCompleted(GameObject zoogi){
		if(zoogi.GetInstanceID() == selectedZoogi.GetInstanceID ()){
			if(getCurrentState() == State.ROLL){
				setCurrentState(State.CLEANUP);
			}
		}
	}
	
	public void launchCompleted(GameObject zoogi){
		if(zoogi.GetInstanceID() == selectedZoogi.GetInstanceID ()){
			GameCameraController.setCurrentState(GameCameraController.State.FOLLOW_FAR);
			if(getCurrentState() == State.LAUNCH){
				setCurrentState(State.ROLL);
			}
		}
	}
	
	public void hopComplete(GameObject zoogi){
		if(zoogi.GetInstanceID() == selectedZoogi.GetInstanceID ()){
			if(getCurrentState() == State.CLEANUP){
				setCurrentState(State.TURN_END);
			}
		}
	}
	
	public void playerOutOfBounds(GameObject zoogi){
		if(zoogi.GetInstanceID() == selectedZoogi.GetInstanceID ()){
			if(getCurrentState() != State.INACTIVE){
				setCurrentState(State.TURN_END);
			}
		}
	}
}
