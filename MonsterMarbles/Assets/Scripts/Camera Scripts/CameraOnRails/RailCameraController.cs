using UnityEngine;
using System.Collections.Generic;

public class RailCameraController : MonoBehaviour {

	public List<RailNode> nodes = new List<RailNode>();
	public GameObject railObject;

	// Use this for initialization
	void Start () {
		railObject.GetComponent<MoveOnRails>().initialize(nodes);
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position, "RailCameraController", true);
	}
	

}
