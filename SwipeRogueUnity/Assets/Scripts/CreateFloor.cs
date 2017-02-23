using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * An enum of the different directions that the
 * user can access
 */
enum Direction {
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
    // Use this for initialization
    private void Start () {

		RoomClass r = new RoomClass (1, null);
		Debug.Log(r.ToString ());


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
