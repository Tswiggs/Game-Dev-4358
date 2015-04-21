using UnityEngine;
using System.Collections;

public class TeamGlow : MonoBehaviour {
	
	public static Color teamColor;
	private ParticleSystem teamGlow;
	
	public enum State {GLOW_NONE, GLOW_RED, GLOW_BLUE};
	private State currentState;
	// Use this for initialization
	void Awake() {
		teamColor = Color.red;
		currentState = State.GLOW_NONE;
		
		teamGlow = GetComponent<ParticleSystem>();
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
			teamGlow.startColor = Color.clear;
		}
		else if (newState == State.GLOW_RED){
			teamGlow.startColor = Color.red;
		}
		else if (newState == State.GLOW_BLUE){
			teamGlow.startColor = Color.blue;
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
