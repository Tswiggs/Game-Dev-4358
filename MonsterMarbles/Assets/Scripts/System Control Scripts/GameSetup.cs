using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetup {
	
	public bool isSolo;
	
	public List<ZoogiTeam> teamList;
	public ZoogiTeamRoster roster;
	public ObjectiveTrackerInterface objectiveTracker;
	public int threeStarTime;
	public int twoStarTime;
	public int oneStarTime;
	
	public ObjectiveTrackerInterface scoreTracker;
	
	public GameSetup(List<ZoogiTeam> TeamList, ZoogiTeamRoster Roster) {
		teamList = TeamList;
		roster = Roster;
		
		isSolo = true;
		threeStarTime = 3;
		twoStarTime = 6;
		oneStarTime = 0;
	}
	
	public void setObjectiveTracker(ObjectiveTrackerInterface tracker){
		objectiveTracker = tracker;
	}
	
	public bool setStarTimes(int oneStar, int twoStar, int threeStar){
		if(oneStar <= 0 || oneStar >= twoStar && twoStar >= threeStar && threeStar >= 0){
			oneStarTime = oneStar;
			twoStarTime = twoStar;
			threeStarTime = threeStar;
			return true;
		}
		else{
			return false;
		}
	}
	
}
