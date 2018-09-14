using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[SerializeField] GameObject _underWaterMask;
	void Start()
	{
		Singleton._eventManager._onEnterWater += OnEnterWater; 
	}

	private void OnDestroy()
	{
		Singleton._eventManager._onEnterWater -= OnEnterWater;
	}

	void OnEnterWater(bool value)
	{
		_underWaterMask.SetActive(value); 
	}
}
