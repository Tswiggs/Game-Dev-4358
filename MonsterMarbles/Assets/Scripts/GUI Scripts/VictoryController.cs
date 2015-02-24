using UnityEngine;
using System.Collections;

public class VictoryController : MonoBehaviour {

	public GUIText text;
	public float timeToDisplayVictoryScreen;
	
	private bool playerHasWon = false;
	
	private float timer = 0;
	// Use this for initialization
	void Start () {
		SkyBitCount.victoryEvent += displayVictoryScreen;
	}
	
	void displayVictoryScreen(int playerIndex){
		playerHasWon = true;
		text.enabled = true;
		string victoryText = "Player ";
		victoryText += (playerIndex+1).ToString ();
		victoryText+= " Has Won!";
		text.text = victoryText;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerHasWon){
			if(timeToDisplayVictoryScreen > timer){
				timer += Time.deltaTime;
				if(Input.anyKeyDown){
					timer = timeToDisplayVictoryScreen;
				}
			}
			else{
				Application.LoadLevel(Constants.SCENE_MAIN_MENU);
			}
		}
	}
}
