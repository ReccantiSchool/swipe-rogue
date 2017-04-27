using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vector2GameObjectDictionary: SerializableDictionary<Vector2, GameObject> {}

// [CreateAssetMenu(menuName = "Floor")]
public class Floor : MonoBehaviour {

	// a dictionary with references to all the rooms and their locations
	[SerializeField]
	public Vector2GameObjectDictionary roomsStore = new Vector2GameObjectDictionary();
	public Dictionary<Vector2, GameObject> rooms {
		get { return roomsStore.dictionary; }
	}

	// the default prefab that will be used to render a room
	public GameObject defaultRoomPrefab;

	// the prefab that will render the key that the player needs to pick up
	public GameObject keyPrefab;

	// the prefab that will render the door that the user needs to unlock
	public GameObject doorPrefab;

	// the enemy that will be rendered at random locations on the floor
	public GameObject enemyPrefab;

    // the treasure chest that will be rendered at a random location on the floor
    public GameObject treasureChestPrefab;

	// is this a custom floor. If it is, the random generation function will not be run
	public bool isCustomFloor = false;

	// the total number of rooms that should be generated
	public int totalRooms = 10;

	// the total number of enemies that will appear on the floor
	public int numEnemies = 2;

    // the total number of treasure chests that will appear on the floor
    public int numTreasureChests = 2;

	// references to the camera dimensions that will be calulated once
	private float camWidth;
	private float camHeight;

	// Use this for initialization
	void Start () {
		
	}

	/**
	 * a method that can be called to initialize the floor manually
	 */
	public void InitializeFloor() {
		camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		camHeight = Camera.main.orthographicSize;
		if (!isCustomFloor) {
			CreateRandomFloor(totalRooms);
		}
		foreach(var room in rooms) {
			room.Value.GetComponent<Room>().UpdateDoors();
		}
	}

	/**
	 * Randomly generates a connected graph of rooms
	 */
	public void CreateRandomFloor(int roomCount) {

		// initialize the rooms list with a single parent node
		AddRoomAtLocation(Vector2.zero);

		// create a List to hold the rooms we will randomly put items in
		List<GameObject> itemRooms = new List<GameObject>();

		// cycle through the total number of rooms wanted and add them to the list
		for (int i = 0; i < roomCount - 1; i++) {
			
			// get a random room with a free neighbor
			// and make sure that it doesn't conflict with another neighbor
			Vector2? parentRoomLocation = GetRandomRoomLocationWithFreeNeighbors();
			if (parentRoomLocation != null) {
				Vector2 indexLocation = parentRoomLocation ?? default(Vector2);
				Room parentRoom = rooms[indexLocation].GetComponent<Room>();

				// instantiate the Room at the given transform
				Direction? randomDirection = GetRandomFreeDirection(indexLocation);
				if (randomDirection != null) {
					Direction adjacentDirection = randomDirection ?? default(Direction);
					Vector2 newLocation;
					switch (adjacentDirection) {
						case Direction.North:
							newLocation = indexLocation + Vector2.up;
							break;
						case Direction.South:
							newLocation = indexLocation + Vector2.down;
							break;
						case Direction.East:
							newLocation = indexLocation + Vector2.right;
							break;
						default:
							newLocation = indexLocation + Vector2.left;
							break;
					}
					GameObject newRoom = AddRoomAtLocation(newLocation);
					itemRooms.Add(newRoom);

					// bind the new room to its parent
					BindRooms(rooms[indexLocation], newRoom, adjacentDirection);
				}
			}
		}

		// instantiate the floor key
		int keyIndex = Random.Range(0, itemRooms.Count);
		GameObject keyRoom = itemRooms[keyIndex];
		GameObject key = Instantiate(keyPrefab, keyRoom.transform.position, Quaternion.identity);
		key.transform.parent = keyRoom.transform;
		itemRooms.RemoveAt(keyIndex);

		int doorIndex = Random.Range(0, itemRooms.Count);
		GameObject doorRoom = itemRooms[doorIndex];
		GameObject door = Instantiate(doorPrefab, doorRoom.transform.position, Quaternion.identity);
		door.transform.parent = doorRoom.transform;
		itemRooms.RemoveAt(doorIndex);
		
		// instantiate all enemies
		for(int i = 0; i < numEnemies; i++) {
			int enemyIndex = Random.Range(0, itemRooms.Count);
			GameObject enemyRoom = itemRooms[enemyIndex];
			GameObject enemy = Instantiate(enemyPrefab, enemyRoom.transform.position, Quaternion.identity);
			enemy.transform.parent = enemyRoom.transform;
			itemRooms.RemoveAt(enemyIndex);
		}

        // instantiate all treasure chests
        for (int i = 0; i < numTreasureChests; i++)
        {
            int chestIndex = Random.Range(0, itemRooms.Count);
            GameObject chestRoom = itemRooms[chestIndex];
            GameObject chest = Instantiate(treasureChestPrefab, chestRoom.transform.position, Quaternion.identity);
            chest.transform.parent = chestRoom.transform;
            itemRooms.RemoveAt(chestIndex);
        }
	}

	/**
	 * A function that initializes a room at a given location
	 */
	private GameObject AddRoomAtLocation(Vector2 gridPosition) {
		Vector3 position = new Vector3(gridPosition.x * camWidth * 2, gridPosition.y * camHeight * 2, 0);
		GameObject room = Instantiate(defaultRoomPrefab, position, Quaternion.identity);
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
		if (rooms.TryGetValue(gridPostion + Vector2.up, out defaultRoom)) {
			adjacentDirection.Add(Direction.North);
		}
		// test the south room
		if (rooms.TryGetValue(gridPostion + Vector2.down, out defaultRoom)) {
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
		if (!rooms.TryGetValue(gridPostion + Vector2.up, out defaultRoom)) {
			freeDirection.Add(Direction.North);
		}
		// test the south room
		if (!rooms.TryGetValue(gridPostion + Vector2.down, out defaultRoom)) {
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

	/**
	 * Gets a random direction of a free neighbor from the current grid position
	 */
	private Direction GetRandomFreeDirection(Vector2 gridPosition) {
		List<Direction> freeDirection = GetAllFreeDirections(gridPosition);
		int randomIndex = Random.Range(0, freeDirection.Count);
		return freeDirection[randomIndex];
	}

	/**
	 * Set two rooms as neighbors to each other
	 */
	private void BindRooms(GameObject fromRoom, GameObject toRoom, Direction inDirection) {
		fromRoom.GetComponent<Room>().SetNeighbor(inDirection, toRoom);
		toRoom.GetComponent<Room>().SetNeighbor(inDirection.OppositeDirection(), fromRoom);
	}
}
