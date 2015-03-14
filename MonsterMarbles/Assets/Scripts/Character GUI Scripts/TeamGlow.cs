using UnityEngine;
using System.Collections;

public class TeamGlow : MonoBehaviour {
	
	public static Color teamColor;
	private Light glow;
	// Use this for initialization
	void Awake() {
		teamColor = Color.red;
	}
	
	void Start () {
		RingerController.PlayerTurnStartEvent += setCurrentTeamColor;
		glow = GetComponent<Light>();
		glow.color = teamColor;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void setCurrentTeamColor(int playerIndex){
		if(playerIndex == 0){
			teamColor = Color.red;
		}
		else{
			teamColor = Color.blue;
		}
	}
	
}
