using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkybitDropper : MonoBehaviour {
	
	
	public GameObject skybit;
	
	private List<Transform> skybitDropPoints;
	// Use this for initialization
	void Start () {
		skybitDropPoints = new List<Transform>();
		for (int x  = 0 ; x < transform.childCount; x++){
			skybitDropPoints.Add(transform.GetChild(x));
		}
		
		RingerController.dropSkybits += randomDropSkybits;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void randomDropSkybits(int amount){
	
		bool[] alreadyUsed = new bool[skybitDropPoints.Count];
		for(int x = 0; x < skybitDropPoints.Count; x++){
			alreadyUsed[x] = false;
		}
		
		for(int x = 0; x < amount; x++){
			int rand = Random.Range(0,skybitDropPoints.Count);
			if(!alreadyUsed[rand]){
				Instantiate (skybit, skybitDropPoints[rand].position, Quaternion.identity);
				alreadyUsed[rand] = true;
			}
			else{
				int count = 0;
				for(int y = rand; count < amount; count++){
					y++;
					if(y >= skybitDropPoints.Count){
						y = 0;
					}
					if(!alreadyUsed[y]){
						Instantiate (skybit, skybitDropPoints[y].position, Quaternion.identity);
						count = amount;
						alreadyUsed[y] = true;
					}
				}
			}
		}
	}
}
