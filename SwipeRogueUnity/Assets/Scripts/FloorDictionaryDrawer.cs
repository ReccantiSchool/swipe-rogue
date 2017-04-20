using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/**
 * Creates a custom drawer for the Rooms Dictonary
 */
[CustomPropertyDrawer(typeof(Vector2GameObjectDictionary))]
public class FloorDictionaryDrawer : SerializableDictionaryDrawer<Vector2, GameObject> {
    protected override SerializableKeyValueTemplate<Vector2, GameObject> GetTemplate() {
        return GetGenericTemplate<SerializableVector2GameObjectTemplate>();
    }

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty(position, label, property);

		// display the name of the property on the first line
		var firstLine = position;
		firstLine.height = EditorGUIUtility.singleLineHeight;
		EditorGUI.PropertyField(firstLine, property);

		// display dropdown properties
		if (property.isExpanded) {

			// define the overall height and dimensions of the second line
			var secondLine = firstLine;
			secondLine.y += EditorGUIUtility.singleLineHeight;

			EditorGUIUtility.labelWidth = 50f;

			// Render a button to create a new Room at Vector2.zero
			var keysProp = GetKeysProp(property);
			if (keysProp.arraySize == 0) {
				var secondLine_createButton = secondLine;

				// Event Handler for Create Button Click
				if (GUI.Button(secondLine_createButton, "Create Start Room")) {
					Debug.Log("test");
				}
			}

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
		}
	}
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        if (!property.isExpanded)
            return EditorGUIUtility.singleLineHeight;
 
        var total = EditorGUIUtility.singleLineHeight;
 
        var kHeight = EditorGUI.GetPropertyHeight(GetTemplateKeyProp(property));
        var vHeight = EditorGUI.GetPropertyHeight(GetTemplateValueProp(property));
        total += Mathf.Max(kHeight, vHeight);
 
        var keysProp = GetKeysProp(property);
        var valuesProp = GetValuesProp(property);
        int numLines = keysProp.arraySize;
        for (int i = 0; i < numLines; i++) {
            kHeight = EditorGUI.GetPropertyHeight(GetIndexedItemProp(keysProp, i));
            vHeight = EditorGUI.GetPropertyHeight(GetIndexedItemProp(valuesProp, i));
            total += Mathf.Max(kHeight, vHeight);
        }
        return total;
    }
 
    private SerializedProperty GetTemplateKeyProp(SerializedProperty mainProp) {
        return GetTemplateProp(templateKeyProp, mainProp);
    }
    private SerializedProperty GetTemplateValueProp(SerializedProperty mainProp) {
        return GetTemplateProp(templateValueProp, mainProp);
    }
 
    private SerializedProperty GetTemplateProp(Dictionary<int, SerializedProperty> source, SerializedProperty mainProp) {
        SerializedProperty p;
        if (!source.TryGetValue(mainProp.GetObjectCode(), out p)) {
            var templateObject = GetTemplate();
            var templateSerializedObject = new SerializedObject(templateObject);
            var kProp = templateSerializedObject.FindProperty("key");
            var vProp = templateSerializedObject.FindProperty("value");
            templateKeyProp[mainProp.GetObjectCode()] = kProp;
            templateValueProp[mainProp.GetObjectCode()] = vProp;
            p = source == templateKeyProp ? kProp : vProp;
        }
        return p;
    }

    private Dictionary<int, SerializedProperty> templateKeyProp = new Dictionary<int, SerializedProperty>();
    private Dictionary<int, SerializedProperty> templateValueProp = new Dictionary<int, SerializedProperty>();
 
    private SerializedProperty GetKeysProp(SerializedProperty mainProp) {
        return GetCachedProp(mainProp, "keys", keysProps); }
    private SerializedProperty GetValuesProp(SerializedProperty mainProp) {
        return GetCachedProp(mainProp, "values", valuesProps); }
 
    private SerializedProperty GetCachedProp(SerializedProperty mainProp, string relativePropertyName, Dictionary<int, SerializedProperty> source) {
        SerializedProperty p;
        int objectCode = mainProp.GetObjectCode();
        if (!source.TryGetValue(objectCode, out p))
            source[objectCode] = p = mainProp.FindPropertyRelative(relativePropertyName);
        return p;
    }
 
    private Dictionary<int, SerializedProperty> keysProps = new Dictionary<int, SerializedProperty>();
    private Dictionary<int, SerializedProperty> valuesProps = new Dictionary<int, SerializedProperty>();
 
    private Dictionary<int, Dictionary<int, SerializedProperty>> indexedPropertyDicts = new Dictionary<int, Dictionary<int, SerializedProperty>>();
 
    private SerializedProperty GetIndexedItemProp(SerializedProperty arrayProp, int index) {
        Dictionary<int, SerializedProperty> d;
        if (!indexedPropertyDicts.TryGetValue(arrayProp.GetObjectCode(), out d))
            indexedPropertyDicts[arrayProp.GetObjectCode()] = d = new Dictionary<int, SerializedProperty>();
        SerializedProperty result;
        if (!d.TryGetValue(index, out result))
            d[index] = result = arrayProp.FindPropertyRelative(string.Format("Array.data[{0}]", index));
        return result;
    }
 
}

internal class SerializableVector2GameObjectTemplate: SerializableKeyValueTemplate<Vector2, GameObject> {}
