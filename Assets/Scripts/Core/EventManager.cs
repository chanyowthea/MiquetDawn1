using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class EventManager
{
	public Action<bool> _onEnterWater;

	//Dictionary<string, object> actionsDict = new Dictionary<string, object>(); 
	//public void Register<T>(string eventName, Action<T> action)
	//{
	//	actionsDict.Add(eventName, action); 
	//}

	//public void Register(string eventName, Action action)
	//{
	//	actionsDict.Add(eventName, action);
	//}

	//public void Invoke(string eventName, params object[] args)
	//{
	//	var a = (Action)actionsDict[eventName];
	//	a(); 
	//}
}
