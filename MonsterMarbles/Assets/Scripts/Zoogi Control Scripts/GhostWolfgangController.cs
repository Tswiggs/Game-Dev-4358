using UnityEngine;
using System.Collections;

public class GhostWolfgangController : MonoBehaviour {
	
	public float alphaValue = 0.5f;
	
	// Use this for initialization
	void Start () {
		//Color oldColor = this.GetComponent<Renderer>().material.color;
		//Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);          
		//this.GetComponent<Renderer>().material.color = newColor;
		
		TurnFlowController.TurnEndEvent += turnEnd;
	}
	
	public void turnEnd(GameObject zoogi){
		TurnFlowController.TurnEndEvent -= turnEnd;
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < transform.parent.FindChild("Ball").position.y-1.5f){
			transform.position = new Vector3(transform.position.x,transform.parent.FindChild("Ball").position.y-0.5f,transform.position.z);
		}
	}
}
