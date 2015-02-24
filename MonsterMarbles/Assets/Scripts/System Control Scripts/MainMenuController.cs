using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {
	
	// Use this for initialization
	private Camera camera;
	public GameController gameController;
	public CharacterSelectController characterSelectController;
	private UserInterface interfaceType;
	private int interfaceNavigation;
	
	private GUIStyle mainMenuStyle;
	public GUITexture title;
	
	private string selectedGameMode = Constants.SCENE_PILA_PLAINS;
	private string selectedMultiplayerMode;
	private List<PlayerBallCreator.MONSTER_PREFABS> selectedTeam1Roster;
	private List<PlayerBallCreator.MONSTER_PREFABS> selectedTeam2Roster;
	
	
	private enum UserInterface {
		NO_INTERFACE,LOGGED_OUT,LOGGED_IN_NO_GAMES,LOGGED_IN_GAMES_IN_PROGRESS
	}
	

	void Start () {
		
		Constants.setupConstants();
		
		interfaceType = UserInterface.LOGGED_IN_NO_GAMES;
		interfaceNavigation = 0;
		mainMenuStyle = GUIStyles.getMainMenuStyle();
		
	}
	
	void OnEnable() {
		interfaceNavigation = 0;
		title.enabled = true;
	}
	
	void OnDisable() {
		if(title != null){
			title.enabled = false;
		}
	}

	void OnGUI(){
		
		
		GUI.backgroundColor = new Color (27, 27, 27);
		
		
		if(interfaceType == UserInterface.NO_INTERFACE){
			
		}
		else if(interfaceType == UserInterface.LOGGED_OUT){
			if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/2) - 60, 220, 120), "Login with Facebook", mainMenuStyle)) {
			}
		}
		else if(interfaceType == UserInterface.LOGGED_IN_NO_GAMES){
			
			if(interfaceNavigation == 0){
				if (GUI.Button (new Rect (Screen.width / 2 - 150, ((Screen.height/5)*3) - 80, 300, 160), "Play!", mainMenuStyle)) {
					interfaceNavigation = 1;
				}
				if (GUI.Button (new Rect (Screen.width/2 - 90, ((Screen.height/5)*3) + 100, 180, 90), "Options", mainMenuStyle)) {
					interfaceNavigation = 2;
				}
				if (GUI.Button (new Rect (Screen.width/2 - 90, ((Screen.height/5)*3) + 200, 180, 90), "Credits", mainMenuStyle)) {
					interfaceNavigation = 3;
				}
			}
			else if (interfaceNavigation == 1){ //Game Mode Menu
				if (GUI.Button (new Rect (Screen.width / 2 - 150, ((Screen.height/5)*3) - 90, 300, 140), "Pila Plains", mainMenuStyle)) {
					selectedGameMode = Constants.SCENE_PILA_PLAINS;
					characterSelectController.enabled = true;
					this.enabled = false;
				}
				if (GUI.Button (new Rect (Screen.width / 2 - 150, ((Screen.height/5)*3) + 50, 300, 140), "Frostwind\n Mountain", mainMenuStyle)) {
					selectedGameMode = Constants.SCENE_FROSTWIND_MOUNTAIN;
					characterSelectController.enabled = true;
					this.enabled = false;
				}
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 110, 180, 90), "Return", mainMenuStyle)) {
					interfaceNavigation = 0;
				}
			}
			else if (interfaceNavigation == 2){ //Options Menu
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", mainMenuStyle)) {
					interfaceNavigation = 0;
				}
			}
			else if (interfaceNavigation == 3){ //Credits Menu
			
				GUIStyle creditsStyle = GUIStyles.getTextDisplayStyle();
			
				GUI.Label (new Rect(Screen.width/5, Screen.height/2+50, Screen.width/5*3, Screen.height/4), "Produced by Little Fish\nMusic by Kevin MacLeod", creditsStyle);
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", mainMenuStyle)) {
					interfaceNavigation = 0;
				}
			}
		}
		else if(interfaceType == UserInterface.LOGGED_IN_GAMES_IN_PROGRESS){
			if (GUI.Button (new Rect (Screen.width / 2 - 150, (Screen.height/5*3) - 90, 300, 180), "View Games In Progress", mainMenuStyle)) {
				interfaceNavigation = 1;
			}
			if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height/5*3) + 100, 180, 90), "Options", mainMenuStyle)) {
				interfaceNavigation = 2;
			}
			if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height/5*3) + 200, 180, 90), "Credits", mainMenuStyle)) {
				interfaceNavigation = 3;
			}
		}  
		
	}

	public void passLoginInformation(User loginInformation)
	{
		if (loginInformation == null) {
			interfaceType = UserInterface.LOGGED_OUT;
		}
		else {
			//Process to see if login information shows games in progress
			//interfaceType = UserInterface.LOGGED_IN_GAMES_IN_PROGRESS
			//else
			interfaceType = UserInterface.LOGGED_IN_NO_GAMES;
		}
	}
	
	public void sendTeamDetails(string multiplayerMode, List<PlayerBallCreator.MONSTER_PREFABS> team1, List<PlayerBallCreator.MONSTER_PREFABS> team2){
		selectedMultiplayerMode = multiplayerMode;
		selectedTeam1Roster = team1;
		selectedTeam2Roster = team2;
	}
	
	public void newMatch(){
		if(selectedTeam2Roster != null){
			gameController.newMatch(selectedGameMode,selectedMultiplayerMode,selectedTeam1Roster,selectedTeam2Roster,gameController.getUser(),gameController.getUser());
		}
		else {
			gameController.newMatch(selectedGameMode,selectedMultiplayerMode,selectedTeam1Roster,selectedTeam1Roster,gameController.getUser(),gameController.getUser());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Debug.isDebugBuild){
			if(Input.GetKeyDown("space")){
			
				selectedGameMode = Constants.SCENE_PILA_PLAINS;
				
				List<PlayerBallCreator.MONSTER_PREFABS> team1 = new List<PlayerBallCreator.MONSTER_PREFABS>();
				team1.Add(PlayerBallCreator.MONSTER_PREFABS.WOLFGANG);
				
				selectedTeam1Roster = team1;
				
				List<PlayerBallCreator.MONSTER_PREFABS> team2 = new List<PlayerBallCreator.MONSTER_PREFABS>();
				team2.Add(PlayerBallCreator.MONSTER_PREFABS.HOTSTREAK);
				
				selectedTeam2Roster = team2;
				
				selectedMultiplayerMode = "HOTSEAT";
				
				newMatch();
			}
		}
		
		if(Input.GetKeyDown("escape"){
			Application.Quit();
		}
	}
}
