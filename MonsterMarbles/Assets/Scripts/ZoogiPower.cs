using UnityEngine;
using System.Collections;

public abstract class ZoogiPower : MonoBehaviour {

	public bool powerReady;
	
	abstract public bool activatePower();
}
