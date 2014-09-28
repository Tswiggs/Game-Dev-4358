using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class PlayerTurnController : MonoBehaviour {

	public PlayerBall activeBall; 
	public List<PlayerBall> player1Balls; 
	public List<PlayerBall> player2Balls; 


	// Use this for initialization
	void Start () {
	
	}
	
	/// <summary>
	/// checks if a game ball has stopped rolling so that the players can switch turns. 
	/// </summary>
	void Update () {
	
	}
	/// <summary>
	/// specifies the game ball that is currently being controlled by the player. 
	/// </summary>
	/// <param name="newActiveBall">New active ball.</param>
	public void setActiveBall(PlayerBall newActiveBall)
	{
		activeBall.setActive (false); 
		activeBall = newActiveBall; 
	}

	/// <summary>
	/// disables control of the current game ball and gives the player control of a new ball. 
	/// </summary>
	public void switchPlayerTurn()
	{

	}

	/// <summary>
	/// sets the game balls that player 1 and player 2 get to use during the game. 
	/// </summary>
	/// <param name="player1Balls">the balls player 1 gets to use. </param>
	/// <param name="player2Balls">the balls that player 2 gets to use.</param>
	public void initializePlayerballs(List<PlayerBall> player1Balls, List<PlayerBall> player2Balls)
	{
		this.player1Balls = player1Balls;
		this.player2Balls = player2Balls; 
		setActiveBall (this.player1Balls [0]);
	}

}
