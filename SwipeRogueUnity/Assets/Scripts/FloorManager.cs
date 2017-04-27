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

	// the floor prefab that will be rendered at each coordinate
	public GameObject floorPrefab;

	// the key prefab that will be rendered in a single room
	public GameObject keyPrefab;

	// the exit prefab that will be rendered in a single room
	public GameObject exitPrefab;

    // the treasure prefab that will be rendered in as many rooms as totalChest
    public GameObject treasurePrefab;

    // List holding art assets to be randomly added to rooms
    public GameObject[] roomAssetList;

    // number or treasure chests
    public int totalChests;

    RoomClass treasureRoom;
    GameObject treasureRoomPrefab;
    GameObject treasureSpawn;

    // List holding used rooms
    List<RoomClass> usedRoomList;

    private RoomGraph rooms;
	private Dictionary<RoomClass, GameObject> roomPrefabs;
	public GameObject currentRoom { get; set; }

	void Awake() {
        usedRoomList = new List<RoomClass>();
        totalChests = 2;
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
        usedRoomList.Add(keyRoom);
        GameObject keyRoomPrefab = roomPrefabs[keyRoom];
        GameObject floorKey = Instantiate(keyPrefab, keyRoomPrefab.transform.position, Quaternion.identity);
		floorKey.transform.parent = roomPrefabs[keyRoom].transform;

        // initialize every art asset in the list to a random room (currently set to place it in a random location in the room)
        for (int i = 0; i < roomAssetList.Length; i++)
        {
            RoomClass assetRoom = rooms.GetRandomRoom();
            GameObject AssetRoomPrefab = roomPrefabs[assetRoom];
            GameObject roomAsset = Instantiate(roomAssetList[i], AssetRoomPrefab.transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), -1), Quaternion.identity);
        }

        // initialize a FloorExit Prefab in one of the furthest rooms from the
        // FloorKey (This is random right now)
        RoomClass exitRoom = rooms.GetFurthestRoomFromNode(keyRoom);
        usedRoomList.Add(exitRoom);
        GameObject exitRoomPrefab = roomPrefabs[exitRoom];
		GameObject floorExit = Instantiate(exitPrefab, exitRoomPrefab.transform.position, Quaternion.identity);
		floorExit.transform.parent = roomPrefabs[exitRoom].transform;

		// set the current room to the first room that was created
		currentRoom = roomPrefabs[rooms.rooms[0]];


        for(int i =0; i < totalChests; i++)
        {
            treasureRoom = rooms.GetRandomRoom();
            

            for (int p = 0; p < usedRoomList.Count; p++)
            {
                if(treasureRoom == usedRoomList[p])
                {
                    treasureRoom = rooms.GetRandomRoom();
                    treasureRoomPrefab = roomPrefabs[treasureRoom];
                    p -= p - 1;
                }
            }

            treasureRoomPrefab = roomPrefabs[treasureRoom];
            usedRoomList.Add(treasureRoom);
            treasureSpawn = Instantiate(treasurePrefab, treasureRoomPrefab.transform.position + new Vector3(0,-1.5f,-1), Quaternion.identity);
            Debug.Log("Treasure Made   " + treasureRoomPrefab.transform.position);
        }

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
	}
	public void MoveSouth() {
		currentRoom = roomPrefabs[currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.South]];
	}
	public void MoveEast() {
		currentRoom = roomPrefabs[currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.East]];
	}
	public void MoveWest() {
		currentRoom = roomPrefabs[currentRoom.GetComponent<Room> ().roomclass.neighbors[Direction.West]];
	}
		
}
