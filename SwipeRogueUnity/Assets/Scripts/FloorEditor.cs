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

	public override void OnInspectorGUI () {
		if (GUI.Button(new Rect(0, 0, 100, 100), "TESTING")) {

		}
		// EditorGUI.BeginProperty(position, label, property);

		// // display the name of the property on the first line
		// var firstLine = position;
		// firstLine.height = EditorGUIUtility.singleLineHeight;
		// EditorGUI.PropertyField(firstLine, property);

		// // display dropdown properties
		// if (property.isExpanded) {

		// 	// define the overall height and dimensions of the second line
		// 	var secondLine = firstLine;
		// 	secondLine.y += EditorGUIUtility.singleLineHeight;

		// 	EditorGUIUtility.labelWidth = 50f;

		// 	// Render a button to create a new Room at Vector2.zero
		// 	// var keysProp = GetKeysProp(property);
		// 	// if (keysProp.arraySize == 0) {
		// 		var secondLine_createButton = secondLine;

		// 		// Event Handler for Create Button Click
		// 		if (GUI.Button(secondLine_createButton, "Create Start Room")) {
		// 			Debug.Log("test");
		// 		}
			// }

			// secondLine.x += 15f; // indentation
			// secondLine.width -= 15f;

			// define the individual components for the second line
			// var secondLine_key = secondLine;

			// var buttonWidth = 60f;
			// secondLine_key.width -= buttonWidth; // assign button
			// secondLine_key.width /= 2f;

			// var secondLine_value = secondLine_key;
			// secondLine_value.x += secondLine_value.width;
			// if (GetTemplateValueProp(property).hasVisibleChildren) {
			// 	secondLine_value.x += 15;
			// 	secondLine_value.width -= 15;
			// }

			// var secondLine_button = secondLine_value;
			// secondLine_button.x += secondLine_value.width;
			// secondLine_button.width = buttonWidth;

            // var kHeight = EditorGUI.GetPropertyHeight(GetTemplateKeyProp(property));
            // var vHeight = EditorGUI.GetPropertyHeight(GetTemplateValueProp(property));
            // var extraHeight = Mathf.Max(kHeight, vHeight);

			// secondLine_key.height = kHeight;
			// secondLine_value.height = vHeight;

			// // Set the fields for the second line
			// EditorGUI.PropertyField(secondLine_key, GetTemplateKeyProp(property), true);
			// EditorGUI.PropertyField(secondLine_value, GetTemplateValueProp(property), true);

			// var keysProp = GetKeysProp(property);
			// var valuesProp = GetValuesProp(property);

			// var numLines = keysProp.arraySize;

			// if (GUI.Button(secondLine_button, "Assign")) {
			// 	bool assignment = false;
			// 	for (int i = 0; i < numLines; i++) { // Try to replace existing value
			// 		if (SerializedPropertyExtension.EqualBasics(GetIndexedItemProp(keysProp, i), GetTemplateKeyProp(property))) {
			// 			SerializedPropertyExtension.CopyBasics(GetTemplateValueProp(property), GetIndexedItemProp(valuesProp, i));
			// 			assignment = true;
			// 			break;
			// 		}
			// 		if (!assignment) { // create a new value
			// 			keysProp.arraySize += 1;
			// 			valuesProp.arraySize += 1;
			// 			SerializedPropertyExtension.CopyBasics(GetTemplateKeyProp(property), GetIndexedItemProp(keysProp, numLines));
            //         	SerializedPropertyExtension.CopyBasics(GetTemplateValueProp(property), GetIndexedItemProp(valuesProp, numLines));
			// 		}
			// 	}
			// }
		// }
	}

}
