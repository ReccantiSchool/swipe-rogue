using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	public GameObject[] rooms;

	/**
	 * A function that will render the floor
	 */
	public void SetupFloor () {
		
		float camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		float camHeight = Camera.main.orthographicSize;
		GameObject centerRoom = Instantiate (rooms [0], new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		GameObject eastRoom = Instantiate (rooms [1], new Vector3 (2 * camWidth, 0f, 0f), Quaternion.identity) as GameObject;
		GameObject westRoom = Instantiate (rooms [2], new Vector3 (-2 * camWidth, 0f, 0f), Quaternion.identity) as GameObject;
		GameObject northRoom = Instantiate (rooms [3], new Vector3 (0f, 2 * camHeight, 0f), Quaternion.identity) as GameObject;
		GameObject southRoom = Instantiate (rooms [4], new Vector3 (0f, -2 * camHeight, 0f), Quaternion.identity) as GameObject;

		centerRoom.GetComponent<Room> ().East = eastRoom;
		centerRoom.GetComponent<Room> ().West = westRoom;
		centerRoom.GetComponent<Room> ().North = northRoom;
		centerRoom.GetComponent<Room> ().South = southRoom;

		northRoom.GetComponent<Room> ().South = centerRoom;
		southRoom.GetComponent<Room> ().North = centerRoom;
		eastRoom.GetComponent<Room> ().West = centerRoom;
		westRoom.GetComponent<Room> ().East = centerRoom;
	}
}
