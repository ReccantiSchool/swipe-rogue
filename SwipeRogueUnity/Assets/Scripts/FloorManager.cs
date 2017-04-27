using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	// The floor object
	public GameObject floorPrefab;

	// a reference to the floor from the floorPrefab
	private Floor floor;

//<<<<<<< HEAD
//	// the exit prefab that will be rendered in a single room
//	public GameObject exitPrefab;

//    // the treasure prefab that will be rendered in as many rooms as totalChest
//    public GameObject treasurePrefab;

//    // List holding art assets to be randomly added to rooms
//    public GameObject[] roomAssetList;

//    // number or treasure chests
//    public int totalChests;

//    RoomClass treasureRoom;
//    GameObject treasureRoomPrefab;
//    GameObject treasureSpawn;

//    // List holding used rooms
//    List<RoomClass> usedRoomList;

//    private RoomGraph rooms;
//	private Dictionary<RoomClass, GameObject> roomPrefabs;
//	public GameObject currentRoom { get; set; }

//	void Awake() {
//        usedRoomList = new List<RoomClass>();
//        totalChests = 2;
//=======
	// the room that the player is currently in
	public GameObject currentRoom { get; set; }

	void Start() {
		GameObject floorObj = Instantiate(floorPrefab, Vector3.zero, Quaternion.identity);
		floor = floorObj.GetComponent<Floor>();
		floor.InitializeFloor();
//>>>>>>> master
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
//<<<<<<< HEAD

//		// initialize a FloorKey Prefab in a random room
//		RoomClass keyRoom = rooms.GetRandomRoom();
//        usedRoomList.Add(keyRoom);
//        GameObject keyRoomPrefab = roomPrefabs[keyRoom];
//        GameObject floorKey = Instantiate(keyPrefab, keyRoomPrefab.transform.position, Quaternion.identity);
//		floorKey.transform.parent = roomPrefabs[keyRoom].transform;

//        // initialize every art asset in the list to a random room (currently set to place it in a random location in the room)
//        for (int i = 0; i < roomAssetList.Length; i++)
//        {
//            RoomClass assetRoom = rooms.GetRandomRoom();
//            GameObject AssetRoomPrefab = roomPrefabs[assetRoom];
//            GameObject roomAsset = Instantiate(roomAssetList[i], AssetRoomPrefab.transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), -1), Quaternion.identity);
//        }

//        // initialize a FloorExit Prefab in one of the furthest rooms from the
//        // FloorKey (This is random right now)
//        RoomClass exitRoom = rooms.GetFurthestRoomFromNode(keyRoom);
//        usedRoomList.Add(exitRoom);
//        GameObject exitRoomPrefab = roomPrefabs[exitRoom];
//		GameObject floorExit = Instantiate(exitPrefab, exitRoomPrefab.transform.position, Quaternion.identity);
//		floorExit.transform.parent = roomPrefabs[exitRoom].transform;

//		// set the current room to the first room that was created
//		currentRoom = roomPrefabs[rooms.rooms[0]];


//        for(int i =0; i < totalChests; i++)
//        {
//            treasureRoom = rooms.GetRandomRoom();
            

//            for (int p = 0; p < usedRoomList.Count; p++)
//            {
//                if(treasureRoom == usedRoomList[p])
//                {
//                    treasureRoom = rooms.GetRandomRoom();
//                    treasureRoomPrefab = roomPrefabs[treasureRoom];
//                    p -= p - 1;
//                }
//            }

//            treasureRoomPrefab = roomPrefabs[treasureRoom];
//            usedRoomList.Add(treasureRoom);
//            treasureSpawn = Instantiate(treasurePrefab, treasureRoomPrefab.transform.position + new Vector3(0,-1.5f,-1), Quaternion.identity);
//            Debug.Log("Treasure Made   " + treasureRoomPrefab.transform.position);
//        }

//=======
//>>>>>>> master
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
