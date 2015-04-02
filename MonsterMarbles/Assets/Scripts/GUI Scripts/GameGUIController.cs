using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameGUIController : MonoBehaviour {
	
	public MenuManager manager;
	
	private static GameGUIController guiController;
	
	public enum State{INACTIVE, LAUNCH, MAP};
	private State currentState;
	
	private List<int> scoreList;
	private int playerScoreIndex = 0;
	
	public delegate void stateChange();
	public static event stateChange changeToLaunchState;
	public static event stateChange changeToMapState;
	
	// Use this for initialization
	void Start () {
		manager = GetComponent<MenuManager>();
		scoreList = new List<int>(3);
		for(int x = 0; x < 3; x++){
			scoreList.Add(0);
		}
		currentState = State.INACTIVE;
		guiController = this;
		
		RoundScoreTracker.playerScoreChange += playerScoreChange;
		GameFlowController.PlayerChangeEvent += playerChange;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public static bool setCurrentState(State newState){
		
		if(guiController.currentState == State.LAUNCH){
			
		}
		
		guiController.currentState = newState;
		
		if(newState == State.INACTIVE){
			guiController.manager.ShowMenu(guiController.transform.FindChild("Inactive Menu").GetComponent<Menu>());
		}
		else if(newState == State.MAP){
			guiController.manager.ShowMenu(guiController.transform.FindChild("Map Menu").GetComponent<Menu>());
			if( changeToMapState != null){
				changeToMapState();
			}
		}
		else if(newState == State.LAUNCH){
			guiController.manager.ShowMenu(guiController.transform.FindChild("Launch Menu").GetComponent<Menu>());
			if( changeToLaunchState != null){
				changeToLaunchState();
			}
		}
		
		return true;
	}
	
	public void changeToLaunch(){
		GameGUIController.setCurrentState(State.LAUNCH);
	}
	
	public void changeToMap(){
		GameGUIController.setCurrentState(State.MAP);
	}
	
	public void playerScoreChange(int score, int player){
		scoreList[player] = score;
		if(player == playerScoreIndex){
			refreshGUIScore();
		}
	}
	
	public void playerChange(int index){
		playerScoreIndex = index;
		refreshGUIScore();
	}
	
	private void refreshGUIScore(){
		transform.FindChild("Always Shown Menu").FindChild("Score Panel").GetChild(0).FindChild("Skybit Image").GetChild(0).gameObject.GetComponent<Text>().text = scoreList[playerScoreIndex].ToString();
	}
}
