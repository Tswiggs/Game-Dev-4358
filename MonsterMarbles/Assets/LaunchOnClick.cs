using UnityEngine;
using System.Collections;

public class LaunchOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Camera.main.ScreenPointToRay(Input.mousePosition);
			foreach (RaycastHit ray in Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition))){
				Debug.Log(ray.point);
				if(ray.collider == this.GetComponent<Collider>()){
					Debug.Log ("Hit!");
					this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(8,0,0),ForceMode.VelocityChange);
					//this.transform.position = new Vector3(this.transform.position.x+2,this.transform.position.y,this.transform.position.z);
				}
			}
		}
	}
}
