using UnityEngine;
using System.Collections;

public class SkybitSpawnPointHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//Returns a random transform from all the children of the gameobject this script is attached to
	public Transform findRandomSpawnPoint(){
		if(transform.childCount > 0){
			int spawnIndex = Random.Range(0,transform.childCount-1);
			return transform.GetChild(spawnIndex);
		}
		else{
			return null;
		}
	}
}
