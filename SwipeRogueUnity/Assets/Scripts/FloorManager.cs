using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	public GameObject[] rooms;
	public GameObject currentRoom;
	public enum Direction {
		North,
		South,
		East,
		West
	};

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

		// centerRoom.GetComponent<Room> ().East = eastRoom;
		// centerRoom.GetComponent<Room> ().West = westRoom;
		// centerRoom.GetComponent<Room> ().North = northRoom;
		// centerRoom.GetComponent<Room> ().South = southRoom;

		// northRoom.GetComponent<Room> ().South = centerRoom;
		// southRoom.GetComponent<Room> ().North = centerRoom;
		// eastRoom.GetComponent<Room> ().West = centerRoom;
		// westRoom.GetComponent<Room> ().East = centerRoom;

		currentRoom = centerRoom;
	}

//	/**
//	 * Checks to see if the User can move in the given direction from the
//	 * current room
//	 */
//	public bool CanMove (Direction direction) {
//		switch (direction) {
//			case Direction.East:
//				return currentRoom.GetComponent<Room>().East != null;
//			case Direction.West:
//				return currentRoom.GetComponent<Room>().West != null;
//			case Direction.North:
//				return currentRoom.GetComponent<Room>().North != null;
//			case Direction.South:
//				return currentRoom.GetComponent<Room>().South != null;
//			default:
//				return false;
//		}
//	}

	/**
	 * Functions that check to see if the user can move to the
	 * specified rooms
	 */
	// public bool CanMoveNorth() {
	// 	return currentRoom.GetComponent<Room> ().North != null;
	// }
	// public bool CanMoveSouth() {
	// 	return currentRoom.GetComponent<Room> ().South != null;
	// }
	// public bool CanMoveEast() {
	// 	return currentRoom.GetComponent<Room> ().East != null;
	// }
	// public bool CanMoveWest() {
	// 	return currentRoom.GetComponent<Room> ().West != null;
	// }

	// /**
	//  * Functions that update the current room
	//  */
	// public void MoveNorth() {
	// 	currentRoom = currentRoom.GetComponent<Room> ().North;
	// }
	// public void MoveSouth() {
	// 	currentRoom = currentRoom.GetComponent<Room> ().South;
	// }
	// public void MoveEast() {
	// 	currentRoom = currentRoom.GetComponent<Room> ().East;
	// }
	// public void MoveWest() {
	// 	currentRoom = currentRoom.GetComponent<Room> ().West;
	// }
		
}
