using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFloor : MonoBehaviour {

    public Transform[] spawnLocations;
    public GameObject RoomPrefab;
    public GameObject RoomClone;

    private List<Room> roomList;
    public int numOfRooms = 10;
    private int roomsMade;


    public string[] roomTypes;

    

    // Use this for initialization
    private void Start () {

        CreateRooms();


	}
	
    // Each Room is created with open slots for adjacent rooms before they are added in

    void CreateRooms()
    {
        // 1: bottom   2: Left   3: Top   4: Right



        //room = new Room[roomNum];
       // room = new List<Room>();
       //
       //
       // room[0] = new Room(0, "entrance");
       //
       // room[0].BuildRoom(1);    //,"entrance");
       //
        for (int i = 0; i < numOfRooms; i++)
        {

            RoomClone = Instantiate(RoomPrefab,transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

       //     // need to a way to track how many rooms are being made
       //     room[i] = new Room(i,room[i-1], roomTypes[UnityEngine.Random.Range(1, 4)]);
       //
       //    // room[i].BuildRoom(UnityEngine.Random.Range(1, 3));  // ,roomTypes[UnityEngine.Random.Range(1, 4)]);
        }


    }

}
