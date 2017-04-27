using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	// The floor object
	public GameObject floorPrefab;

	// a reference to the floor from the floorPrefab
	[HideInInspector]
	public Floor floor;

	// the room that the player is currently in
	private GameObject _currentRoom;
	public Room currentRoomScript { get; set; }
	public GameObject currentRoom { 
		get { return _currentRoom; } 
		set { 
			_currentRoom = value;
			currentRoomScript = _currentRoom.GetComponent<Room>();
		} 
	}


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
		return currentRoomScript.NorthRoom != null;
	}
	public bool CanMoveSouth() {
		return currentRoomScript.SouthRoom != null;
	}
	public bool CanMoveEast() {
		return currentRoomScript.EastRoom != null;
	}
	public bool CanMoveWest() {
		return currentRoomScript.WestRoom != null;
	}

	/**
	 * Functions that update the current room
	 */
	public void MoveNorth() {
		currentRoom = currentRoomScript.NorthRoom;
		currentRoomScript.ActivateEnemy();
	}
	public void MoveSouth() {
		currentRoom = currentRoomScript.SouthRoom;
		currentRoomScript.ActivateEnemy();
	}
	public void MoveEast() {
		currentRoom = currentRoomScript.EastRoom;
		currentRoomScript.ActivateEnemy();
	}
	public void MoveWest() {
		currentRoom = currentRoomScript.WestRoom;
		currentRoomScript.ActivateEnemy();
	}
}
