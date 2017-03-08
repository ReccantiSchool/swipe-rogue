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
