using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    // references to the neighbors of the room
    public GameObject NorthRoom;
    public GameObject SouthRoom;
    public GameObject EastRoom;
    public GameObject WestRoom;

    // renderers for the doors of the room
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
    }

    /**
     * Determine whether or not to render the doors
     */
    public void UpdateDoors() {
        northDoor.enabled = (NorthRoom == null) ? false : true;
        southDoor.enabled = (SouthRoom == null) ? false : true;
        eastDoor.enabled = (EastRoom == null) ? false : true;
        westDoor.enabled = (WestRoom == null) ? false : true;
    }

    /**
     * Determines if the current room has an enemy in it
     */
    public bool HasEnemy() {
        if (transform.Find("Enemy") != null || transform.Find("Enemy(Clone)") != null) {
            return true;
        } else {
            return false;
        }
    }

    /**
     * Activate an enemy if it exists
     */
    public void ActivateEnemy() {
        if (transform.Find("Enemy") != null) {
            transform.Find("Enemy").GetComponent<Enemy>().shouldAttack = true;
        } else if (transform.Find("Enemy(Clone)") != null) {
            transform.Find("Enemy(Clone)").GetComponent<Enemy>().shouldAttack = true;
        }
    }

}