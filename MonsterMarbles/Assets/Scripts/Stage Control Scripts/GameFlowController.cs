﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlowController : MonoBehaviour {
	
	public List<ZoogiTeam> teamList;
	
	public ZoogiTeamRoster teamRoster;
	
	private TurnFlowController turnFlowController;
	private ZoogiSpawnPointHandler zoogiSpawnPointHandler;
	private RoundScoreTracker scoreTracker;
	
	public enum State {TAKING_TURN, WAITING};
	private State currentState;
	
	public delegate void stateChange(int index);
	public static event stateChange PlayerChangeEvent;
	public static event stateChange RoundBeginEvent;
	public static event stateChange RoundEndEvent;
	
	private int currentTeamIndex;
	private int currentZoogiIndex;
	
	// Use this for initialization
	void Start () {
		enabled = false;
		currentTeamIndex = 0;
		currentZoogiIndex = 0;
	}
	
	public void initialize( List<ZoogiTeam> TeamList, ZoogiTeamRoster roster){
		teamList = TeamList;
		enabled = true;
		currentState = State.WAITING;
		
		teamRoster = roster;
		
		turnFlowController = gameObject.AddComponent<TurnFlowController>();
		turnFlowController.enabled = false;
		
		scoreTracker = new RoundScoreTracker(teamList.Count,3,0);
		
		TurnFlowController.TurnEndEvent += turnEnded;
		
		zoogiSpawnPointHandler = GameObject.Find("ZoogiSpawnPoints").GetComponent<ZoogiSpawnPointHandler>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentState == State.WAITING){
			setCurrentState (State.TAKING_TURN);
		}
		else if (currentState == State.TAKING_TURN){
			
		}
	}
	
	public State getCurrentState(){
		return currentState;
	}
	
	public bool setCurrentState(State newState){
	
		if(currentState == State.TAKING_TURN){
			turnFlowController.enabled = false;
			ShipCollectorCollisionHandler.SkybitsCollected -= skybitsCollected;
		}
		
		currentState = newState;
		
		if(newState == State.TAKING_TURN){
			turnFlowController.enabled = true;
			ShipCollectorCollisionHandler.SkybitsCollected += skybitsCollected;
			if(!teamRoster.getZoogi(currentTeamIndex, currentZoogiIndex).activeSelf){
				spawnZoogiAt(zoogiSpawnPointHandler.findRandomSpawnPoint(),teamRoster.getZoogi(currentTeamIndex, currentZoogiIndex));
			}
			turnFlowController.takeTurn(teamRoster.getZoogi(currentTeamIndex, currentZoogiIndex));
		}
		
		return true;
	}
	
	public void turnEnded(GameObject zoogi){
		goToNextZoogi();
		setCurrentState(State.WAITING);
	}
	
	public void spawnZoogiAt(Transform location, GameObject zoogi){
		zoogi.transform.position = location.position;
		zoogi.transform.FindChild("Ball").localPosition = Vector3.zero;
		zoogi.transform.FindChild("Ball").localRotation = Quaternion.identity;
		if(!zoogi.activeSelf){
			zoogi.SetActive(true);
		}
	}
	
	public void goToNextZoogi(){
		if(currentZoogiIndex < teamList[currentTeamIndex].teamMembers.Count-1){
			currentZoogiIndex += 1;
		}
		else{
			currentZoogiIndex = 0;
			if(currentTeamIndex < teamList.Count-1){
				currentTeamIndex += 1;
			}
			else{
				currentTeamIndex = 0;
			}
			scoreTracker.changePlayerIndex(currentTeamIndex);
			if(PlayerChangeEvent != null){
				PlayerChangeEvent(currentTeamIndex);
			}
		}
	}
	
	public void skybitsCollected(int amount){
		scoreTracker.addPointsToCurrentPlayer(amount);
	}
}