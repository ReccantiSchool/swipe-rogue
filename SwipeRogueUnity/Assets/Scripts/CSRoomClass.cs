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

