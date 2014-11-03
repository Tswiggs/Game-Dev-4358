using UnityEngine;
using System.Collections;
using AssemblyCSharp;

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
		mainMenu=new MainMenuController(this);
		mainMenu.passLoginInformation(user);

		//else show login form
	}
	

	void Update () {
		
	}

	void onEnable(){

	}
	void onDisable(){

	}

	void loadMatch(){

	}
	void newMatch(string gameMode, string multiplayerMode, IList team1, IList team2, User user2){
		//TODO: change this so that it checks that the game mode is a valid type and then loads that level.
		if(gameMode==Constants.SCENE_PILA_PLAINS){
			Application.LoadLevel(gameMode);
			Destroy(mainMenu);
			ringerController=new RingerController(multiplayerMode, team1, team2, )
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
