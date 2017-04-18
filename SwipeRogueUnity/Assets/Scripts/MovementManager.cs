using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

	private Vector3 inputDownPosition;
	private Vector3 inputDownPositionWorld;

	private bool isDragging = false;
	private bool shouldBeListening = true;

	private FloorManager floorScript;

	/**
	 * Controls whether the user is currently able to move
	 */
	public bool canMove = true;

	void Awake () {
		floorScript = GetComponent<FloorManager>();
	}


	// Update is called once per frame
	void Update () {
		// prevent movement if the current room has an enemy
		canMove = !floorScript.CurrentRoomHasEnemy();
		Vector3 currentPosition = Camera.main.transform.position;
		if (canMove) {
			# if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEB_PLAYER
			moveMouse();
			# else
			moveTouch();
			# endif
		}
		Vector3 moveTo = Vector3.MoveTowards (currentPosition, floorScript.currentRoom.transform.position, Time.deltaTime * 100);
		moveTo.z = -10f;
		Camera.main.transform.position = moveTo;
	}

	/**
	 * A function that handles movement with touch controls
	 */
	private void moveTouch() {
		if (Input.touchCount > 0) {
			if (shouldBeListening) {
				Touch myTouch = Input.touches[0];
				// log the position where the user first touched inputDownPosition
				if (myTouch.phase == TouchPhase.Began) {
					inputDownPosition = myTouch.position;
					inputDownPositionWorld = Camera.main.ScreenToWorldPoint(inputDownPosition);
					inputDownPosition.z = 0;
					inputDownPositionWorld.z = 0;
				}
				else if (myTouch.phase == TouchPhase.Ended) {
					Debug.Log("over");
				} 
				else if (myTouch.phase == TouchPhase.Moved) {
					// get the touch vector in world coordinates
					Vector3 touchWorld = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
					touchWorld.z = 0;

					// draw the moved vector
					Color validColor = Color.red;
					Vector3 movedVector = inputDownPositionWorld - touchWorld;
					if (movedVector.sqrMagnitude > 20.0)
					{
						validColor = Color.green;
					}
					Debug.DrawLine(inputDownPositionWorld, touchWorld, validColor);

					// get y vector
					Vector3 yVector = new Vector3(inputDownPositionWorld.x, touchWorld.y, 0);
					Debug.DrawLine(inputDownPositionWorld, yVector, Color.blue);

					// get x vector
					Vector3 xVector = new Vector3(touchWorld.x, inputDownPositionWorld.y, 0);
					Debug.DrawLine(inputDownPositionWorld, xVector, Color.cyan);

					// move based on the dragging vector
					if (Mathf.Abs(movedVector.x) > Mathf.Abs(movedVector.y) && movedVector.sqrMagnitude > 20.0) {
						if (xVector.x - inputDownPositionWorld.x > 0)
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
					} 
					else if (Mathf.Abs(movedVector.x) < Mathf.Abs(movedVector.y) && movedVector.sqrMagnitude > 20.0) {
						if (yVector.y - inputDownPositionWorld.y > 0) {
							if (floorScript.CanMoveSouth()) {
								floorScript.MoveSouth();
							}
						}
						else {
							if (floorScript.CanMoveNorth()) {
								floorScript.MoveNorth();
							}
						}
						shouldBeListening = false;
					}
				}
			}
		}
		else {
			shouldBeListening = true;
		}
	}

	/**
	 * A function that handles movement with mouse controls
  	 */
	private void moveMouse() {

		if (Input.GetKey (KeyCode.Mouse0) && shouldBeListening) {
			// log the position where the mouse was first clicked
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

			// move based on the dragging vector
			if (Mathf.Abs(movedVector.x) > Mathf.Abs(movedVector.y) && movedVector.sqrMagnitude > 20.0) {
				if (xVector.x - inputDownPositionWorld.x > 0)
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
			} 
			else if (Mathf.Abs(movedVector.x) < Mathf.Abs(movedVector.y) && movedVector.sqrMagnitude > 20.0) {
				if (yVector.y - inputDownPositionWorld.y > 0) {
					if (floorScript.CanMoveSouth()) {
						floorScript.MoveSouth();
					}
				}
				else {
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
	}
}
