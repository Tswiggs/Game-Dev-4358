using UnityEngine;
using System.Collections;

public class FollowPositionAndRotation : MonoBehaviour {
	public Transform target;
	public float sleepVelocityMagnitude=1;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position ;
		if (target.rigidbody.velocity.magnitude > sleepVelocityMagnitude)
		{
			transform.rotation=Quaternion.LookRotation(target.rigidbody.velocity);
		}
	}
}
