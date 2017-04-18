using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	// the initial x coordinate
	public int x;

	// the initial y coordinate
	public int y;

	// the total number of rooms that will be rendered
	public int totalRooms = 10;

	// the total number of enemies who should be inserted into the scene
	public int numEnemies = 2;

	// the floor prefab that will be rendered at each coordinate
	public GameObject floorPrefab;

	// the key prefab that will be rendered in a single room
	public GameObject keyPrefab;

	// the exit prefab that will be rendered in a single room
	public GameObject exitPrefab;

	// the enemy prefab that will be rendered in a room
	public GameObject enemyPrefab;

	private RoomGraph rooms;
	private Dictionary<RoomClass, GameObject> roomPrefabs;
	public GameObject currentRoom { get; set; }

	void Awake() {
		SetupFloor();
	}

	/**
	 * A function that will render the floor
	 */
	public void SetupFloor () {   
        
		rooms = new RoomGraph();
		rooms.CreateRandomGraph(x, y, totalRooms);

		// Render each of the rooms on the screen
		float camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		float camHeight = Camera.main.orthographicSize;
		roomPrefabs = new Dictionary<RoomClass, GameObject>(new RoomClassComparer());
		int counter = 0;
		foreach (RoomClass room in rooms.rooms) {
			float xCoordinate = 2 * camHeight * room.x;
			float yCoordinate = 2 * camWidth * room.y;
			GameObject newRoomPrefab = Instantiate (floorPrefab, new Vector3(-yCoordinate, xCoordinate, 0f), Quaternion.identity) as GameObject;
			// newRoomPrefab.GetComponentInChildren<TextMesh>().text = counter.ToString();
			newRoomPrefab.GetComponent<Room>().roomclass = room;

			// display the doors on the room prefab and its neighbor
			if (room.neighbors[Direction.West] == null) {
				newRoomPrefab.transform.Find("WestDoor").GetComponent<Renderer>().enabled = false;
			}
			if (room.neighbors[Direction.East] == null) {
				newRoomPrefab.transform.Find("EastDoor").GetComponent<Renderer>().enabled = false;
			}
			if (room.neighbors[Direction.North] == null) {
				newRoomPrefab.transform.Find("NorthDoor").GetComponent<Renderer>().enabled = false;
			}
			if (room.neighbors[Direction.South] == null) {
				newRoomPrefab.transform.Find("SouthDoor").GetComponent<Renderer>().enabled = false;
			}
			counter++;
			roomPrefabs.Add(room, newRoomPrefab);
		}

		// initialize a FloorKey Prefab in a random room
		RoomClass keyRoom = rooms.GetRandomRoom();
		GameObject keyRoomPrefab = roomPrefabs[keyRoom];
		GameObject floorKey = Instantiate(keyPrefab, keyRoomPrefab.transform.position, Quaternion.identity);
		floorKey.transform.parent = roomPrefabs[keyRoom].transform;

		// initialize a FloorExit Prefab in one of the furthest rooms from the
		// FloorKey (This is random right now)
		RoomClass exitRoom = rooms.GetFurthestRoomFromNode(keyRoom);
		GameObject exitRoomPrefab = roomPrefabs[exitRoom];
		GameObject floorExit = Instantiate(exitPrefab, exitRoomPrefab.transform.position, Quaternion.identity);
		floorExit.transform.parent = roomPrefabs[exitRoom].transform;

		// initialize enemies
		for (int i = 0; i < numEnemies; i++) {
			RoomClass enemyRoom = rooms.GetRandomRoom();
			GameObject enemyRoomPrefab = roomPrefabs[enemyRoom];
			GameObject enemy = Instantiate(enemyPrefab, enemyRoomPrefab.transform.position, Quaternion.identity);
			enemy.transform.parent = roomPrefabs[enemyRoom].transform;
		}

		// set the current room to the first room that was created
		currentRoom = roomPrefabs[rooms.rooms[0]];
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
	public bool CanMoveNorth() {
		return currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.North] != null;
	}
	public bool CanMoveSouth() {
		return currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.South] != null;
	}
	public bool CanMoveEast() {
		return currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.East] != null;
	}
	public bool CanMoveWest() {
		return currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.West] != null;
	}

	/**
	 * Functions that update the current room
	 */
	public void MoveNorth() {
		currentRoom = roomPrefabs[currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.North]];
		Debug.Log(CurrentRoomHasEnemy());
	}
	public void MoveSouth() {
		currentRoom = roomPrefabs[currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.South]];
		Debug.Log(CurrentRoomHasEnemy());
	}
	public void MoveEast() {
		currentRoom = roomPrefabs[currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.East]];
		Debug.Log(CurrentRoomHasEnemy());
	}
	public void MoveWest() {
		currentRoom = roomPrefabs[currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.West]];
		Debug.Log(CurrentRoomHasEnemy());
	}

	/**
	 * Checks to see if the current rooms contains an
	 * an enemy prefab
	 */
	public bool CurrentRoomHasEnemy() {
		Transform enemyTransform = currentRoom.transform.Find("Enemy(Clone)");
		if (enemyTransform != null) {
			return true;
		}
		return false;
		// GameObject enemy = currentRoom.transform.Find("Enemy").gameObject;
		// // Debug.Log(enemy);
		// if (enemy != null) {
		// 	return true;
		// }
		// return false;
	}
		
}
