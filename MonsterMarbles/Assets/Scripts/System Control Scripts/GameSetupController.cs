using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetupController : MonoBehaviour {
	
	public List<ZoogiTeam> teamList;
	public GameFlowController gameFlowController;
	
	private ZoogiTeamRoster teamRoster;
	
	// Use this for initialization
	void Start () {
		Transform startingParameters = gameObject.transform.Find("Starting Parameters");
		if(startingParameters == null){ //No starting parameters found, create generic starting parameters
			assignDefaultStartingParameters();
		}
		initialize();
	}
	
	//Setups up all controllers that are dependent on GameSetup, like the GameFlowController
	void initialize(){
		gameFlowController.initialize(teamList, teamRoster);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void assignDefaultStartingParameters(){
		teamList = new List<ZoogiTeam>();
		ZoogiTeam team1 = new ZoogiTeam("Player 1");
		team1.addTeamMember(new Zoogi(0));
		team1.addTeamMember(new Zoogi(1));
		team1.addTeamMember(new Zoogi(2));
		teamList.Add(team1);
		
		ZoogiTeam team2 = new ZoogiTeam("Player 2");
		team2.addTeamMember(new Zoogi(3));
		team2.addTeamMember(new Zoogi(2));
		team2.addTeamMember(new Zoogi(0));
		teamList.Add(team2);
		
		teamRoster = createTeamRoster(teamList);
	}
	
	public static ZoogiTeamRoster createTeamRoster(List<ZoogiTeam> teamList){
		
		ZoogiTeamRoster teamRoster = new ZoogiTeamRoster(teamList.Count);
		
		int x = 0;
		foreach( ZoogiTeam team in teamList){
			foreach( Zoogi zoogi in team.teamMembers){
				teamRoster.addZoogiToTeam(x, ZoogiAssembler.instantiateZoogi(new Vector3(0,-5,0), Quaternion.identity, zoogi.model, zoogi.script));
			}
			x++;
		}
		
		return teamRoster;
	}
}
