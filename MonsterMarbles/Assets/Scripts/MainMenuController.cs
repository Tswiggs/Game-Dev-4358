using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class MainMenuController : MonoBehaviour {
	
	// Use this for initialization
	private Camera camera;
	private int interfaceType;
	private int interfaceNavigation;
	
	private const int NO_INTERFACE = 0;
	private const int LOGGED_OUT = 1;
	private const int LOGGED_IN_NO_GAMES = 2;
	private const int LOGGED_IN_GAMES_IN_PROGRESS = 3;

	void Start () {
		interfaceType = LOGGED_IN_NO_GAMES;
		interfaceNavigation = 0;
		
	}

	void OnGUI(){
		
		GUIStyle redStyle = new GUIStyle(GUI.skin.button);
		redStyle.fontSize = 36;
		redStyle.alignment = TextAnchor.MiddleCenter;
		redStyle.hover.textColor = Color.red;
		
		GUI.backgroundColor = new Color (27, 27, 27);
	
		if(interfaceType == NO_INTERFACE){
			
		}
		else if(interfaceType == LOGGED_OUT){
			if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/2) - 60, 220, 120), "Login with Facebook", redStyle)) {
			}
		}
		else if(interfaceType == LOGGED_IN_NO_GAMES){
			
			if(interfaceNavigation == 0){
				if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/2) - 60, 220, 120), "Play!", redStyle)) {
					interfaceNavigation = 1;
				}
				if (GUI.Button (new Rect (Screen.width/8 * 3 - 90, (Screen.height/2) + 100, 180, 90), "Options", redStyle)) {
					interfaceNavigation = 2;
				}
				if (GUI.Button (new Rect (Screen.width/8 * 5 - 90, (Screen.height/2) + 100, 180, 90), "Credits", redStyle)) {
					interfaceNavigation = 3;
				}
			}
			else if (interfaceNavigation == 1){ //Game Mode Menu
				if (GUI.Button (new Rect (Screen.width/2 - 150, (Screen.height/2), 300, 120), "Ringer Royale", redStyle)) {
					interfaceNavigation = 0;
				}
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", redStyle)) {
					interfaceNavigation = 0;
				}
			}
			else if (interfaceNavigation == 2){ //Options Menu
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", redStyle)) {
					interfaceNavigation = 0;
				}
			}
			else if (interfaceNavigation == 3){ //Credits Menu
				if (GUI.Button (new Rect (Screen.width/2 - 90, (Screen.height - 200) + 100, 180, 90), "Return", redStyle)) {
					interfaceNavigation = 0;
				}
			}
		}
		else if(interfaceType == LOGGED_IN_GAMES_IN_PROGRESS){
			if (GUI.Button (new Rect (Screen.width / 2 - 110, (Screen.height/2) - 60, 220, 120), "View Games In Progress", redStyle)) {
				Application.LoadLevel(Constants.SCENE_PILA_PLAINS);
			}
			if (GUI.Button (new Rect (Screen.width/8 * 3 - 60, (Screen.height/2) + 100, 180, 50), "Options", redStyle)) {
				
			}
			if (GUI.Button (new Rect (Screen.width/8 * 5 - 60, (Screen.height/2) + 100, 180, 50), "Credits", redStyle)) {
				
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
	
	// Update is called once per frame
	void Update () {

	}
}
