using UnityEngine;
using System.Collections;

public class Viewport3DGUIController : MonoBehaviour {
	
	
	public Camera GUI3DCamera;
	// Use this for initialization
	void Start () {
		GUI3DCamera.pixelRect = new Rect(10f,Screen.height-Screen.width*0.1f-10, Screen.width*0.1f,Screen.width*0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
