using UnityEngine;
//using UnityEditor;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class CharacterSelectController : MonoBehaviour {
	
	public MainMenuController mainMenuController;
	
	private Vector2 scrollPosition;
	private bool[] unlockedCharacters;
	private bool[] charactersSelected;
	
	private int maxNumberOfCharacters;
	private int numberOfSelectedCharacters;
	
	private GUIStyle characterSelectStyle;
	private GUIStyle invertedCharacterSelectStyle;
	private GUIStyle mainMenuStyle;
	
	private Texture[] characterPortraits;
	
	private int interfaceNavigation;
	private int mode;
	private int currentPlayer;
	
	private List<PlayerBallCreator.MONSTER_PREFABS> player0;
	private List<PlayerBallCreator.MONSTER_PREFABS> player1;
	
	private const int CHARACTER_SELECT_BOX_WIDTH = 180;
	private const int CHARACTER_SELECT_BOX_HEIGHT = 180;
	
	private const int HOTSEAT_MODE = 0;
	private const int INTERNET_MODE = 1;
	
	public Texture unknownPortrait;
	public Texture wolfgangPortrait;
	public Texture hotstreakPortrait;
	public Texture larsPortrait;
	
	// Use this for initialization
	void Start () {
	
		mainMenuStyle = GUIStyles.getMainMenuStyle();
		characterSelectStyle = GUIStyles.getCharacterSelectStyle();
		invertedCharacterSelectStyle = GUIStyles.getInvertedCharacterSelectStyle();
	
		interfaceNavigation = 0;
		maxNumberOfCharacters = 1;
		numberOfSelectedCharacters = 0;
		
		currentPlayer = 0;
		
		player0 = new List<PlayerBallCreator.MONSTER_PREFABS>();
		player1 = new List<PlayerBallCreator.MONSTER_PREFABS>();
		
		mode = HOTSEAT_MODE;
		
		unlockedCharacters = new bool[30];
		charactersSelected = new bool[30];
		characterPortraits = new Texture[30];
		for(int x = 0; x <18; x++){
			unlockedCharacters[x] = true;
			charactersSelected[x] = false;
		}
		unlockedCharacters[0] = true;
		unlockedCharacters[1] = true;
		unlockedCharacters[2] = true;
		//unlockedCharacters[3] = true;
		
		/*characterPortraits[0] = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Box_Assets/Pixel Rocket/Textures/GUI Textures/Wolfgang_Portrait.png", typeof(Texture));
		characterPortraits[1] = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Box_Assets/Pixel Rocket/Textures/GUI Textures/Hotstreak_Portrait.png", typeof(Texture));
		characterPortraits[2] = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Temporary Assets/Lars_Portrait.png", typeof(Texture));
		
		unknownPortrait = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Box_Assets/Pixel Rocket/Textures/GUI Textures/Unknown_Portrait.png", typeof(Texture));
		*/
		characterPortraits[0] = wolfgangPortrait;
		characterPortraits[1] = hotstreakPortrait;
		characterPortraits[2] = larsPortrait;
		
	}
	
	void OnEnable() {
		interfaceNavigation = 0;
		maxNumberOfCharacters = 1;
		numberOfSelectedCharacters = 0;
		
		currentPlayer = 0;
		
		player0 = new List<PlayerBallCreator.MONSTER_PREFABS>();
		player1 = new List<PlayerBallCreator.MONSTER_PREFABS>();
		
		mode = HOTSEAT_MODE;
		
		unlockedCharacters = new bool[30];
		charactersSelected = new bool[30];
		for(int x = 0; x <30; x++){
			unlockedCharacters[x] = true;
			charactersSelected[x] = false;
		}
		//unlockedCharacters[0] = true;
		//unlockedCharacters[1] = true;
		//unlockedCharacters[2] = true;
	}
	
	bool selectCharacter(int index){
		if(numberOfSelectedCharacters < maxNumberOfCharacters){
			if(charactersSelected[index] != true){
				if(index < 3){
					charactersSelected[index] = true;
					numberOfSelectedCharacters += 1;
				
					if(currentPlayer == 0){
						player0.Add((PlayerBallCreator.MONSTER_PREFABS)index);
					}
					else{
						player1.Add((PlayerBallCreator.MONSTER_PREFABS)index);
					}
					return true;
				}
			}
		}
		return false;
	}
	
	bool deselectCharacter(int index){
		if(numberOfSelectedCharacters != 0){
			if(charactersSelected[index] == true){
				charactersSelected[index] = false;
				numberOfSelectedCharacters -= 1;
				
				
				if(currentPlayer == 0){
					return player0.Remove((PlayerBallCreator.MONSTER_PREFABS)index);
				}
				else{
					return player1.Remove((PlayerBallCreator.MONSTER_PREFABS)index);
				}
				
			}
		}
		return false;
	}
	
	void playerReady(){
		if(mode == HOTSEAT_MODE){
			if(currentPlayer == 0){
				currentPlayer = 1;
				
				numberOfSelectedCharacters = 0;
				
				for(int x = 0; x <30; x++){
					charactersSelected[x] = false;
				}
			}
			else if(currentPlayer == 1){
				this.enabled = false;
				mainMenuController.newMatch(Constants.SCENE_PILA_PLAINS,"HOTSEAT",player0,player1);
			}
		}
		else{
		this.enabled = false;
			mainMenuController.newMatch(Constants.SCENE_PILA_PLAINS,"HOTSEAT",player0,player0);
		}
	}
	
	void OnGUI(){
		
		GUIStyle redStyle = new GUIStyle(GUI.skin.box);
		redStyle.fontSize = 28;
		redStyle.alignment = TextAnchor.MiddleCenter;
		redStyle.normal.textColor = Color.red;
		
		GUI.backgroundColor = new Color (27, 27, 27);
		
		if(mode == HOTSEAT_MODE){
			string text;
			if(currentPlayer == 0){
				text = "PLAYER 1: SELECT YOUR ZOOGIS";
			}
			else{
				text = "PLAYER 2: SELECT YOUR ZOOGIS";
			}
			
			GUI.Label(new Rect(0,10,Screen.width,50), text, redStyle);
		}
		
		
		float scrollBoxWidth = Mathf.Floor(((Screen.width - Screen.width/4)/CHARACTER_SELECT_BOX_WIDTH)) * CHARACTER_SELECT_BOX_WIDTH; 
		
		scrollPosition = GUI.BeginScrollView(new Rect(Screen.width/10, Screen.height/8, Screen.width - Screen.width/5, Screen.height-Screen.height/3),scrollPosition,new Rect(0,0, scrollBoxWidth, Screen.height*2),false,false);
		int xPos = 0;
		int yPos = 0;
		
		for(int num =0; num < 30; num++){
			if(unlockedCharacters[num] == true){
				if(xPos >= (scrollBoxWidth)){
					xPos = 0;
					yPos+= CHARACTER_SELECT_BOX_HEIGHT;
				}
				
				Texture portrait;
				if(characterPortraits[num] == null){
					portrait = unknownPortrait;
				}
				else{
					portrait = characterPortraits[num];
				}
				
				if(charactersSelected[num] == true){
					if (GUI.Button(new Rect(xPos,yPos,CHARACTER_SELECT_BOX_WIDTH,CHARACTER_SELECT_BOX_HEIGHT), new GUIContent(Constants.CHARACTER_INDEX[num], portrait), invertedCharacterSelectStyle)){
						deselectCharacter(num);
					}
				}
				else if (GUI.Button(new Rect(xPos,yPos,CHARACTER_SELECT_BOX_WIDTH,CHARACTER_SELECT_BOX_HEIGHT), new GUIContent(Constants.CHARACTER_INDEX[num], portrait), characterSelectStyle)){
					selectCharacter(num);
				}
				xPos += CHARACTER_SELECT_BOX_WIDTH;
			}
		
		}
		GUI.EndScrollView();
		
		if(numberOfSelectedCharacters == maxNumberOfCharacters){
			if(GUI.Button (new Rect (Screen.width/2 - 190, (Screen.height - 200) + 100, 180, 90), "Ready!", mainMenuStyle)) {
				playerReady();
			}
			
			if (GUI.Button (new Rect (Screen.width/2 +10, (Screen.height - 200) + 100, 180, 90), "Return", mainMenuStyle)) {
				mainMenuController.enabled = true;
				this.enabled = false;
			}
		}
		else {
			if (GUI.Button (new Rect (Screen.width/2 -90, (Screen.height - 200) + 100, 180, 90), "Return", mainMenuStyle)) {
				mainMenuController.enabled = true;
				this.enabled = false;
			}
		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
