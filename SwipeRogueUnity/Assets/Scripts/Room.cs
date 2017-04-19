using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public RoomClass roomclass;

    public GameObject NorthRoom;

    public GameObject SouthRoom;

    public GameObject EastRoom;

    public GameObject WestRoom;

    /**
     * Check to see if this room has a free edge
     */
    public bool HasFreeEdges() {
        if (NorthRoom == null ||
            SouthRoom == null ||
            EastRoom  == null ||
            WestRoom  == null) {
            return true;
        }
        return false;
    }

    /**
     * Returns a random free adjacent direction to the room
     */
    public Direction? GetRandomFreeDirection() {

        // generate a list of free adjacent directions
        List<Direction> freeDirections = new List<Direction>();
        if (NorthRoom == null) {
            freeDirections.Add(Direction.North);
        }
        if (SouthRoom == null) {
            freeDirections.Add(Direction.South);
        }
        if (EastRoom == null) {
            freeDirections.Add(Direction.East);
        }
        if (WestRoom == null) {
            freeDirections.Add(Direction.West);
        }
        
        // return a random free adjacent direction or null if none exist
        if (freeDirections.Count > 0) {
            int freeIndex = Random.Range(0, freeDirections.Count);
            return freeDirections[freeIndex];
        }
        return null;
    }

}