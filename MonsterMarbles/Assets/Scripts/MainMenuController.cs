using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class MainMenuController : MonoBehaviour {
	
	// Use this for initialization
	private Camera camera;
	public GameController gameController;
	public CharacterSelectController characterSelectController;
	private int interfaceType;
	private int interfaceNavigation;
	
	public GUIStyle mainMenuStyle;
	public GUITexture title;
	
	private const int NO_INTERFACE = 0;
	private const int LOGGED_OUT = 1;
	private const int LOGGED_IN_NO_GAMES = 2;
	private const int LOGGED_IN_GAMES_IN_PROGRESS = 3;

	void Start () {

		interfaceType = LOGGED_IN_NO_GAMES;
		interfaceNavigation = 0;
		
	}
	
	void OnEnable() {
		interfaceNavigation = 0;
		title.enabled = true;
	}
	
	void OnDisable() {
		title.enabled = false;
	}

	void OnGUI(){
		
		
		GUI.backgroundColor = new Color (27, 27, 27);
		
		
		if(interfaceType == NO_INTERFACE){
			
		}
		else if(interfaceType == LOGGED_OUT){
			if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/2) - 60, 220, 120), "Login with Facebook", mainMenuStyle)) {
			}
		}
		else if(interfaceType == LOGGED_IN_NO_GAMES){
			
			if(interfaceNavigation == 0){
				if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/2) - 60, 220, 120), "Play!", mainMenuStyle)) {
					interfaceNavigation = 1;
				}
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height/2) + 100, 180, 90), "Options", mainMenuStyle)) {
					interfaceNavigation = 2;
				}
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height/2) + 200, 180, 90), "Credits", mainMenuStyle)) {
					interfaceNavigation = 3;
				}
			}
			else if (interfaceNavigation == 1){ //Game Mode Menu
				if (GUI.Button (new Rect (Screen.width/2 - 150, (Screen.height/2), 300, 120), "Ringer Royale", mainMenuStyle)) {
					characterSelectController.enabled = true;
					this.enabled = false;
				}
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", mainMenuStyle)) {
					interfaceNavigation = 0;
				}
			}
			else if (interfaceNavigation == 2){ //Options Menu
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", mainMenuStyle)) {
					interfaceNavigation = 0;
				}
			}
			else if (interfaceNavigation == 3){ //Credits Menu
			
				GUIStyle creditsStyle = new GUIStyle();
				creditsStyle.alignment = TextAnchor.UpperCenter;
				creditsStyle.fontSize = 28;
				creditsStyle.normal.textColor = Color.black;
			
				GUI.Label (new Rect(Screen.width/5, Screen.height/2+50, Screen.width/5*3, Screen.height/4), "Produced by Pixel Rocket\nSTART Music by Kevin MacLeod", creditsStyle);
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", mainMenuStyle)) {
					interfaceNavigation = 0;
				}
			}
		}
		else if(interfaceType == LOGGED_IN_GAMES_IN_PROGRESS){
			if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/2) - 60, 220, 120), "View Games In Progress", mainMenuStyle)) {
				Application.LoadLevel(Constants.SCENE_PILA_PLAINS);
			}
			if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height/2) + 100, 180, 90), "Options", mainMenuStyle)) {
			
			}
			if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height/2) + 200, 180, 90), "Credits", mainMenuStyle)) {
			
			}
		}  
		
	}

	public void passLoginInformation(User loginInformation)
	{
		if (loginInformation == null) {
			interfaceType = LOGGED_OUT;
		}
		else {
			//Process to see if login information shows games in progress
			//interfaceType = LOGGED_IN_GAMES_IN_PROGRESS
			//else
			interfaceType = LOGGED_IN_NO_GAMES;
		}
	}
	
	public void newMatch(string gameMode, string multiplayerMode, List<PlayerBallCreator.MONSTER_PREFABS> team1, List<PlayerBallCreator.MONSTER_PREFABS> team2){
		if(team2 != null){
			gameController.newMatch(gameMode,multiplayerMode,team1,team2,gameController.getUser(),gameController.getUser());
		}
		else {
			gameController.newMatch(gameMode,multiplayerMode,team1,team1,gameController.getUser(),gameController.getUser());
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
