using UnityEngine;
using System.Collections;

public class whirlwind_effect : MonoBehaviour {
	public float windStregnth=10f;
	void OnTriggerStay(Collider other) {
		Vector3 pushDirection= other.transform.position - gameObject.transform.position ;
		other.gameObject.GetComponent<Rigidbody>().AddForce(pushDirection*windStregnth);
	}
}
