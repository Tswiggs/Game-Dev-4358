using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

	public Vector3 axisSpeed;
	
	void Start(){

	}
	
	void Update(){
		transform.Rotate( axisSpeed * Time.deltaTime);
	}
}
