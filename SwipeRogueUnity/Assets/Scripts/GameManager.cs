using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public FloorManager floorScript;

	private Vector3 inputDownPosition;
	private Vector3 inputDownPositionWorld;

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
		Vector3 currentPosition = Camera.main.transform.position;

		if (Input.GetKey (KeyCode.Mouse0) && shouldBeListening) {
			if (!isDragging) {
				isDragging = true;
				inputDownPosition = Input.mousePosition;
				inputDownPositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
			inputDownPosition.z = 0;
			inputDownPositionWorld.z = 0;

			// get the mouse vector in world coordinates
			Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouseWorld.z = 0;

			// draw the moved vector
			Color validColor = Color.red;
			Vector3 movedVector = inputDownPositionWorld - mouseWorld;
			if (movedVector.sqrMagnitude > 20.0)
			{
				validColor = Color.green;
			}
			Debug.DrawLine(inputDownPositionWorld, mouseWorld, validColor);

			// get y vector
			Vector3 yVector = new Vector3(inputDownPositionWorld.x, mouseWorld.y, 0);
			Debug.DrawLine(inputDownPositionWorld, yVector, Color.blue);

			// get x vector
			Vector3 xVector = new Vector3(mouseWorld.x, inputDownPositionWorld.y, 0);
			Debug.DrawLine(inputDownPositionWorld, xVector, Color.cyan);

			// move based on the dragging
			// if ((xVector.sqrMagnitude > yVector.sqrMagnitude) && movedVector.sqrMagnitude > 20.0) {
			if (Mathf.Abs(movedVector.x) > Mathf.Abs(movedVector.y) && movedVector.sqrMagnitude > 20.0) {
				if (xVector.x - inputDownPositionWorld.x > 0)
				{
					Debug.Log("West");					
					if (floorScript.CanMoveWest())
					{
						floorScript.MoveWest();
					}
				}
				else
				{
					Debug.Log("East");					
					if (floorScript.CanMoveEast())
					{
						floorScript.MoveEast();
					}
				}
				shouldBeListening = false;
			} 
			// else if ((xVector.sqrMagnitude < yVector.sqrMagnitude) && movedVector.sqrMagnitude > 20.0) {
			else if (Mathf.Abs(movedVector.x) < Mathf.Abs(movedVector.y) && movedVector.sqrMagnitude > 20.0) {
				if (yVector.y - inputDownPositionWorld.y > 0) {
					Debug.Log("South");					
					if (floorScript.CanMoveSouth()) {
						floorScript.MoveSouth();
					}
				}
				else {
					Debug.Log("North");					
					if (floorScript.CanMoveNorth()) {
						floorScript.MoveNorth();
					}
				}
				shouldBeListening = false;
			}
		}
		else {
			isDragging = false;
		}
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			shouldBeListening = true;
		}

		Vector3 moveTo = Vector3.MoveTowards (currentPosition, floorScript.currentRoom.transform.position, Time.deltaTime * 100);
		moveTo.z = -10f;
		Camera.main.transform.position = moveTo;
	}
}
