using UnityEngine;
using System.Collections;

// @author Meoaim

public class CameraRotateController : MonoBehaviour {

	public GameObject player;

	public float rotateZone = 0.2f;
	public float minRotationSpeed = 50f;
	public float maxRotationSpeed = 1000f;
	public float rotateVerticalOffset = 2f;

	private float rotation;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.mousePosition.x < Screen.width * rotateZone) {
			rotation=-1 * Time.deltaTime* (minRotationSpeed +((maxRotationSpeed-minRotationSpeed)* (((Screen.width * rotateZone)- Input.mousePosition.x)/Screen.width*rotateZone)));
			//offset=Quaternion.Euler(0f, rotation, 0f)*offset;
			//transform.position=player.transform.position + offset;
	        transform.RotateAround(player.transform.position, Vector3.up, rotation);
			transform.LookAt(new Vector3(player.transform.position.x,player.transform.position.y+rotateVerticalOffset,player.transform.position.z));
		} 
		else if (Input.mousePosition.x > Screen.width - Screen.width * rotateZone) {
			rotation=Time.deltaTime* (minRotationSpeed +((maxRotationSpeed-minRotationSpeed)* ((Input.mousePosition.x - (Screen.width - Screen.width * rotateZone))/Screen.width*rotateZone)));
			//offset=Quaternion.Euler(0f, rotation, 0f)*offset;
			//transform.position=player.transform.position + offset;
			//transform.Rotate(0f,rotation * -1, 0f);
			transform.RotateAround(player.transform.position, Vector3.up, rotation);
			transform.LookAt(new Vector3(player.transform.position.x,player.transform.position.y+rotateVerticalOffset,player.transform.position.z));
		}
	}
}
