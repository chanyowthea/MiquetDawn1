using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Singleton
{
	public static EventManager _eventManager; 
	public static void Init()
	{
		_eventManager = new EventManager(); 
	}
}
