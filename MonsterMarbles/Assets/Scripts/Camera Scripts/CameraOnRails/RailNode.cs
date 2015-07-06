using UnityEngine;
using System.Collections;


public class RailNode : MonoBehaviour {
	public Transform target;

	void Start()
	{
		target = transform.GetChild(0);
	}
}
