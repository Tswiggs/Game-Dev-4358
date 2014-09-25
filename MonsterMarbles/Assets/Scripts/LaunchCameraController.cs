using UnityEngine;
using System.Collections;

public class LaunchCameraController : MonoBehaviour {

	public float horizontalOffset = 1f;
	public float verticalOffset = 1f;

	public Transform target;

	// Use this for initialization
	void Start () {
		//vector3 offset=(target.Position-horizontalOffset
		transform.postion=target.position + 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
