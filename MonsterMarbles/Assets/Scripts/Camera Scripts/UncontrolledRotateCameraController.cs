using UnityEngine;
using System.Collections;

public class UncontrolledRotateCameraController : MonoBehaviour {

	public GameObject target;

	public float rotationSpeed = 75f;
	public float rotateVerticalOffset = 0f;
	public Vector3 rotationAxis = new Vector3 (0, 1, 0);
	public int direction = 1;
	
	private float rotation;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		rotation= direction * Time.deltaTime * rotationSpeed;
		//offset=Quaternion.Euler(0f, rotation, 0f)*offset;
		//transform.position=player.transform.position + offset;
		transform.RotateAround(target.transform.position, rotationAxis, rotation);
		transform.LookAt(new Vector3(target.transform.position.x,target.transform.position.y+rotateVerticalOffset,target.transform.position.z));
	}
}
