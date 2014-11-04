using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using AssemblyCSharp;
public class RingerController : MonoBehaviour {

	/// <summary>specifies whether the game will be conducted online</summary>
	public enum MULTIPLAYER_MODE
	{
		HOTSEAT, ONLINE
	}

	public static int POINTS_FOR_SKY_BIT = 3; 
	public int remainingSkybits;


	public MULTIPLAYER_MODE gameMode; 
	/// <summary>
	/// The players.
	/// </summary>
	public IList players; 
	/// <summary>
	/// The active player.
	/// </summary>
	public Player activePlayer;
	/// <summary>
	/// The game camera.
	/// </summary>
	public CameraBoomController cameraBoom; 
	/// <summary>
	/// The "net" that catches game objects when they fall off of the arena. 
	/// </summary>
	public GameObject objectCatcher; 
	public GameController gameController;
	public PlayerBallCreator ballSpawner;
	public bool isShooting=false;
	private int activePlayerIndex=0;

	void Start () {
		//players = new ArrayList (); 
		gameMode = MULTIPLAYER_MODE.HOTSEAT; 
	}

	public void initialize(GameController gameController, string multiplayerMode, ArrayList players){
		this.gameController=gameController;
		this.cameraBoom=GameObject.Find("CameraBoom").GetComponent<CameraBoomController>();
		//TODO: change so that both RingerController and GameController are using the enum MULTIPLAYER_MODE
		gameMode=MULTIPLAYER_MODE.HOTSEAT;

		this.players=players;
		if(players.Count==0){print("Player List not initialized"); return;}
		activePlayer=players[0] as Player;
		foreach( Player player in players){
			foreach( PlayerBallCreator.MONSTER_PREFABS character in player.getTeamSelection()){
				PlayerBall ball=new PlayerBall(player, ballSpawner.createPlayerBall(character));
				player.addPlayerBall(ball);
			}
		}
		possess(activePlayer.getActiveBall());
		focusCameraForTurnStart(activePlayer.getActiveBall().getBallObject().transform.FindChild("Character Root").gameObject);
	}

	void Update () {
	
	}

	public void addPlayer(Player player)
	{
		players.Add (player); 
	}
	public void focusCameraForTurnStart(GameObject objectToFocusOn)
	{
		cameraBoom.startOfTurn(objectToFocusOn.transform);
	}

	public void possess(PlayerBall nextBall)
	{
		if(!nextBall.isOnBoard())
		{

			//nextBall.getBallObject().transform.position=ballSpawner.transform.position;
			//nextBall.getBallObject().transform.rotation=ballSpawner.transform.rotation;
			nextBall.getBallObject().SetActive(true);


		}
		LaunchController.launchCompleted+=shootingAction;
		nextBall.possess();
		focusCameraForTurnStart(activePlayer.getActiveBall().getBallObject().transform.FindChild("Character Root").gameObject);
	}

	void shootingAction(){
		isShooting=true;
		LaunchController.launchCompleted-=shootingAction;
		SteeringController.rollCompleted+=endOfTurnAction;
	}

	void endOfTurnAction(){
		SteeringController.rollCompleted-=endOfTurnAction;
		StartCoroutine(delaySeconds(5));
		activePlayer.nextBall();
		waitForTurn();
	}

	IEnumerator delaySeconds(int seconds){
		yield return new WaitForSeconds(seconds);
	}


	//TODO: Need to pull out nextPlayer logic into its own method.
	public void waitForTurn()
	{
	if (gameMode == MULTIPLAYER_MODE.ONLINE) {

		} else if(gameMode == MULTIPLAYER_MODE.HOTSEAT) {
			if((activePlayerIndex+1) < players.Count)
			{
				activePlayerIndex++;
				//this is a change
			
				print(activePlayerIndex.ToString());
				activePlayer=players[activePlayerIndex] as Player;
			}else{
				activePlayer=players[0] as Player;
			}
		}

		startOfTurn();
	}

	void startOfTurn(){
		//Show which players turn it is.
		//Have them tap a button to begin.
		possess(activePlayer.getActiveBall());

	}

	public void incrementScoreForCurrentPlayer()
	{
		activePlayer.setScore (activePlayer.getScore () + POINTS_FOR_SKY_BIT);
	}
}
