using UnityEngine;
using System.Collections;

public class SkyBitCount : MonoBehaviour {

	
	
	public GUIText counter;
	private int activePlayerIndex = 0;
	private int[] playerBitCounts;
	// Use this for initialization
	
	public delegate void PlayerHasWon(int playerIndex);
	public static event PlayerHasWon victoryEvent;
	
	
	void Start () {
		RingerController.PlayerTurnStartEvent += changePlayer;
		RingerScoreTracker.playerScoreChange += setPlayerScore;
		
		playerBitCounts = new int[4];
	}
	
	void changePlayer(int playerIndex){
		if(playerIndex < playerBitCounts.Length){
			activePlayerIndex = playerIndex;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void setPlayerScore(int score, int playerIndex){
		playerBitCounts[playerIndex] = score;
		string text = "x ";
		text += playerBitCounts[activePlayerIndex].ToString();
		counter.text = text;
	}
}
