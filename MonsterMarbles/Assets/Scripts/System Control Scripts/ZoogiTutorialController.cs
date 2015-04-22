using UnityEngine;
using System.Collections;

public class ZoogiTutorialController : MonoBehaviour {
	
	
	/* The tutorial controller should:
	Display tutorial screens when specific actions happen
	Cease displaying tutorial screens when specific actions happen
	*/
	
	public enum State {IDLE, LAUNCH_MECHANICS, MAP_MECHANICS, POWER_MECHANICS, SOLO_PLAY_COLLECT_SKYBITS_VICTORY_MECHANICS, DO_NOT_SHOW};
	private State currentState;
	
	private string currentTutorial;
	private int maxTutorialIndex;
	private int currentTutorialIndex;
	
	private bool launchTutorial = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName == Constants.SCENE_WOLFGANG_1){
			if(!launchTutorial){
				launchTutorial = true;
				setCurrentState(State.LAUNCH_MECHANICS);
			}
		}
	}
	
	public bool setCurrentState(State newState){
		//TODO: Verify we got a valid State pattern
		
		if(currentState == State.DO_NOT_SHOW){
			return false;
		}
		else if(currentState == State.IDLE){
			
		}
		else if(currentState == State.LAUNCH_MECHANICS){
			GameGUIController.hideTutorial("Launch Mechanics", 1);
			GameGUIController.hideTutorial("Launch Mechanics", 2);
			GameGUIController.hideTutorial("Launch Mechanics", 3);
		}
		else if(currentState == State.MAP_MECHANICS){
			
		}
		
		currentState = newState;
		
		if(currentState == State.IDLE){
			currentTutorialIndex = 0;
			currentTutorial = "None";
			maxTutorialIndex = 0;
		}
		else if(newState == State.LAUNCH_MECHANICS){
			currentTutorial = "Launch Mechanics";
			maxTutorialIndex = 3;
			currentTutorialIndex = 0;
			nextTutorial();
		}
		else if(newState == State.MAP_MECHANICS){
			
		}
		
		return true;
	}
	
	
	public void nextTutorial(){
		unsubscribe();
		if(currentTutorialIndex != 0){
			GameGUIController.hideTutorial(currentTutorial, currentTutorialIndex);
		}
		currentTutorialIndex += 1;
		if(currentTutorialIndex <= maxTutorialIndex){
			GameGUIController.showTutorial(currentTutorial, currentTutorialIndex);
			subscribe ();
		}
		else{
			setCurrentState(State.IDLE);
		}
	}
	
	public void nextTutorial(GameObject zoogi){
		nextTutorial();
	}
	
	public void unsubscribe(){
		if(currentTutorial == "Launch Mechanics"){
			if(currentTutorialIndex == 1){
				AimPlayerBall.rotateButtonReleased -= nextTutorial;
			}
			else if(currentTutorialIndex == 2){
				AimPlayerBall.pullbackButtonTapped -= nextTutorial;
			}
			else if(currentTutorialIndex == 3){
				ZoogiLaunchBehavior.launchCompleted -= nextTutorial;
			}
		}
	}
	
	public void subscribe(){
		if(currentTutorial == "Launch Mechanics"){
			if(currentTutorialIndex == 1){
				AimPlayerBall.rotateButtonReleased += nextTutorial;
			}
			else if(currentTutorialIndex == 2){
				AimPlayerBall.pullbackButtonTapped += nextTutorial;
			}
			else if(currentTutorialIndex == 3){
				ZoogiLaunchBehavior.launchCompleted += nextTutorial;
			}
		}
	}
	
	void OnDestroy(){
		unsubscribe();
		setCurrentState(State.IDLE);
	}
}
