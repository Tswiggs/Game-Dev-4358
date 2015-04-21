using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameSoloGUIController : MonoBehaviour {
	
	private Text headerText;
	private Text bodyText;
	
	public AudioClip victoryFanfare;
	public AudioClip defeatFanfare;
	 
	// Use this for initialization
	void Start () {
		headerText = transform.FindChild("Header").FindChild("Information").GetChild (0).GetComponent<Text>();
		bodyText = transform.FindChild("Information Panel").FindChild("Information").GetChild (0).GetComponent<Text>();
		
		GameGUIController.changeToEndGameSoloState += updateTextFromSettings;
	}
	
	public void updateTextFromSettings(){
		GameAudioController.pauseBackgroundMusic();
		if(GameFlowController.Settings.objectiveTracker.getPlayerWon() == 0){
			headerText.text = "Victory!";
			GameAudioController.playOneShotSound(victoryFanfare, 0.3f);
		}
		else{
			headerText.text = "Too Bad!";
			GameAudioController.playOneShotSound(defeatFanfare, 0.3f);
		}
		
		bodyText.text = "";
		
		
		bodyText.text += "1 Star: Reach the Ship Without Falling Off\n\n";
		
		bodyText.text += "2 Stars: Collect All Skybits and Reach the Ship\n\n";

		
		if(GameFlowController.Settings.threeStarTime == 0){
			bodyText.text += "3 Stars: Collect All Skybits and Reach the Ship";
		}
		else{
			bodyText.text += "3 Stars: Collect All Skybits and Reach the Ship in "+GameFlowController.Settings.threeStarTime.ToString()+" Turns Or Less";
		}
	}
	
	void OnDestroy(){
		GameGUIController.changeToEndGameSoloState -= updateTextFromSettings;
	}
}
