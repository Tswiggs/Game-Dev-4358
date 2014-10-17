using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
public class PlayerTurnController : MonoBehaviour {

	public PlayerBall activeBall; 
	public List<PlayerBall> player1Balls; 
	public List<PlayerBall> player2Balls; 
	private int numberOfBallsPerPlayer; 
	private int currentPlayer1Ball; 
	private int currentPlayer2Ball; 

	// Use this for initialization
	void Start () {
		SteeringController.rollCompleted += switchPlayerTurn;
		setActiveBall (player1Balls [0]);
		currentPlayer1Ball = 0; 
		currentPlayer2Ball = 0; 
		if (player1Balls.Count == player2Balls.Count) 
		{
						numberOfBallsPerPlayer = player1Balls.Count; 
		} 
		else 
		{
			throw new PlayerPrefsException("The number of player balls needs to match before the game starts.");
		}

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
		if (activeBall != null)
		{
						activeBall.setActive (false); 
		}
		activeBall = newActiveBall; 
		activeBall.setActive (true);
	}

	/// <summary>
	/// disables control of the current game ball and gives the player control of a new ball. 
	/// </summary>
	public void switchPlayerTurn()
	{
		if (activeBall != null) 
		{
			if (activeBall.playerOwner.CompareTo(PlayerBall.PLAYERS.player1) == 0)
			{
				if (currentPlayer1Ball == 0)
				{
					currentPlayer2Ball = 0; 
				}
				else
				{
					currentPlayer2Ball++; 
				}
				setActiveBall(player2Balls[currentPlayer2Ball]);
			}
			else
			{
				if (activeBall.playerOwner.CompareTo(PlayerBall.PLAYERS.player2) == 0)
				{
					if (currentPlayer1Ball == numberOfBallsPerPlayer)
					{
						currentPlayer1Ball = 0; 
					}
					else
					{
						currentPlayer1Ball++; 
					}
					setActiveBall(player1Balls[currentPlayer1Ball]);
				}
			}
		}
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
