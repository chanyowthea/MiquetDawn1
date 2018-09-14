using UnityEngine;
using System; 

[RequireComponent(typeof(Camera))]
//This script enables underwater effects. Attach to main camera.
public class UnderWater : MonoBehaviour
{
	//Define variables
	[SerializeField] int underwaterLevel = 10;

	//The scene's default fog settings
	private bool defaultFog;
	private Color defaultFogColor;
	private float defaultFogDensity;
	private Material defaultSkybox;
	private Color defaultBGColor;
	Material noSkybox;

	void Start()
	{
		defaultFog = RenderSettings.fog;
		defaultFogColor = RenderSettings.fogColor;
		defaultFogDensity = RenderSettings.fogDensity;
		defaultSkybox = RenderSettings.skybox;
		//Set the background color
		//GetComponent<Camera>().backgroundColor = new Color(0, 0.4f, 0.7f, 1);
	}

	[SerializeField] bool isFog = true; 
	[SerializeField] Color fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
	[SerializeField] float density = 0.04f;
	[SerializeField] Color backgroundColor = new Color(0, 0.4f, 0.7f, 1); 

	void Update()
	{
		if (transform.position.y < underwaterLevel)
		{
			RenderSettings.fog = isFog;
			RenderSettings.fogColor = fogColor;
			RenderSettings.fogDensity = density;
			RenderSettings.skybox = noSkybox;
			//GetComponent<Camera>().backgroundColor = backgroundColor;
		}
		else
		{
			//GetComponent<Camera>().backgroundColor = defaultBGColor;
			RenderSettings.fog = defaultFog;
			RenderSettings.fogColor = defaultFogColor;
			RenderSettings.fogDensity = defaultFogDensity;
			RenderSettings.skybox = defaultSkybox;
		}
	}
}