using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoogiTeam {

	public string teamName;
	public List<Zoogi> teamMembers; 
	
	public ZoogiTeam(){
		teamName = "Player";
		teamMembers = new List<Zoogi>();
	}
	
	public ZoogiTeam(string name){
		teamName = name;
		teamMembers = new List<Zoogi>();	
	}
	
	public ZoogiTeam(string name, List<Zoogi> team){
		teamName = name;
		teamMembers = new List<Zoogi>(team);
		
	}
	
	public bool addTeamMember(Zoogi teamMember){
		teamMembers.Add(teamMember);
		return true;
	}
}
