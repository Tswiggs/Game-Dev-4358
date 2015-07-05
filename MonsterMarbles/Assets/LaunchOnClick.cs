﻿using UnityEngine;
using System.Collections;

public class LaunchOnClick : MonoBehaviour {

	public bool active = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(active){

		}


		if(Input.GetMouseButtonDown(0)){
			Camera.main.ScreenPointToRay(Input.mousePosition);
			foreach (RaycastHit ray in Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition))){
				Debug.Log(ray.point);
				if(ray.collider == this.GetComponent<Collider>()){
					Debug.Log ("Hit!");
					active = true;
				}
			}
		}
	}
}
