using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class RingerController : MonoBehaviour {

	/// <summary>specifies whether the game will be conducted online</summary>
	public enum MULTIPLAYER_MODE
	{
		HOTSEAT, ONLINE
	}

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

	void Start () {
		players = new ArrayList (); 

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
	
	}

	public void incrementScore()
	{

	}
}
