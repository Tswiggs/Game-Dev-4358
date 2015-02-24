using UnityEngine;
using System.Collections;

public class whirlwind_effect : MonoBehaviour {
	public float upStrength=10f;
	public float forwardStrength=10f;
	void OnTriggerStay(Collider other) {
		Vector3 direction = other.rigidbody.velocity;
		direction.Set(direction.x, 0, direction.z);
		direction=direction*forwardStrength;
		direction+=Vector3.up*upStrength;
		other.gameObject.rigidbody.AddForce(direction);
	}
}
