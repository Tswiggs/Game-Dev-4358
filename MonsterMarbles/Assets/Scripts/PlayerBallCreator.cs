using UnityEngine;
using System.Collections;

/// <summary>
/// Used to generate new player balls to be used in the game world. 
/// </summary>
public class PlayerBallCreator : MonoBehaviour {

	///<summary> the different zugies that can be created.</summary>
	public enum MONSTER_PREFABS 
	{
		WOLF, FIREBALL
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Creates A player ball with a specified monster prefab. 
	/// </summary>
	/// <returns>The newly created player ball.</returns>
	/// <param name="prefabToUse">Prefab to use.</param>
	public static PlayerBall createPlayerBall(MONSTER_PREFABS prefabToUse)
	{
		GameObject defaultPlayerBall = GameObject.Find ("Player Ball"); 
		PlayerBall newBall = Instantiate (defaultPlayerBall, new Vector3(2.0F, 70.0f, 3.0f), Quaternion.identity) as PlayerBall; 
		switch (prefabToUse) 
		{
		case MONSTER_PREFABS.FIREBALL:

		default: 
			break;
		}
		return newBall; 
	}
}
