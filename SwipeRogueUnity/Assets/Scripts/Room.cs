using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    // references to the neighbors of the room
    public GameObject NorthRoom;
    public GameObject SouthRoom;
    public GameObject EastRoom;
    public GameObject WestRoom;

    // references to different room assets
    public List<GameObject> roomAssets;
    public int minNumberOfAssets = 0;
    public int maxNumberOfAssets = 3;

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

        // scale the room to fit the camera
        var renderer = GetComponent<Renderer>();
        var width    = renderer.bounds.size.x;
        var height   = renderer.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth  = worldScreenHeight / Screen.height * Screen.width;

        float scaleWidth = (float)worldScreenWidth / width;
        float scaleHeight = (float)worldScreenHeight / height;

        Debug.Log(scaleWidth);
        Debug.Log(scaleHeight);

        transform.localScale = new Vector3(scaleWidth, scaleWidth, 1.0f);

        // place assets
        PlaceAssets();
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

    /**
     * Place a few assets randomly in the room
     */
    private void PlaceAssets() {

        // don't place assets if there are no assets in the list
        if (roomAssets.Count > 0) {

            // randomly decide on placing between 0 and 3 assets
            int numAssets = Random.Range(minNumberOfAssets, maxNumberOfAssets);
            for (int i = 0; i < numAssets; i++) {
                
                // fetch a random asset from the list and place it at a random location
                GameObject roomAssetPrefab = roomAssets[Random.Range(0, roomAssets.Count)];
                Vector3 assetPosition = transform.position + new Vector3(
                    Random.Range(-3, 3),
                    Random.Range(-3, 3),
                    0
                );
                GameObject roomAsset = Instantiate(
                    roomAssetPrefab, 
                    assetPosition, 
                    Quaternion.identity);
                roomAsset.transform.parent = transform;
            }
        }
    }

}