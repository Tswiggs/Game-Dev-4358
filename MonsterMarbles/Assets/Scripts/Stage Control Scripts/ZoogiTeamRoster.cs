using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoogiTeamRoster {
	
	public List<List<GameObject>> teamsRoster;
	
	private ZoogiAssembler zoogiAssembler;
	// Use this for initialization
	public ZoogiTeamRoster(int numberOfTeams = 2){
		if( numberOfTeams >0){
			teamsRoster = new List<List<GameObject>>(numberOfTeams);
			for(int x = 0; x < numberOfTeams; x++){
				teamsRoster.Add(new List<GameObject>());
			}
		}
		else{
			teamsRoster = new List<List<GameObject>>(1);
			teamsRoster.Add(new List<GameObject>());
		}
	}
	
	public ZoogiTeamRoster(List<List<GameObject>> roster){
		teamsRoster = roster;
	}
	
	public bool addZoogiToTeam(int teamIndex, GameObject zoogi){
		if(teamIndex == 0){
			zoogi.GetComponent<TeamAffiliation>().setCurrentState(TeamAffiliation.State.RED);
		}
		else if(teamIndex == 1){
			zoogi.GetComponent<TeamAffiliation>().setCurrentState(TeamAffiliation.State.BLUE);
		}
		else{
			zoogi.GetComponent<TeamAffiliation>().setCurrentState(TeamAffiliation.State.NONE);
		}
		teamsRoster[teamIndex].Add(zoogi);
		return true;
	}
	
	public GameObject getZoogi(int teamIndex, int zoogiIndex){
		return teamsRoster[teamIndex][zoogiIndex];
	}
}
