using UnityEngine;
using System.Collections;

public class DisableEnableWithKeyPress : MonoBehaviour {
	
	public KeyCode key = KeyCode.Q;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0)){
			Vector3 position = Input.mousePosition;
			if(position.y > Screen.height - (Screen.height*0.1f)){
				if(enabled){
					this.gameObject.SetActive(false);
				}
			}
		}
	}
}
