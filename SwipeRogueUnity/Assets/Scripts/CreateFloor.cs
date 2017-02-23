using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * An enum of the different directions that the
 * user can access
 */
public enum Direction {
	North,
	South,
	East,
	West
};

/**
 * This class holds data related to Rooms in the floor graph.
 * This may later be replaced with a MonoBehavior game object
 */
public class RoomClass {
	private Dictionary<Direction, RoomClass> neighbors;
	private int number;
	private RoomClass parent;

	/**
	 * Instantiate a room with a number and it's parent
	 */
	public RoomClass(int number, RoomClass parent) {
		this.number = number;
		this.parent = parent;
		this.neighbors = new Dictionary<Direction, RoomClass>();
		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
			this.neighbors.Add(direction, null);
		}
	}

	/**
	 * Check to see if the room has a free edge
	 */
	public bool HasFreeEdges() {
		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
			if (this.neighbors [direction] != null) {
				return true;
			}
		}
		return false;
	}

	/**
	 * Get a random direction that doesn't have a room next to it
	 */
	public Direction getRandomFreeDirection() {

		// create a list of directions, and randomly remove values from it
		// until we find a neighbor in the direction that is unoccupied
		Direction[] directions = (Direction[])Direction.GetValues(typeof(Direction));
		List<Direction> directionList = new List<Direction> (directions);
		while (directionList.Count > 1) {
			int randomIndex = Random.Range (0, directionList.Count);
			if (this.neighbors [directionList[randomIndex]] == null) {
				return directionList [randomIndex];
			} else {
				directionList.RemoveAt(randomIndex);
			}
		}
//		List<Direction> directionList = new List<Direction> (Direction.GetValues (typeof(Direction)));
		return directionList[0];
	}

	/**
	 * Print all of the properties of the Room object
	 */
	public override string ToString ()
	{
		string roomString = string.Format ("ID: {0}\n", this.number);
		roomString += (this.parent == null) ? string.Format ("Parent: null\n") : string.Format ("Parent: {0}\n", this.parent.number);
		roomString += "Neighbors:\n"; 
		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
			RoomClass directionRoom = this.neighbors [direction];
			roomString += (directionRoom == null) ? string.Format ("\t{0}: null\n", direction.ToString()) : string.Format ("\t{0}: {1}\n", direction.ToString(), directionRoom.number);
		}
		return roomString;
	}


}

public class CreateFloor : MonoBehaviour {

//    public Transform[] spawnLocations;
//    public GameObject RoomPrefab;
//    public GameObject RoomClone;
//
//    private List<Room> roomList;
//    public int numOfRooms = 10;
//    private int roomsMade;
//
//
//    public string[] roomTypes;
//
//    
//

	private List<RoomClass> rooms;

    // Use this for initialization
    private void Start () {
		rooms = new List<RoomClass> ();
		RoomClass r = new RoomClass (1, null);
		for (int i = 0; i < 10; i++) {
			Debug.Log(r.getRandomFreeDirection());
		}
	}
//		
//    // Each Room is created with open slots for adjacent rooms before they are added in
//
//    void CreateRooms()
//    {
//        // 1: bottom   2: Left   3: Top   4: Right
//
//
//
//        //room = new Room[roomNum];
//       // room = new List<Room>();
//       //
//       //
//       // room[0] = new Room(0, "entrance");
//       //
//       // room[0].BuildRoom(1);    //,"entrance");
//       //
//        for (int i = 0; i < numOfRooms; i++)
//        {
//
//            RoomClone = Instantiate(RoomPrefab,transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
//
//       //     // need to a way to track how many rooms are being made
//       //     room[i] = new Room(i,room[i-1], roomTypes[UnityEngine.Random.Range(1, 4)]);
//       //
//       //    // room[i].BuildRoom(UnityEngine.Random.Range(1, 3));  // ,roomTypes[UnityEngine.Random.Range(1, 4)]);
//        }
//
//
//    }

}
