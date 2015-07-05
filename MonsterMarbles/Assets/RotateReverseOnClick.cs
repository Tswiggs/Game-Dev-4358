using UnityEngine;
using System.Collections;

public class RotateReverseOnClick : MonoBehaviour {

	bool reverse = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!reverse){
			this.transform.parent.Rotate(Vector3.up,.75f);
		}
		else{
			this.transform.parent.Rotate(Vector3.up,-1.5f);
			if(!Input.GetMouseButton(0)){
				reverse = false;
			}
		}

		if(Input.GetMouseButtonDown(0)){
			Camera.main.ScreenPointToRay(Input.mousePosition);
			foreach (RaycastHit ray in Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition))){
				Debug.Log(ray.point);
				if(ray.collider == this.GetComponent<Collider>()){
					Debug.Log ("Hit!");
					reverse = true;
					//this.transform.position = new Vector3(this.transform.position.x+2,this.transform.position.y,this.transform.position.z);
				}
			}
		}
	}
}
