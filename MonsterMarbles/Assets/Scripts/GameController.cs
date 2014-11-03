using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public MonoBehaviour mainMenu;
	//private MatchData activeMatches[];
	private AssemblyCSharp.User user;
	//private Settings settings;

	// This Object should be attached to the main menu scene
	void Start () {
		DontDestroyOnLoad (transform.gameObject);
		//Check for saved login data
		login ();
		//if login is successfull show game buttons
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
