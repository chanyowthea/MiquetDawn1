using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] GameObject _underWater;
	[SerializeField] GameObject _aboveWater; 

	void Awake()
	{
		Singleton.Init(); 
	}

	void Start()
	{
		Singleton._eventManager._onEnterWater += OnEnterWater;
	}

	void OnDestroy()
	{
		Singleton._eventManager._onEnterWater -= OnEnterWater;
	}

	void OnEnterWater(bool value)
	{
		_underWater.SetActive(value);
		_aboveWater.SetActive(!value); 
	}

	void Update()
	{

	}
}
