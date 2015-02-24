using UnityEngine;
using System.Collections;

public class FollowBallCameraController : MonoBehaviour {

	public GameObject player;
	public float rotationSpeed=1f;
	private Vector3 offset;
	private float rotation;
	// Use this for initialization
	void Start () {
		offset=transform.position-player.transform.position;
		rotation=transform.rotation.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		rotation=Input.GetAxis("Mouse X")*Time.deltaTime*rotationSpeed+rotation;
		offset=Quaternion.Euler(0f, rotation, 0f)*offset;
		transform.position=player.transform.position + offset;
		transform.Rotate(0f,rotation, 0f);
	}
}
