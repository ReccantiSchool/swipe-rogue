using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public RoomClass roomclass;

    [HideInInspector]
    public GameObject NorthRoom;

    [HideInInspector]
    public GameObject SouthRoom;

    [HideInInspector]
    public GameObject EastRoom;

    [HideInInspector]
    public GameObject WestRoom;

    // public Dictionary<Direction, GameObject> neighbors { get; set; }
    // public int number { get; set; }

    // public GameObject parent { get; set; }

    // public int x { get; set; }

    // public int y { get; set; }


    // //private int adjRooms;
    // public GameObject Room = null;

    // public List<Room> connectedRooms;

    // protected int num;
    // public Room parent;
    // public string roomType;


	
	// public Room(int num, Room parent, string type)
    // {
    //     this.num = num;
    //     this.parent = parent;
    //     roomType = type;
    // }
    // public Room(int num, string type)
    // {
    //     this.num = num;
    //     roomType = type;
    // }

    // // Floor starting room
    // public void BuildRoom(int adjRooms )                                          //,string rmType)
    // {

        
        

    //     //roomType = rmType;

    //     /*switch (roomType)
    //     {
    //         case "entrance":
    //             break;
    //         case "empty":
    //             break;
    //         case "enemy":
    //             break;
    //         case "treasure":
    //             break;
    //         case "fountain":
    //             break;
    //         case "exit":
    //             break;
    //         default: // "exit"
    //             break;
    //     }*/
    // }



    // void GetAdjRooms()
    // {

    // }

    // void setAdjRooms()
    // {

    // }


}



// /**
//  * This class holds data related to Rooms in the floor graph.
//  * This may later be replaced with a MonoBehavior game object
//  */
// public class RoomClass {
// 	public Dictionary<Direction, RoomClass> neighbors;
// 	public int number;
// 	private RoomClass parent;
//     public int x;
//     public int y;

//     /**
// 	 * Instantiate a room with a number and it's parent
// 	 */
//     public RoomClass(int number, RoomClass parent) {
// 		this.number = number;
// 		this.parent = parent;
//         x = parent.x;
//         y = parent.y;
// 		this.neighbors = new Dictionary<Direction, RoomClass>();
// 		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
// 			this.neighbors.Add(direction, null);
// 		}
// 	}
//     public RoomClass(int number, RoomClass parent,int x,int y)
//     {
//         this.number = number;
//         this.parent = parent;
//         this.x = x;
//         this.y = y;
//         this.neighbors = new Dictionary<Direction, RoomClass>();
//         foreach (Direction direction in Direction.GetValues(typeof(Direction)))
//         {
//             this.neighbors.Add(direction, null);
//         }
//     }
// 	/**
// 	 * Check to see if the room has a free edge
// 	 */
// 	public bool HasFreeEdges() {
// 		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
// 			if (this.neighbors [direction] == null) {
// 				return true;
// 			}
// 		}
// 		return false;
// 	}

// 	/**
// 	 * Get a random direction that doesn't have a room next to it
// 	 */
// 	public Direction GetRandomFreeDirection() {

// 		// create a list of directions, and randomly remove values from it
// 		// until we find a neighbor in the direction that is unoccupied
// 		Direction[] directions = (Direction[])Direction.GetValues(typeof(Direction));
// 		List<Direction> directionList = new List<Direction> (directions);
// 		while (directionList.Count > 1) {
// 			int randomIndex = Random.Range (0, directionList.Count);
// 			if (this.neighbors [directionList[randomIndex]] == null) {
// 				return directionList [randomIndex];
// 			} else {
// 				directionList.RemoveAt(randomIndex);
// 			}
// 		}
// 		return directionList[0];
// 	}

// 	/**
// 	 * Print all of the properties of the Room object
// 	 */
// 	public override string ToString ()
// 	{
// 		string roomString = string.Format ("ID: {0}\n", this.number);
// 		roomString += (this.parent == null) ? string.Format ("Parent: null\n") : string.Format ("Parent: {0}\n", this.parent.number);
// 		roomString += string.Format("({0}, {1})", this.x, this.y);
// 		roomString += "Neighbors:\n"; 
// 		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
// 			RoomClass directionRoom = this.neighbors [direction];
// 			roomString += (directionRoom == null) ? string.Format ("\t{0}: null\n", direction.ToString()) : string.Format ("\t{0}: {1}\n", direction.ToString(), directionRoom.number);
// 		}
// 		return roomString;
// 	}


// }
