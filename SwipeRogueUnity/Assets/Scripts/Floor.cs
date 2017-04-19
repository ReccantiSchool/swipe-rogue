using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	public Dictionary<Vector2, GameObject> rooms = new Dictionary<Vector2, GameObject>();
	public GameObject roomPrefab;

	// references to the camera dimensions that will be calulated once
	private float camWidth;
	private float camHeight;


	// Use this for initialization
	void Start () {
		camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		camHeight = Camera.main.orthographicSize;
		CreateRandomFloor(50);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateRandomFloor(int roomCount) {

		// initialize the rooms list with a single parent node
		AddRoomAtLocation(Vector2.zero);

		// cycle through the total number of rooms wanted and add them to the list
		for (int i = 0; i < roomCount - 1; i++) {
			
			// get a random room with a free neighbor
			// and make sure that it doesn't conflict with another neighbor
			Vector2? parentRoomLocation = GetRandomRoomLocationWithFreeNeighbors();
			Debug.Log(parentRoomLocation);
			if (parentRoomLocation != null) {
				Vector2 indexLocation = parentRoomLocation ?? default(Vector2);
				Room parentRoom = rooms[indexLocation].GetComponent<Room>();

				// instantiate the Room at the given transform
				Direction? adjacentDirection = GetRandomFreeDirection(indexLocation);
				Vector2 newLocation;
				switch (adjacentDirection) {
					case Direction.North:
						newLocation = indexLocation + Vector2.down;
						break;
					case Direction.South:
						newLocation = indexLocation + Vector2.up;
						break;
					case Direction.East:
						newLocation = indexLocation + Vector2.right;
						break;
					default:
						newLocation = indexLocation + Vector2.left;
						break;
				}
				GameObject newRoom = AddRoomAtLocation(newLocation);

			}

			
			// bind it both ways
		}
	}

	/**
	 * A function that initializes a room at a given location
	 */
	private GameObject AddRoomAtLocation(Vector2 gridPosition) {
		Vector3 position = new Vector3(gridPosition.x * camWidth * 2, gridPosition.y * camHeight * 2, 0);
		GameObject room = Instantiate(roomPrefab, position, Quaternion.identity);
		room.transform.parent = transform;
		rooms.Add(gridPosition, room);
		return room;
	}

	/**
	 * A function that randomly gets a room location with at least one free edge.
	 * If no rooms in the list have a free edge, this function returns null
	 */
	private Vector2? GetRandomRoomLocationWithFreeNeighbors() {

		// make a list of all of the room locations in dictionary
		List<Vector2> shuffleLocations = new List<Vector2>(this.rooms.Keys);

		// cycle through the room locations until we find one one with a free edge
		while (shuffleLocations.Count > 0) {

			// get a room at a random index
			int randomIndex = Random.Range (0, shuffleLocations.Count);

			// return the room location if it has a free edge. Otherwise remove it and iterate the loop
			if (GetAllFreeDirections(shuffleLocations[randomIndex]).Count > 0) {
				return shuffleLocations[randomIndex];
			} else {
				shuffleLocations.RemoveAt(randomIndex);
			}
		}

		// return null if no free rooms were found
		return null;
	}

	/**
	 * Returns a list of all adjacent rooms to a position in the grid
	 */
	private List<Direction> GetAllAdjacentDirections(Vector2 gridPostion) {
		List<Direction> adjacentDirection = new List<Direction>();
		GameObject defaultRoom;

		// test the north room
		if (rooms.TryGetValue(gridPostion + Vector2.down, out defaultRoom)) {
			adjacentDirection.Add(Direction.North);
		}
		// test the south room
		if (rooms.TryGetValue(gridPostion + Vector2.up, out defaultRoom)) {
			adjacentDirection.Add(Direction.South);
		}
		// test the east room
		if (rooms.TryGetValue(gridPostion + Vector2.right, out defaultRoom)) {
			adjacentDirection.Add(Direction.East);
		}
		// test the west room
		if (rooms.TryGetValue(gridPostion + Vector2.left, out defaultRoom)) {
			adjacentDirection.Add(Direction.West);
		}

		return adjacentDirection;
	}

	/**
	 * Returns a list of all adjacent free spaces to a position in the grid
	 */
	private List<Direction> GetAllFreeDirections(Vector2 gridPostion) {
		List<Direction> freeDirection = new List<Direction>();
		GameObject defaultRoom;

		// test the north room
		if (!rooms.TryGetValue(gridPostion + Vector2.down, out defaultRoom)) {
			freeDirection.Add(Direction.North);
		}
		// test the south room
		if (!rooms.TryGetValue(gridPostion + Vector2.up, out defaultRoom)) {
			freeDirection.Add(Direction.South);
		}
		// test the east room
		if (!rooms.TryGetValue(gridPostion + Vector2.right, out defaultRoom)) {
			freeDirection.Add(Direction.East);
		}
		// test the west room
		if (!rooms.TryGetValue(gridPostion + Vector2.left, out defaultRoom)) {
			freeDirection.Add(Direction.West);
		}

		return freeDirection;
	}

	private Direction GetRandomFreeDirection(Vector2 gridPosition) {
		List<Direction> freeDirection = GetAllFreeDirections(gridPosition);
		int randomIndex = Random.Range(0, freeDirection.Count);
		return freeDirection[randomIndex];
	}

	private void BindRooms(GameObject fromRoom, GameObject toRoom, Direction inDirection) {

	}
}
