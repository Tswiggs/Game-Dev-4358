using UnityEngine;
using System.Collections;

public class TeamAffiliation : MonoBehaviour {
	
	public enum State {NONE, RED, BLUE};
	private State currentState;
	// Use this for initialization
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
		
		currentState = newState;
		
		if(newState == State.NONE){
			transform.FindChild("Character Root").FindChild ("Halo Effect").GetComponent<TeamGlow>().setCurrentState(TeamGlow.State.GLOW_NONE);
			
		}
		else if (newState == State.RED){
			transform.FindChild("Character Root").FindChild ("Halo Effect").GetComponent<TeamGlow>().setCurrentState(TeamGlow.State.GLOW_RED);
		}
		else if (newState == State.BLUE){
			transform.FindChild("Character Root").FindChild ("Halo Effect").GetComponent<TeamGlow>().setCurrentState(TeamGlow.State.GLOW_BLUE);
		}
		
		return true;
	}
}
