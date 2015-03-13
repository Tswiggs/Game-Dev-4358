using UnityEngine;
using System.Collections;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class CharacterSelectMenuController : MonoBehaviour {
	
	public MainMenuController mainMenu;
	
	private int maxTeamSize;
	private int numberOfTeams;
	
	private int currentPlayer;
	
	private List<PlayerBallCreator.MONSTER_PREFABS> player0;
	private List<PlayerBallCreator.MONSTER_PREFABS> player1;
	
	private bool[] charactersSelected;
	private int numberOfSelectedCharacters;
	
	// Use this for initialization
	void Awake () {
		currentPlayer = 0;
		
		maxTeamSize = 3;
		
		numberOfSelectedCharacters = 0;
		
		player0 = new List<PlayerBallCreator.MONSTER_PREFABS>();
		player1 = new List<PlayerBallCreator.MONSTER_PREFABS>();
		
		charactersSelected = new bool[30];
		for(int x = 0; x <30; x++){
			charactersSelected[x] = false;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if (mainMenu.getSelectedMultiplayerMode() == "HOTSEAT"){
			numberOfTeams = 2;
		}
		else{
			numberOfTeams = 1;
		}
	}
	
	public void attemptToToggle(ToggleButtonController script){
		if (!script.IsToggled){
			if (selectCharacter(script.index)){
				script.toggle();
			}
		}
		else{
			if (deselectCharacter(script.index)){
				script.toggle();
			}
		}
	}
	
	bool selectCharacter(int index){
		if(numberOfSelectedCharacters < maxTeamSize){
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
}
