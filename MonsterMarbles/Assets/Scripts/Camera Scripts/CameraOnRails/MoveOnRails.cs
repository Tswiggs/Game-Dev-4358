using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/**
 * This script will find all RailNodes in the scene guides the object its attached to
 * between the nodes in a definable order.
 * 
 */
public class MoveOnRails : MonoBehaviour {

	private IList<RailNode> nodes= new List<RailNode>();
	private RailNode currentNode;
	private Vector3 upVector = Vector3.up;
	private int currentNodeId;
	private bool isMoving = false;
	private Vector3 lerpBegin;
	private float timeLerpStarted;
	private float totalDistance;
	private float totalTime;
	private Quaternion startRot;

	/// <summary>
	/// Defines the move speed of camera in unitdistance/time.
	/// </summary>
	public float speed = 10;

	public void FixedUpdate()
	{
		if(isMoving)
		{
			float timeSinceStarted = Time.time - timeLerpStarted;
			float lerpProgress =  timeSinceStarted / totalTime;
			transform.LookAt(currentNode.target);
			Quaternion newRot = transform.rotation;
			transform.position = Vector3.Lerp(lerpBegin, currentNode.transform.position, lerpProgress);
			transform.rotation = Quaternion.Lerp(startRot, newRot, lerpProgress);
			if(lerpProgress >= 1)
			{
				lerpProgress = 0;
				isMoving = false;
			}
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="MoveOnRails"/> class.
	/// </summary>
	 public void initialize (List<RailNode> nodes) 
	{
		if(nodes.Count==0)
		{
			throw new UnityException("Camera Unable to Initialize.  Requires at least 1 node in RailCameraController.nodes");
		}
		this.nodes=nodes;
		jumpToNode(0);
		//listenForAdvance
	}

	/**
	 * Moves the object to the next node as defined by the order in "nodes"
	 * Also orients towards nodes.next().getTarget()
	 * */
	void advanceToNext () 
	{
		currentNodeId += 1;
		currentNode=nodes[currentNodeId];

		lerpToCurrentNode ();
	}
	/// <summary>
	/// Advances to nodes[id].
	/// </summary>
	/// <param name="id">id of node to advance to.</param>
	void advanceToNodeId(int id)
	{
		currentNodeId = id;
		currentNode= nodes[id];

		lerpToCurrentNode();
	}

	private void lerpToCurrentNode()
	{
		isMoving = true;
		lerpBegin = transform.position;
		timeLerpStarted= Time.time;
		totalDistance = (lerpBegin - currentNode.transform.position).magnitude;
		totalTime = totalDistance/speed;
		startRot = transform.rotation;
	}

	private void jumpToNode(int id)
	{
		currentNodeId=id;
		currentNode=nodes[id];
		transform.position=currentNode.transform.position;
		transform.LookAt(currentNode.target);
	}
}
