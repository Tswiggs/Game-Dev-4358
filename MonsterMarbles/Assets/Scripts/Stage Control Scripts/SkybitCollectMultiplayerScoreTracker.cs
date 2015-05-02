using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using AssemblyCSharp;
using System;

public class SkybitCollectMultiplayerScoreTracker : ObjectiveTrackerInterface {
	
	private int[,] roundMatrix;
	private int[] roundWins;
	private int numberOfPlayers;
	private int numberOfRounds;
	
	private int playerIndex = 0;
	private int roundIndex = 0;
	
	private int skybitsForRound = 0;
	private int totalSkybits = 0;
	
	private bool roundWon = false;
	
	
	public SkybitCollectMultiplayerScoreTracker (int numPlayers, int numRounds, int currentPlayer = 0)
	{
		roundMatrix = new int[numRounds,numPlayers];
		roundWins = new int[numPlayers];
		numberOfPlayers = numPlayers;
		numberOfRounds = numRounds;
		
		roundWon = false;
		
		playerIndex = currentPlayer;
		roundIndex = 0;
	}
	
	public bool isGameOver() {
		return false;
	}
	
	public int getPlayerWon(){
		return -1;
	}
	
	
	public int getPlayerScore(int index){
		return 0;
	}
	
	public string getPlayerScoreString(int index){
		return "0/0";
	}
	
	public int getPlayerRanking(int index){
		return 0;
	}
	
	public void changePlayerIndex(int index){
		if(index > -1 && index < roundWins.Length){
			playerIndex = index;
		}
	}
	
	public void addPointToCurrentPlayer(){
		addPointsToCurrentPlayer(1);
	}
	
	public void addPointsToCurrentPlayer(int amount){
		roundMatrix[roundIndex,playerIndex] += amount;
		checkForRoundWin();
	}
	
	public int getCurrentRound(){
		return roundIndex;
	}
	
	public void checkForRoundWin(){
		if(!roundWon){
			for(int x = 0; x < numberOfPlayers; x++){
				if(roundMatrix[roundIndex,x] >= (Math.Floor((double)skybitsForRound/2)+1)){
					roundWins[x] += 1;
					roundWon = true;
					
					x = numberOfPlayers;
					checkForGameWin();
					RingerController.PlayerTurnCompleteEvent += advanceRound;
				}
			}
		}	
	}
	
	public void checkForGameWin(){
		for(int x = 0; x < numberOfPlayers; x++){
			if(roundWins[x] >= (Math.Floor((double)numberOfRounds/2)+1)){
				x = numberOfPlayers;
			}
		}
	}
	
	public void advanceRound(int index){
		int count = 0;
		for( int x  =0; x < numberOfPlayers; x++){
			count += roundMatrix[roundIndex,x];
		}
		skybitsForRound -= count;
		if(roundIndex < numberOfRounds-1){
			roundIndex += 1;
			for(int x = 0; x < roundWins.Length; x++){

			}
		}
		roundWon = false;
		RingerController.PlayerTurnCompleteEvent -= advanceRound;
		
	}
	
	public int skybitsInPlay(){
		int count = 0;
		for( int x  =0; x < numberOfPlayers; x++){
			count += roundMatrix[roundIndex,x];
		}
		return skybitsForRound - count;
	}
	
	public void addSkybitsToRound(int amount){
		skybitsForRound += amount;
		totalSkybits += amount;
	}
}
