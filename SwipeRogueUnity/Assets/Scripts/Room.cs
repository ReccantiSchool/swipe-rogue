using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public RoomClass roomclass;

    public GameObject NorthRoom;
    public GameObject SouthRoom;
    public GameObject EastRoom;
    public GameObject WestRoom;

    private Renderer northDoor;
    private Renderer southDoor;
    private Renderer eastDoor;
    private Renderer westDoor;

    void Awake() {
        // get references to the doors
        northDoor = transform.Find("NorthDoor").GetComponent<Renderer>();
        southDoor = transform.Find("SouthDoor").GetComponent<Renderer>();
        eastDoor = transform.Find("EastDoor").GetComponent<Renderer>();
        westDoor = transform.Find("WestDoor").GetComponent<Renderer>();
    }

    /**
     * Sets the neighbor for the room in the given direction
     */
    public void SetNeighbor(Direction direction, GameObject room) {
        switch (direction) {
            case (Direction.North):
                NorthRoom = room;
                break;
            case (Direction.South):
                SouthRoom = room;
                break;
            case (Direction.East):
                EastRoom = room;
                break;
            default:
                WestRoom = room;
                break;
        }
        UpdateDoors();
    }

    /**
     * Determine whether or not to render the doors
     */
    private void UpdateDoors() {
        Debug.Log(northDoor);
        northDoor.enabled = (NorthRoom == null) ? false : true;
        southDoor.enabled = (SouthRoom == null) ? false : true;
        eastDoor.enabled = (EastRoom == null) ? false : true;
        westDoor.enabled = (WestRoom == null) ? false : true;
    }

}