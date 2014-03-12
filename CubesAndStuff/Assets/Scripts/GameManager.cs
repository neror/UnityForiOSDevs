using UnityEngine;
using System.Collections;
namespace CubesAndStuff
{
	public class GameManager : MonoBehaviour
	{
		public GameObject cubeStackPrefab;
		public Transform player;
		
		private GameObject liveCubes;
		private Vector3 playerStartPos;
		
		void Start()
		{
			liveCubes = Instantiate(cubeStackPrefab) as GameObject;
			playerStartPos = player.transform.position;
		}
		
		void OnGUI()
		{
			if(GUI.Button(new Rect(10, 10, 80, 22), "Reset")) {
				Destroy(liveCubes);
				liveCubes = Instantiate(cubeStackPrefab) as GameObject;
				player.transform.position = playerStartPos;
				player.gameObject.SendMessage("OnGameReset", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}