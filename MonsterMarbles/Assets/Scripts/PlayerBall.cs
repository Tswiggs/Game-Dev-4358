using UnityEngine;
using System.Collections;

public class PlayerBall : MonoBehaviour {

	///<summary>
	/// the different players that can possibly own a player ball. 
	/// </summary>
	public enum PLAYERS
	{
		player1, player2
	}

	/// <summary>
	/// The player that owns this ball.
	/// </summary>
	public PLAYERS playerOwner; 

	/// <summary>
	/// <c>true</c> if the ball is on the game board, <c> false if it is not.</c>
	/// </summary>
	public bool onGameBoard; 



	// Use this for initialization
	void Start () {
		onGameBoard = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// toggles player control of the game ball.  
	/// </summary>
	/// <param name="active"> if set to <c>true</c> gives the player control of the game ball 
	/// </param>
	public void setActive (bool active)
	{
		this.gameObject.SetActive (active);
	}
}
