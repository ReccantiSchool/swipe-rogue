using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public FloorManager floorScript;

	private Vector3 inputDownPosition;
	private bool isDragging = false;
	private bool shouldBeListening = true;

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
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
//		if (Input.GetKeyUp("left")) {
//			print (floorScript.CanMoveWest ());
//			if (floorScript.CanMoveWest ()) {
//				floorScript.MoveWest ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
////				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}
//		else if (Input.GetKeyUp("right")) {
//			print (floorScript.CanMoveEast ());
//			if (floorScript.CanMoveEast ()) {
//				floorScript.MoveEast ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
////				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}
//		else if (Input.GetKeyUp("down")) {
//			print (floorScript.CanMoveSouth ());
//			if (floorScript.CanMoveSouth ()) {
//				floorScript.MoveSouth ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
////				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}
//		else if (Input.GetKeyUp("up")) {
//			print (floorScript.CanMoveNorth ());
//			if (floorScript.CanMoveNorth ()) {
//				floorScript.MoveNorth ();
//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
//				print (newPosition);
////				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
//			}
//		}

//		Camera.main.transform.Translate (new Vector3 (horizontal, vertical, 0.0f));

		Vector3 currentPosition = Camera.main.transform.position;
		if (Input.GetKey (KeyCode.Mouse0) && shouldBeListening) {
			if (!isDragging)
			{
				isDragging = true;
				inputDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//inputDownPosition = Input.mousePosition;
			}
			inputDownPosition.z = 0;
			
			// get y vector
			Vector3 yVector = new Vector3(inputDownPosition.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
			Debug.DrawLine(inputDownPosition, yVector, Color.blue);

			// get x vector
			Vector3 xVector = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, inputDownPosition.y, 0);
			Debug.DrawLine(inputDownPosition, xVector, Color.cyan);

			// draw the moved vector
			Color validColor = Color.red;
			Vector3 movedVector = inputDownPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (movedVector.sqrMagnitude > 115.0)
			{
				validColor = Color.green;
			}
			Debug.DrawLine(inputDownPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition), validColor);

			// move based on the dragging
			if (xVector.sqrMagnitude > yVector.sqrMagnitude && movedVector.sqrMagnitude > 115.0) {
				if ((inputDownPosition.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) < 0)
				{
					if (floorScript.CanMoveWest())
					{
						floorScript.MoveWest();
					}
				}
				else
				{
					if (floorScript.CanMoveEast())
					{
						floorScript.MoveEast();
					}
				}
				shouldBeListening = false;
			} else if (xVector.sqrMagnitude < yVector.sqrMagnitude && movedVector.sqrMagnitude > 115.0)
			{
				if ((inputDownPosition.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y) < 0)
				{
					if (floorScript.CanMoveSouth())
					{
						floorScript.MoveSouth();
					}
				}
				else
				{
					if (floorScript.CanMoveNorth())
					{
						floorScript.MoveNorth();
					}
				}
				shouldBeListening = false;
			}
			//		else if (Input.GetKeyUp("right")) {
			//			print (floorScript.CanMoveEast ());
			//			if (floorScript.CanMoveEast ()) {
			//				floorScript.MoveEast ();
			//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
			//				print (newPosition);
			////				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
			//			}
			//		}
			//		else if (Input.GetKeyUp("down")) {
			//			print (floorScript.CanMoveSouth ());
			//			if (floorScript.CanMoveSouth ()) {
			//				floorScript.MoveSouth ();
			//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
			//				print (newPosition);
			////				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
			//			}
			//		}
			//		else if (Input.GetKeyUp("up")) {
			//			print (floorScript.CanMoveNorth ());
			//			if (floorScript.CanMoveNorth ()) {
			//				floorScript.MoveNorth ();
			//				Vector3 newPosition = Vector3.MoveTowards(Camera.main.transform.position, floorScript.currentRoom.transform.position, 100f);
			//				print (newPosition);
			////				Camera.main.transform.Translate(new Vector3 (newPosition.x, newPosition.y, 0.0f));
			//			}


		} else {
			isDragging = false;
			shouldBeListening = true;
		}
		Vector3 moveTo = Vector3.MoveTowards (currentPosition, floorScript.currentRoom.transform.position, Time.deltaTime * 100);
		moveTo.z = -10f;
//		Debug.Log (moveTo);
		Camera.main.transform.position = moveTo;
	}
}
