using UnityEngine;
using System.Collections;
using AssemblyCSharp;

/// <summary>
/// Used to generate new player balls to be used in the game world. 
/// </summary>
public class PlayerBallCreator : MonoBehaviour {
	public  Transform spawnLocation;
	public  Characters characterPrefabs;
	///<summary> the different zoogis that can be created.</summary>
	public enum MONSTER_PREFABS 
	{
		WOLFGANG, HOTSTREAK, LARS, PINPOINT
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
	public GameObject createPlayerBall(MONSTER_PREFABS prefabToUse)
	{
//		GameObject defaultPlayerBall = GameObject.Find ("Player Ball"); 
//		PlayerBall newBall = Instantiate (defaultPlayerBall, spawnLocation.position , spawnLocation.rotation) as PlayerBall; 

		GameObject newBall;
		switch (prefabToUse) 
		{
		case MONSTER_PREFABS.HOTSTREAK: 

			newBall = Instantiate (characterPrefabs.Hotstreak, spawnLocation.position , spawnLocation.rotation) as GameObject;
			break;

		case MONSTER_PREFABS.WOLFGANG:
			newBall = Instantiate (characterPrefabs.Wolfgang, spawnLocation.position , spawnLocation.rotation) as GameObject;
			break;
			
		case MONSTER_PREFABS.LARS:
			newBall = Instantiate (characterPrefabs.Lars, spawnLocation.position , spawnLocation.rotation) as GameObject;
			break;
			
		case MONSTER_PREFABS.PINPOINT:
			newBall = Instantiate (characterPrefabs.Pinpoint, spawnLocation.position , spawnLocation.rotation) as GameObject;
			break;
			

		default: 
			return null;
		}

		newBall.SetActive(false); 
		return newBall;

	}
}
