using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkybitDropper : MonoBehaviour {
	
	
	private List<Transform> skybitDropPoints;
	// Use this for initialization
	void Start () {
		for (int x  = 0 ; x < transform.childCount; x++){
			skybitDropPoints.Add(transform.GetChild(x));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
