using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A class that describes a Room
 */
public class RoomClass {
	public Dictionary<Direction, RoomClass> neighbors;
	public int number;
	private RoomClass parent;
	public int x;
	public int y;

	/**
	* Instantiate a room with a number and it's parent
	*/
	public RoomClass(int number, RoomClass parent) {
		this.number = number;
		this.parent = parent;
		x = parent.x;
		y = parent.y;
		this.neighbors = new Dictionary<Direction, RoomClass>();
		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
			this.neighbors.Add(direction, null);
		}
	}
	public RoomClass(int number, RoomClass parent,int x,int y)
	{
		this.number = number;
		this.parent = parent;
		this.x = x;
		this.y = y;
		this.neighbors = new Dictionary<Direction, RoomClass>();
		foreach (Direction direction in Direction.GetValues(typeof(Direction)))
		{
			this.neighbors.Add(direction, null);
		}
	}
	/**
	* Check to see if the room has a free edge
	*/
	public bool HasFreeEdges() {
		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
			if (this.neighbors [direction] == null) {
				return true;
			}
		}
		return false;
	}

	/**
	* Get a random direction that doesn't have a room next to it
	*/
	public Direction GetRandomFreeDirection() {

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
		return directionList[0];
	}

	/**
	* Print all of the properties of the Room object
	*/
	public override string ToString ()
	{
		string roomString = string.Format ("ID: {0}\n", this.number);
		roomString += (this.parent == null) ? string.Format ("Parent: null\n") : string.Format ("Parent: {0}\n", this.parent.number);
		roomString += string.Format("({0}, {1})\n", this.x, this.y);
		roomString += "Neighbors:\n"; 
		foreach (Direction direction in Direction.GetValues(typeof(Direction))) {
			RoomClass directionRoom = this.neighbors [direction];
			roomString += (directionRoom == null) ? string.Format ("\t{0}: null\n", direction.ToString()) : string.Format ("\t{0}: {1}\n", direction.ToString(), directionRoom.number);
		}
		return roomString;
	}

	/**
	 * Create a custom hash code so we can use this as a key in a dicitonary
	 */
	 public override int GetHashCode() {
		 return this.number.GetHashCode();
	 }

	 /**
	  * Create a function that compares two room classes to determine
	  * if they are equal
	  */
	  public override bool Equals(object obj) {
		  return Equals(obj as RoomClass);
	  }
	  public bool Equals(RoomClass room) {
		  return room != null && room.number != this.number;
	  }
}

/** 
 * A class that will allow us to compare with a dictionary
 */
class RoomClassComparer : IEqualityComparer<RoomClass> {
	public bool Equals(RoomClass room1, RoomClass room2) {
		return room1.number == room2.number;
	}
	public int GetHashCode(RoomClass room) {
		return room.number.GetHashCode();
	}
}

/**
 * An enum which represents the different
 * cardinal directions
 */
public enum Direction {
	North,
	South,
	East,
	West
};

public class FloorManager : MonoBehaviour {

	// the initial x coordinate
	public int x;

	// the initial y coordinate
	public int y;

	// the total number of rooms that will be rendered
	public int totalRooms;

	// the prefab that will be rendered at each coordinate
	public GameObject floorPrefab;
	private List<RoomClass> rooms;
	private Dictionary<RoomClass, GameObject> roomPrefabs;
	public GameObject currentRoom { get; set; }

