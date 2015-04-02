using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	public bool tutorialsEnabled;
	public int numberOfTutorials=5;
	public int currentTutorial=0;
	public GameObject[] tutorials;
	public GameObject tutorialGUI;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnEnable(){
		LaunchController.LaunchControllerEnabled += showLaunchTips;
		LaunchController.launchCompleted += hideLaunchTips;
	}

	void showLaunchTips(GameObject thing){
		if(tutorialsEnabled){
			currentTutorial=0;
			enableTutorial(currentTutorial);

		}
	}
	void hideLaunchTips(){

		disableTutorial(currentTutorial);
		tutorialGUI.SetActive(false);

	}
	public void nextTutorial(){
		disableTutorial (currentTutorial);
		if(currentTutorial<numberOfTutorials-1){
			currentTutorial++;
		}else{
			currentTutorial=0;
		}
		enableTutorial(currentTutorial);

	}
	void enableTutorial(int tutorialID){
		tutorials[tutorialID].gameObject.SetActive(true);
	}
	void disableTutorial(int tutorialID){
		tutorials[tutorialID].gameObject.SetActive(false);
	}
	void setShowTutorials(bool show){
		tutorialsEnabled=show;
	}
}
