using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetupController : MonoBehaviour {
	
	public List<ZoogiTeam> teamList;
	public GameFlowController gameFlowController;
	
	public static GameSetupController gameSetupController;
	
	public GameObject wolfgangModel;
	public GameObject wolfgangScripts;
	public GameObject hotstreakModel;
	public GameObject hotstreakScripts;
	public GameObject larsModel;
	public GameObject larsScripts;
	
	public GameSetup setupParameters;
	
	public bool isSolo = true;
	public int perfectTime;
	public int goodTime;
	public ObjectiveTrackerInterface scoreTracker;
	
	private ZoogiTeamRoster teamRoster;
	
	// Use this for initialization
	void Start () {
		gameSetupController = this;
		Transform startingParameters = gameObject.transform.Find("Starting Parameters");
		if(startingParameters == null){ //No starting parameters found, create generic starting parameters
			assignDefaultStartingParameters();
		}
		initialize();
	}
	
	//Setups up all controllers that are dependent on GameSetup, like the GameFlowController
	void initialize(){
		gameFlowController.initialize(setupParameters);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void assignDefaultStartingParameters(){
		teamList = new List<ZoogiTeam>();
		if(Application.loadedLevelName == Constants.SCENE_WOLFGANG_1 || Application.loadedLevelName == Constants.SCENE_WOLFGANG_2){
			ZoogiTeam team1 = new ZoogiTeam("Player 1");
			team1.addTeamMember(new Zoogi(0));
			teamList.Add(team1);
		}
		else if(Application.loadedLevelName == Constants.SCENE_HOTSTREAK_1){
			ZoogiTeam team1 = new ZoogiTeam("Player 1");
			team1.addTeamMember(new Zoogi(1));
			teamList.Add(team1);
		}
		else{
			ZoogiTeam team1 = new ZoogiTeam("Player 1");
			team1.addTeamMember(new Zoogi(2));
			team1.addTeamMember(new Zoogi(1));
			team1.addTeamMember(new Zoogi(0));
			teamList.Add(team1);
			
			ZoogiTeam team2 = new ZoogiTeam("Player 2");
			team2.addTeamMember(new Zoogi(3));
			//team2.addTeamMember(new Zoogi(2));
			//team2.addTeamMember(new Zoogi(0));
			//teamList.Add(team2);
		}
		
		teamRoster = createTeamRoster(teamList);
		
		setupParameters = new GameSetup(teamList, teamRoster);
		setupParameters.isSolo = true;
		if(Application.loadedLevelName == Constants.SCENE_HOTSTREAK_1){
			setupParameters.setStarTimes(0,9,6);
		}
		else{
			setupParameters.setStarTimes(0,6,3);
		}
		scoreTracker = new ShipCollectSoloScoreTracker(ShipCollectSoloScoreTracker.CollectObject.SKY_BIT,5,true,setupParameters.oneStarTime,setupParameters.twoStarTime,setupParameters.threeStarTime);
		setupParameters.setObjectiveTracker(scoreTracker);
	}
	
	public static ZoogiTeamRoster createTeamRoster(List<ZoogiTeam> teamList){
		
		ZoogiTeamRoster teamRoster = new ZoogiTeamRoster(teamList.Count);
		
		int x = 0;
		foreach( ZoogiTeam team in teamList){
			foreach( Zoogi zoogi in team.teamMembers){
				//TODO: I'm a fucking fucker for doing it this way, even temporarilly, fuck
				if(zoogi.designation == 0){
					teamRoster.addZoogiToTeam(x, ZoogiAssembler.instantiateZoogi(new Vector3(0,-5,0), Quaternion.identity, gameSetupController.wolfgangModel, gameSetupController.wolfgangScripts));		
				}
				else if(zoogi.designation == 1){
					teamRoster.addZoogiToTeam(x, ZoogiAssembler.instantiateZoogi(new Vector3(0,-5,0), Quaternion.identity, gameSetupController.hotstreakModel, gameSetupController.hotstreakScripts));		
				}
				else if(zoogi.designation == 2){
					teamRoster.addZoogiToTeam(x, ZoogiAssembler.instantiateZoogi(new Vector3(0,-5,0), Quaternion.identity, gameSetupController.larsModel, gameSetupController.larsScripts));		
				}
				else {
					teamRoster.addZoogiToTeam(x, ZoogiAssembler.instantiateZoogi(new Vector3(0,-5,0), Quaternion.identity, zoogi.model, zoogi.script));
				}
			}
			x++;
		}
		
		return teamRoster;
	}
}
