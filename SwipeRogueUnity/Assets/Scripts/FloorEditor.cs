using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Floor))]
public class FloorEditor : Editor {

	SerializedProperty rooms;

	void OnEnable() {
		// rooms = serializedObject.FindProperty("roomsStore");
		// Debug.Log(rooms);
	}
}
