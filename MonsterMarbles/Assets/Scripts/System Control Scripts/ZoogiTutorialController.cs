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
	
	private bool tutorialScreenTimed = false;
	private float tutorialTimer = 0f;
	private float tutorialMaxScreenTime = 0f;
	
	private bool launchTutorial = false;
	private bool collectObjectiveTutorial = false;
	
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
		else if(Application.loadedLevelName == Constants.SCENE_WOLFGANG_2){
			if(!collectObjectiveTutorial){
				collectObjectiveTutorial = true;
				setCurrentState(State.SOLO_PLAY_COLLECT_SKYBITS_VICTORY_MECHANICS);
			}
		}
		
		if(tutorialScreenTimed){
			if(tutorialTimer >= tutorialMaxScreenTime || Input.GetMouseButtonDown(0)){
				tutorialTimer =0;
				tutorialScreenTimed = false;
				nextTutorial();
			}
			else{
				tutorialTimer += Time.deltaTime;
			}
		}
	}
	
	public void setTutorialScreenTimer(float time){
		if(time > 0){
			tutorialMaxScreenTime = time;
		}
		else {
			tutorialMaxScreenTime = 1f;
		}
		tutorialTimer = 0;
		tutorialScreenTimed = true;
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
			GameGUIController.hideTutorial("Launch Mechanics", 4);
			GameGUIController.hideTutorial("Launch Mechanics", 5);
		}
		else if(currentState == State.SOLO_PLAY_COLLECT_SKYBITS_VICTORY_MECHANICS){
			GameGUIController.hideTutorial("Collect Objective", 1);
			GameGUIController.hideTutorial("Collect Objective", 2);
			GameGUIController.hideTutorial("Collect Objective", 3);
			GameGUIController.hideTutorial("Collect Objective", 4);
			GameGUIController.hideTutorial("Collect Objective", 5);
		}
		
		currentState = newState;
		
		if(currentState == State.IDLE){
			currentTutorialIndex = 0;
			currentTutorial = "None";
			maxTutorialIndex = 0;
		}
		else if(newState == State.LAUNCH_MECHANICS){
			currentTutorial = "Launch Mechanics";
			maxTutorialIndex = 4;
			currentTutorialIndex = 0;
			nextTutorial();
		}
		else if(newState == State.SOLO_PLAY_COLLECT_SKYBITS_VICTORY_MECHANICS){
			currentTutorial = "Collect Objective";
			maxTutorialIndex = 3;
			currentTutorialIndex = 0;
			nextTutorial();
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
	
	public void nextTutorial(int index){
		nextTutorial();
	}
	
	public void unsubscribe(){
		if(currentTutorial == "Launch Mechanics"){
			if(currentTutorialIndex == 1){
				PullbackBehavior.pullbackStarted -= nextTutorial;
			}
			else if(currentTutorialIndex == 2){
				TurnFlowController.TurnBeginEvent -= nextTutorial;
			}
			else if(currentTutorialIndex == 3){
				AimPlayerBall.rotateButtonTapped -= nextTutorial;
			}
			else if(currentTutorialIndex == 4){
				GameFlowController.SoloGameEnd -= nextTutorial;
			}
		}
		else if(currentTutorial == "Collect Objective"){
			if(currentTutorialIndex == 1){
			}
			else if(currentTutorialIndex == 2){
			}
			else if(currentTutorialIndex == 3){
			}
		}
	}
	
	public void subscribe(){
		if(currentTutorial == "Launch Mechanics"){
			if(currentTutorialIndex == 1){
				PullbackBehavior.pullbackStarted += nextTutorial;
			}
			else if(currentTutorialIndex == 2){
				TurnFlowController.TurnBeginEvent += nextTutorial;
			}
			else if(currentTutorialIndex == 3){
				AimPlayerBall.rotateButtonTapped += nextTutorial;
			}
			else if(currentTutorialIndex == 4){
				GameFlowController.SoloGameEnd += nextTutorial;
			}
		}
		else if(currentTutorial == "Collect Objective"){
			if(currentTutorialIndex == 1){
				setTutorialScreenTimer(2.2f);
			}
			else if(currentTutorialIndex == 2){
				setTutorialScreenTimer(2.2f);
			}
			else if(currentTutorialIndex == 3){
				setTutorialScreenTimer(2.2f);
			}
		}
	}
	
	void OnDestroy(){
		unsubscribe();
		setCurrentState(State.IDLE);
	}
}
