using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public GameObject cubeStackPrefab;
	public Transform sphere;
	
	private GameObject liveCubes;
	private Vector3 sphereStartPos;
	
	void Start()
	{
		liveCubes = Instantiate(cubeStackPrefab) as GameObject;
		sphereStartPos = sphere.transform.position;
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(10, 10, 80, 22), "Reset")) {
			Destroy(liveCubes);
			liveCubes = Instantiate(cubeStackPrefab) as GameObject;
			sphere.transform.position = sphereStartPos;
			sphere.gameObject.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
		}
	}
}
