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
		else if(Vector3.Angle(Vector3.up, target.up)<5f)
		{
			Vector3 forward=target.forward;
			forward.y=0f;

			transform.forward=forward;
		}
	}
}
