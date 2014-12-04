using UnityEngine;
using System.Collections;

public class SkyBitCount : MonoBehaviour {

	
	public int totalSkyBits;
	private int skyBitsCollected;
	
	public GUIText counter;
	private int activePlayerIndex = 0;
	private int[] playerBitCounts;
	// Use this for initialization
	
	public delegate void PlayerHasWon(int playerIndex);
	public static event PlayerHasWon victoryEvent;
	
	
	void Start () {
		OutOfBoundsHandler.pointCollected += increaseSkyBitCount;
		RingerController.PlayerChangeEvent += changePlayer;
		
		playerBitCounts = new int[4];
	}
	
	void changePlayer(int playerIndex){
		if(playerIndex < playerBitCounts.Length){
			activePlayerIndex = playerIndex;
		}
	}
	
	// Update is called once per frame
	void Update () {
		string text = "x ";
		text += playerBitCounts[activePlayerIndex].ToString();
		counter.text = text;
	}
	
	int victoryCheck(){
		
		if(skyBitsCollected >= totalSkyBits){
			int highestTotal = 0;
			int playerWithHighestTotal = 0;
			for(int x = 0; x < playerBitCounts.Length; x++){
				if(playerBitCounts[x] > highestTotal){
					highestTotal = playerBitCounts[x];
					playerWithHighestTotal = x;
				}
			}
			if(victoryEvent != null){
				victoryEvent(playerWithHighestTotal);
				return playerWithHighestTotal;
			}
		}
		return -1;
	}
	
	void increaseSkyBitCount(){
		playerBitCounts[activePlayerIndex] += 1;
		skyBitsCollected += 1;
		victoryCheck();
	}
}