	/**
	 * A function that will render the floor
	 */
	public void SetupFloor () {   
        // initialize the list of rooms with a single parent room
        rooms = new List<RoomClass> ();
		rooms.Add(new RoomClass(0, null,x,y));  // ###################### initializing first room with overloaded method

        // randomly add 10 connected rooms to the floor. Is increased by one when a room overlaps so there will be (10) rooms
        for (int i = 1; i < totalRooms; i++) {

            bool overlap = false;

            // get the old node and the new node we will be linking to it
            RoomClass newParent = GetRandomRoomWithFreeNeighbors ();
			RoomClass newRoom = new RoomClass (i, newParent);

			// get the directions so we can bind the rooms both ways
			Direction direction = newParent.GetRandomFreeDirection ();
			Direction oppositeDirection;
			if (direction == Direction.North) 
				oppositeDirection = Direction.South;
			else if (direction == Direction.South)
				oppositeDirection = Direction.North;
			else if (direction == Direction.West)
				oppositeDirection = Direction.East;
			else
				oppositeDirection = Direction.West;

            // Sets newRoom's x & y to parents
            newRoom.x = newParent.x;
            newRoom.y = newParent.y;
            
            // Checks which direction the newRoom is compared to its parent and changes x or y accordingly.
            if (oppositeDirection == Direction.South)
            {
                newRoom.x++;
            }
            else if (oppositeDirection == Direction.North)
            {
                newRoom.x--;
            }
            else if (oppositeDirection == Direction.East)
            {
                newRoom.y++;
            }
            else if (oppositeDirection == Direction.West)
            {
                newRoom.y--;
            }

            //print("PARENT ID: " +newParent.number + "  x: " + newParent.x + "  y: " + newParent.y);
            //print("ROOM ID: " + newRoom.number + "   x: " + newRoom.x + "  y: " + newRoom.y + "  # of Rooms: " + rooms.Count);

            // Loops through current rooms in the list and checks if newRoom x&y match any other rooms
            for (int j = 1;j < rooms.Count; j++)
            {
                // If there is a match break out of loop and set overlap to true
                if(newRoom.x == rooms[j].x && newRoom.y == rooms[j].y)
                {
                    overlap = true;
                    // print("OVERLAP: ID: " + newRoom.number + " newRoom.x: " + newRoom.x + "  newRoom.y: " + newRoom.y + " rooms[" + j + "] ID: " + rooms[j].number +" room[" + j+"].x: " + rooms[j].x + "  room[" + j + "].y: " + rooms[j].y);
                    totalRooms++;
                    break;
                }
            }


            // If not true then the newRoom is not overlaping another room so its ok to use
            if (!overlap)
            {
                // link the two nodes both ways
                newParent.neighbors[direction] = newRoom;
                newRoom.neighbors[oppositeDirection] = newParent;

                // add the new room to the list so we can search again
                rooms.Add(newRoom);
            }
		}

		// test that this worked
		// foreach (RoomClass r in rooms) {
		// 	Debug.Log (r.ToString ());
		// }

		// Render each of the rooms on the screen
		float camWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		float camHeight = Camera.main.orthographicSize;
		roomPrefabs = new Dictionary<RoomClass, GameObject>(new RoomClassComparer());
		int counter = 0;
		foreach (RoomClass room in rooms) {
			float xCoordinate = 2 * camHeight * room.x;
			float yCoordinate = 2 * camWidth * room.y;
			GameObject newRoomPrefab = Instantiate (floorPrefab, new Vector3(-yCoordinate, xCoordinate, 0f), Quaternion.identity) as GameObject;
			newRoomPrefab.GetComponentInChildren<TextMesh>().text = counter.ToString();
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
		currentRoom = roomPrefabs[rooms[0]];
	}

	/**
	 * A function to randomly get a room with free edges
	 */ 
	private RoomClass GetRandomRoomWithFreeNeighbors() {

		// create a shallow copy of the list so that we don't remove actual values
		List<RoomClass> shuffleRooms = this.rooms.GetRange (0, this.rooms.Count);

		// randomly remove rooms from the list
		// until we find one with free edges
		while (shuffleRooms.Count > 0) {
			int randomIndex = Random.Range (0, shuffleRooms.Count);
			if (shuffleRooms [randomIndex].HasFreeEdges ()) {
				return shuffleRooms [randomIndex];
			} else {
				shuffleRooms.RemoveAt (randomIndex);
			}
		}
		return null;
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
