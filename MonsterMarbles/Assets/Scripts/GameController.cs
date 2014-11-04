using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	//References to every scene
	public MainMenuController mainMenu;
	public RingerController ringerController;



	//private MatchData activeMatches[];
	private AssemblyCSharp.User user;
	//private Settings settings;

	// This Object should be attached to the main menu scene
	void Start () {
		DontDestroyOnLoad (transform.gameObject);

		//Check for saved login data
		login ();

		//initialize the mainmenu controller
		mainMenu.enabled=true;
		mainMenu.passLoginInformation(user);

		//else show login form
	}
	
	public User getUser() {
		return user;
	}

	void Update () {
		
	}

	void onEnable(){

	}
	void onDisable(){

	}

	void loadMatch(){

	}
	public void newMatch(string gameMode, string multiplayerMode, List<PlayerBallCreator.MONSTER_PREFABS> team1, List<PlayerBallCreator.MONSTER_PREFABS> team2, User user1, User user2){
		//TODO: change this so that it checks that the game mode is a valid type and then loads that level.
		if(gameMode==Constants.SCENE_PILA_PLAINS){
			mainMenu.enabled=false;
			Application.LoadLevel(gameMode);

			//Populate the player list
			ArrayList players=new ArrayList();
			if(multiplayerMode=="HOTSEAT"){
				//players.Add(new Player(team1, 0, "Player1"));
				//players.Add(new Player(team2, 0, "Player2"));
			}else{
				//players.Add(new Player(team1, 0,user1.getFbID() )) ;
				//players.Add(new Player(team2, 0, user2.getFbID() ));
			}

			ringerController.initialize(this, multiplayerMode, players);
			ringerController.enabled=true;
		}

	}
	void saveMatch(){

	}

	void loadMenu(){

	}

	void login(){
		//TODO: This is not really logging into anything.  Its just pretending to.
		IList list=new ArrayList();
		list.Add(true);
		list.Add(true);
		user= new AssemblyCSharp.User("12345", "Cat", "Diggity","english","H-Town","nope","Yesterday",
		                              list,542,100);
	}

}
