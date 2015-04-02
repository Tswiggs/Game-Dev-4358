using UnityEngine;
using System.Collections;

public class ZoogiAssembler : MonoBehaviour {
	
	public static GameObject playerZoogi;
	public static GameObject model;
	public static GameObject script;
	
	public GameObject instantiatedPlayerZoogi;
	public GameObject instantiatedModel;
	public GameObject instantiatedScript;
	
	// Use this for initialization
	void Awake() {
		playerZoogi = instantiatedPlayerZoogi;
		model = instantiatedModel;
		script = instantiatedScript;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static GameObject instantiateZoogi(Vector3 zoogiPosition, Quaternion zoogiRotation, GameObject zoogiModel = null, GameObject zoogiScript = null){
		if(zoogiModel != null){
			model = zoogiModel;
		}
		if(zoogiScript != null){
			script = zoogiScript;
		}
		return createZoogi(zoogiPosition, zoogiRotation);
		
	}
	
	private static GameObject createZoogi(Vector3 zoogiPosition, Quaternion zoogiRotation){
		GameObject instPlayerZoogi = Instantiate(playerZoogi, zoogiPosition, zoogiRotation) as GameObject;
		GameObject instModel = Instantiate(model) as GameObject;
		Vector3 previousLocalPosition = instModel.transform.localPosition;
		instModel.transform.SetParent(instPlayerZoogi.transform.FindChild("Ball"));
		instModel.transform.localPosition = previousLocalPosition;
		
		Component[] listOfComponents = script.GetComponents<MonoBehaviour>();
		
		instPlayerZoogi.name = "Zoogi Hotstreak";
		
		foreach (Component component in listOfComponents){
			instPlayerZoogi.AddComponent(component.GetType());
		}
		
		instPlayerZoogi.GetComponent<ZoogiController>().initialize();
		
		instPlayerZoogi.SetActive(false);
		return instPlayerZoogi;
	}
}
