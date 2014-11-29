using UnityEngine;
using System.Collections;

public class SkyBitCount : MonoBehaviour {


	public GUIText counter;
	private int count = 0;
	// Use this for initialization
	void Start () {
		OutOfBoundsHandler.pointCollected += increaseSkyBitCount;
	}
	
	// Update is called once per frame
	void Update () {
		string text = "x ";
		text += count.ToString();
		counter.text = text;
	}
	
	void increaseSkyBitCount(){
		count += 1;
	}
}
