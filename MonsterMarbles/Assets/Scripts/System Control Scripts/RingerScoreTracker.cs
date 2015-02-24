
using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using AssemblyCSharp;
using System;

public class RingerScoreTracker
{
	
	private int[,] roundMatrix;
	private int[] roundWins;
	private int numberOfPlayers;
	
	private int playerIndex = 0;
	private int roundIndex = 0;
	
	private int skybitsForRound = 0;
	private int totalSkybits = 0;
	
	public delegate void broadcastScore(int score, int player);
	public static event broadcastScore playerScoreChange;
	
	public delegate void hasWinner(int index);
	public static event hasWinner playerHasWonRound;
	public static event  hasWinner playerHasWonGame;
	

		public RingerScoreTracker (int numPlayers, int numRounds, int currentPlayer = 0)
		{
			roundMatrix = new int[numRounds,numPlayers];
			roundWins = new int[numPlayers];
			numberOfPlayers = numPlayers;
			
			playerIndex = currentPlayer;
			roundIndex = 0;
			
			OutOfBoundsHandler.pointCollected += addPointToCurrentPlayer;
			RingerController.dropSkybits += addSkybitsToRound;
			RingerController.PlayerTurnStartEvent += changePlayerIndex;
		}
		
		public void changePlayerIndex(int index){
			if(index > -1 && index < roundWins.Length){
				playerIndex = index;
			}
		}
		
		public void addPointToCurrentPlayer(){
			roundMatrix[roundIndex,playerIndex] += 1;
			if(playerScoreChange != null){
				playerScoreChange(roundMatrix[roundIndex,playerIndex], playerIndex);
			}
			checkForRoundWin();
		}
		
		public int getCurrentRound(){
			return roundIndex;
		}
		
		public void checkForRoundWin(){
			for(int x = 0; x < numberOfPlayers; x++){
				if(roundMatrix[roundIndex,x] >= (Math.Floor((double)skybitsForRound/2)+1)){
					roundWins[x] += 1;
					if(playerHasWonRound != null){
						playerHasWonRound(x);
					}
					x = roundMatrix.Length;
					checkForGameWin();
					ZoogiController.ZoogiTurnCompleteEvent += advanceRound;
				}
			}
		}
		
		public void checkForGameWin(){
			for(int x = 0; x < numberOfPlayers; x++){
			if(roundWins[x] > (Math.Floor((double)roundMatrix.Length/2)+1)){
					if(playerHasWonGame != null){
						playerHasWonGame(x);
					}
					x = roundWins.Length;
				}
			}
		}
		
		public void advanceRound(int index){
			int count = 0;
			for( int x  =0; x < numberOfPlayers; x++){
				count += roundMatrix[roundIndex,x];
			}
			skybitsForRound -= count;
			
			for(int x = 0; x < roundWins.Length; x++){
				if(playerScoreChange != null){
					playerScoreChange(roundMatrix[roundIndex,x], x);
				}
			}
			roundIndex += 1;
			ZoogiController.ZoogiTurnCompleteEvent -= advanceRound;
	
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

