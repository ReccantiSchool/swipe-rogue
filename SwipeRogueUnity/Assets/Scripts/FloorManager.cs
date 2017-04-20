using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	// the initial x coordinate
	public int x;

	// the initial y coordinate
	public int y;

	// the total number of rooms that will be rendered
	public int totalRooms;

	// the room prefab that will be rendered at each coordinate
	public GameObject roomPrefab;

	// the key prefab that will be rendered in a single room
	public GameObject keyPrefab;

	// the exit prefab that will be rendered in a single room
	public GameObject exitPrefab;

    // List holding art assets to be randomly added to rooms
    public GameObject[] roomAssetList;

	// The floor object
	public GameObject floorPrefab;
	private Floor floor;

    private RoomGraph rooms;
	private Dictionary<RoomClass, GameObject> roomPrefabs;
	public GameObject currentRoom { get; set; }

	void Start() {
		GameObject floorObj = Instantiate(floorPrefab, Vector3.zero, Quaternion.identity);
		floor = floorObj.GetComponent<Floor>();
		floor.InitializeFloor();
		SetupFloor();
	}

	/**
	 * A function that will render the floor
	 */
	public void SetupFloor () {   
		GameObject initialRoom;
		if (floor.rooms.TryGetValue(Vector2.zero, out initialRoom)) {
			currentRoom = initialRoom;
		}
	}

	/**
	 * Functions that check to see if the user can move to the
	 * specified rooms
	 */
	public bool CanMoveNorth() {
		return currentRoom.GetComponent<Room>().NorthRoom != null;
	}
	public bool CanMoveSouth() {
		return currentRoom.GetComponent<Room>().SouthRoom != null;
	}
	public bool CanMoveEast() {
		return currentRoom.GetComponent<Room>().EastRoom != null;
	}
	public bool CanMoveWest() {
		return currentRoom.GetComponent<Room>().WestRoom != null;
	}

	/**
	 * Functions that update the current room
	 */
	public void MoveNorth() {
		currentRoom = currentRoom.GetComponent<Room>().NorthRoom;
	}
	public void MoveSouth() {
		currentRoom = currentRoom.GetComponent<Room>().SouthRoom;
	}
	public void MoveEast() {
		currentRoom = currentRoom.GetComponent<Room>().EastRoom;
	}
	public void MoveWest() {
		currentRoom = currentRoom.GetComponent<Room>().WestRoom;
	}
		
}
