using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameGUIController : MonoBehaviour {
	
	public MenuManager manager;
	
	private static GameGUIController guiController;
	
	public enum State{INACTIVE, LAUNCH, MAP, NOTICE, OPTIONS, END_GAME_SOLO, PREVIOUS_STATE};
	private State currentState;
	
	private State previousState = State.INACTIVE;
	
	private List<int> scoreList;
	private int playerScoreIndex = 0;
	
	private GameObject informationPanel;
	private Text informationText;
	private float informationShowTime = 2f;
	private float informationShowTimer = 0f;
	private bool showInformation = false;
	
	private Text noticeText;
	
	public delegate void stateChange();
	public static event stateChange changeToLaunchState;
	public static event stateChange changeToMapState;
	public static event stateChange changeToNoticeState;
	public static event stateChange changeToOptionsState;
	public static event stateChange changeToEndGameSoloState;
	
	public delegate void performAction();
	public static event performAction retryLevel;
	public static event performAction nextLevel;
	
	// Use this for initialization
	void Start () {
		manager = GetComponent<MenuManager>();
		scoreList = new List<int>(3);
		for(int x = 0; x < 3; x++){
			scoreList.Add(0);
		}
		currentState = State.INACTIVE;
		guiController = this;
		
		informationPanel = transform.FindChild ("Always Shown Menu").FindChild("Information Spacing Panel").FindChild("Information Panel").FindChild("Information Box").gameObject;
		informationText = informationPanel.transform.GetChild(0).GetComponent<Text>();
		
		noticeText = transform.FindChild ("Notice Menu").FindChild ("Information Panel").FindChild("Information").GetChild(0).GetComponent<Text>();
		
		RoundScoreTracker.playerScoreChange += playerScoreChange;
		GameFlowController.PlayerChangeEvent += playerChange;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		refreshGUIScore();
		
		if(showInformation){
			if(Input.GetMouseButtonDown(0) || (Input.touchCount > 0)){
				informationShowTimer = informationShowTime;
			}
		
			if(informationShowTimer < informationShowTime){
				informationShowTimer += Time.deltaTime;
			}
			else{
				guiController.informationPanel.GetComponent<Image>().CrossFadeAlpha(0f, 1.0f, false);
				informationText.CrossFadeAlpha(0f,1.0f,false);
				showInformation = false;
			}
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public static bool setCurrentState(State newState){
		
		if(guiController.currentState == State.LAUNCH){
			
		}
		else if(guiController.currentState == State.NOTICE){
			Time.timeScale = 1;
		}
		else if(guiController.currentState == State.OPTIONS){
			Time.timeScale = 1;
		}
		
		State oldState = guiController.currentState;
		
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
		else if(newState == State.NOTICE){
			Time.timeScale = 0;
			guiController.manager.ShowMenu(guiController.transform.FindChild("Notice Menu").GetComponent<Menu>());
			if( changeToNoticeState != null){
				changeToNoticeState();
			}
		}
		else if(newState == State.OPTIONS){
			Time.timeScale = 0;
			guiController.manager.ShowMenu(guiController.transform.FindChild("Options Menu").GetComponent<Menu>());
			if( changeToOptionsState != null){
				changeToOptionsState();
			}
		}
		else if(newState == State.END_GAME_SOLO){
			guiController.manager.ShowMenu(guiController.transform.FindChild("End Game Solo Menu").GetComponent<Menu>());
			if( changeToEndGameSoloState != null){
				changeToEndGameSoloState();
			}
		}
		else if(newState == State.PREVIOUS_STATE){
			setCurrentState(guiController.previousState);
		}
		
		if(oldState != State.PREVIOUS_STATE){
			guiController.previousState = oldState;
		}
		
		return true;
	}
	
	public static void showInformationPanel(string text, float time){
		guiController.informationShowTime = time;
		guiController.informationShowTimer = 0;
		guiController.showInformation = true;
		guiController.informationText.text = text;
		guiController.informationPanel.SetActive(true);
		
		//guiController.informationText.CrossFadeAlpha(1.0f,1.0f,false);
		guiController.informationText.GetComponent<CanvasRenderer>().SetAlpha(1f);
		//guiController.informationPanel.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, false);
		guiController.informationPanel.GetComponent<CanvasRenderer>().SetAlpha(1f);
	}
	
	public static void showNoticePanel(string text){
		guiController.noticeText.text = text;
		GameGUIController.setCurrentState(State.NOTICE);
	}
	
	public static void showTutorial(string name, int index){
		guiController.transform.FindChild ("Always Shown Menu").FindChild("Tutorial").FindChild(name).FindChild(index.ToString()).gameObject.SetActive(true);
	}
	
	public static void hideTutorial(string name, int index){
		guiController.transform.FindChild ("Always Shown Menu").FindChild("Tutorial").FindChild(name).FindChild(index.ToString()).gameObject.SetActive(false);
	}
	
	public void changeToOptions(){
		GameGUIController.setCurrentState(State.OPTIONS);
	}
	
	public void changeToLaunch(){
		GameGUIController.setCurrentState(State.LAUNCH);
	}
	
	public void changeToMap(){
		GameGUIController.setCurrentState(State.MAP);
	}
	
	public void changeToEndGameSolo(){
		GameGUIController.setCurrentState(State.END_GAME_SOLO);
	}
	
	public void changeToPrevious() {
		GameGUIController.setCurrentState(State.PREVIOUS_STATE);
	}
	
	public void performRetryLevel() {
		if(retryLevel != null){
			retryLevel();
		}
	}
	
	public void performNextLevel() {
		if(nextLevel != null){
			nextLevel();
		}
	}
	
	public void performLoadLevel(string name){
		Application.LoadLevel(name);
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
	
	public void refreshScore(GameObject zoogi){
		refreshGUIScore();
	}
	
	private void refreshGUIScore(){
		transform.FindChild("Always Shown Menu").FindChild("Spacing Panel").FindChild("Score Panel").GetChild(0).FindChild("Skybit Image").GetChild(0).gameObject.GetComponent<Text>().text = GameFlowController.Settings.objectiveTracker.getPlayerScoreString(playerScoreIndex);
	}
	
	void OnDestroy(){
		RoundScoreTracker.playerScoreChange -= playerScoreChange;
		GameFlowController.PlayerChangeEvent -= playerChange;
	}
}
