using UnityEngine;
using System.Collections;

public class TeamGlow : MonoBehaviour {
	
	public static Color teamColor;
	private Light glow;
	
	public enum State {GLOW_NONE, GLOW_RED, GLOW_BLUE};
	private State currentState;
	// Use this for initialization
	void Awake() {
		teamColor = Color.red;
		currentState = State.GLOW_NONE;
		glow = GetComponent<Light>();
		glow.color = teamColor;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.GLOW_NONE){
			
		}
		
		currentState = newState;
		
		if(newState == State.GLOW_NONE){
			glow.color = Color.clear;
		}
		else if (newState == State.GLOW_RED){
			glow.color = Color.red;
		}
		else if (newState == State.GLOW_BLUE){
			glow.color = Color.blue;
		}
		
		return true;
	}
	
	void setCurrentTeamColor(int playerIndex){
		if(playerIndex == 0){
			teamColor = Color.red;
		}
		else{
			teamColor = Color.blue;
		}
	}
	
}
