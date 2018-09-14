using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraAssist : MonoBehaviour
{
	[SerializeField] BlurOptimized _blur;
	[SerializeField] int _underwaterLevel = 10;

	bool _isEnterWater;
	bool _isInited;
	public bool IsEnterWater
	{
		set
		{
			// 初始化之后才执行
			if (_isInited)
			{
				if (value == _isEnterWater)
				{
					return;
				}
			}
			else
			{
				_isInited = true; 
			}
			_isEnterWater = value; 
			if (Singleton._eventManager._onEnterWater != null)
			{
				Singleton._eventManager._onEnterWater(value);
			}
		}
		get
		{
			return _isEnterWater;
		}
	}

	void Update()
	{
		IsEnterWater = transform.position.y < _underwaterLevel;
	}

	void Start()
	{
		Singleton._eventManager._onEnterWater += OnEnterWater;
		//IsEnterWater = false; 
	}

	void OnDestroy()
	{
		Singleton._eventManager._onEnterWater -= OnEnterWater;
	}

	void OnEnterWater(bool value)
	{
		_blur.enabled = value;
	}
}
