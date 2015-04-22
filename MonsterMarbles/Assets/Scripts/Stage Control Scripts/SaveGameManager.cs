using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp{

	public class SaveGameManager : MonoBehaviour {

		long lastIdAssigned =1;
		public List<IntSavable> saveList;
		public List<string> turnSavesJSON;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public long requestId(){
			return ++lastIdAssigned;
		}

		/** Method used to add a savable object to the list of objects to be recorded.
		 * 
		 * */
		public void registerObject(IntSavable saveThis){
			saveList.Add(saveThis);
		}

		/** Method used to remove a savable object from the list of objects to be recorded.
		 * 
		 * */
		public void unregisterObject(IntSavable removeThis){
			saveList.Remove(removeThis);
		}

		/**
	 *  This method will be used to record a frame of the game
	 * can be used to create instant replay
	 */
		public void saveFrame(){
			
			foreach (IntSavable saveObject in saveList){
				//Build Json String
			}
		}

	}
}