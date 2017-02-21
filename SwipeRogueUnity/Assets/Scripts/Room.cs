using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {


    //private int adjRooms;
    public GameObject RRoom = null;

    public List<Room> connectedRooms;

    protected int num;
    public Room parent;
    public string roomType;


	
	public Room(int num, Room parent, string type)
    {
        this.num = num;
        this.parent = parent;
        roomType = type;
    }
    public Room(int num, string type)
    {
        this.num = num;
        roomType = type;
    }

    // Floor starting room
    public void BuildRoom(int adjRooms )                                          //,string rmType)
    {

        
        

        //roomType = rmType;

        /*switch (roomType)
        {
            case "entrance":
                break;
            case "empty":
                break;
            case "enemy":
                break;
            case "treasure":
                break;
            case "fountain":
                break;
            case "exit":
                break;
            default: // "exit"
                break;
        }*/
    }



    void GetAdjRooms()
    {

    }

    void setAdjRooms()
    {

    }


}
