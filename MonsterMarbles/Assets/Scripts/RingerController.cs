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
	public Camera gameCamera; 
	/// <summary>
	/// The "net" that catches game objects when they fall off of the arena. 
	/// </summary>
	public GameObject objectCatcher; 
	public GameController gameController;

	void Start () {
		players = new ArrayList (); 
		gameMode = MULTIPLAYER_MODE.HOTSEAT;
		Player player1 = new Player (null, 0); 
		Player player2 = new Player (null, 0); 
		players.Add (player1); 
		players.Add (player2); 
	}

	public void initialize(GameController gameController, string multiplayerMode, ArrayList players){
		this.gameController=gameController;

		//TODO: change so that both RingerController and GameController are using the enum MULTIPLAYER_MODE
		gameMode=MULTIPLAYER_MODE.HOTSEAT;

		this.players=players;
	}

	void Update () {
	
	}

	public void addPlayer(Player player)
	{
		players.Add (player); 
	}
	public void focusCamera(GameObject objectToFocusOn)
	{

	}

	public void posess(PlayerBall nextBall)
	{

	}

	public void waitForTurn()
	{
	if (gameMode == MULTIPLAYER_MODE.ONLINE) {

		} else {

		}
	}

	public void incrementScoreForCurrentPlayer()
	{
		activePlayer.setScore (activePlayer.getScore () + POINTS_FOR_SKY_BIT);
	}
}
