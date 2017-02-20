using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public FloorManager floorScript;

	private Vector3 inputDownPosition;
	private bool isDragging = false;

	/**
	 * This function is called before the Start() method.
	 * It creates a GameManager singleton and initializes
	 * the floor
	 */
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
		floorScript = GetComponent<FloorManager> ();
		floorScript.SetupFloor ();
	}
	
	// Update is called once per frame
	void Update () {

		// get the direction the player wants to move
//		float horizontal = Input.GetAxisRaw ("Horizontal");
//		float vertical = Input.GetAxisRaw ("Vertical");
//		if (Input.GetKeyUp("left")) {
//			print (floorScript.CanMoveWest ());
//			if (floorScript.CanMoveWest ()) {
//				floorScript.MoveWest ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
//				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}
//		else if (Input.GetKeyUp("right")) {
//			print (floorScript.CanMoveEast ());
//			if (floorScript.CanMoveEast ()) {
//				floorScript.MoveEast ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
//				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}
//		else if (Input.GetKeyUp("down")) {
//			print (floorScript.CanMoveSouth ());
//			if (floorScript.CanMoveSouth ()) {
//				floorScript.MoveSouth ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
//				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}
//		if (Input.GetKeyUp("up")) {
//			print (floorScript.CanMoveNorth ());
//			if (floorScript.CanMoveNorth ()) {
//				floorScript.MoveNorth ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
//				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}

//		Camera.main.transform.Translate (new Vector3 (horizontal, vertical, 0.0f));

		if (Input.GetKey (KeyCode.Mouse0)) {
			if (!isDragging) {
				isDragging = true;
				inputDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
			Debug.Log (inputDownPosition);
			inputDownPosition.z = 0;
			Debug.DrawLine (inputDownPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition), Color.red);
		} else {
			isDragging = false;
		}
	}
}
