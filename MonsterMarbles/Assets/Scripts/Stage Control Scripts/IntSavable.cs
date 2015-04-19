//------------------------------------------------------------------------------
// All objects wishing that you would like to track via the save/replay system
// must implement this interface.
//
// Its purpose it to act as a gameobject/JSON converter.
//------------------------------------------------------------------------------
using System;
using UnityEngine;
namespace AssemblyCSharp
{
	public abstract class IntSavable
	{

		public void enableSaving(){
			SaveGameManager manager=GameObject.Find("SaveGameManager").GetComponent<SaveGameManager>();
			if(getId()==0)
			{
				setId(manager.requestId());
			}
			manager.registerObject(this);
		}

		public void disableSaving(){
			SaveGameManager manager=GameObject.Find("SaveGameManager").GetComponent<SaveGameManager>();
			manager.unregisterObject(this);
		}

		public abstract long getId();
		public abstract string stringifyPosition();
		public abstract string stringifyOtherData();

		public abstract void setId(long id);
		public abstract void decodePosition();
		public abstract void decodeData();
	}
}

