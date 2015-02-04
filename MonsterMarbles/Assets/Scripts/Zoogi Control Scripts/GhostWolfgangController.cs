﻿using UnityEngine;
using System.Collections;

public class GhostWolfgangController : MonoBehaviour {
	
	public float alphaValue = 0.5f;
	
	// Use this for initialization
	void Start () {
		Color oldColor = this.renderer.material.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);          
		this.renderer.material.color = newColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
