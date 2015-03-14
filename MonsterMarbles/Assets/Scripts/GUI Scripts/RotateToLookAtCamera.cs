using UnityEngine;
using System.Collections;

public class RotateToLookAtCamera : MonoBehaviour {

	void Update()
	{
		if(Camera.main != null)
		{
			transform.LookAt(Camera.main.transform.position);
		}
	}
}
