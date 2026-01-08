using UnityEditor;
using UnityEngine;
using Utils.Observables;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ObservableFloatRegistry))]
    public class ObservableFloatRegistryDrawer : PropertyDrawer
    {
        private const float Padding = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(
                new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
                property.isExpanded,
                label,
                true
            );

            if (!property.isExpanded)
                return;

            EditorGUI.indentLevel++;

            SerializedProperty entriesProp = property.FindPropertyRelative("entries");
            float y = position.y + EditorGUIUtility.singleLineHeight + Padding;

            for (int i = 0; i < entriesProp.arraySize; i++)
            {
                SerializedProperty entry = entriesProp.GetArrayElementAtIndex(i);
                SerializedProperty keyProp = entry.FindPropertyRelative("key");
                SerializedProperty valueProp = entry.FindPropertyRelative("value");

                float buttonWidth = 20f;

                // Draw key
                Rect keyRect = new Rect(position.x, y, position.width * 0.3f - buttonWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(keyRect, keyProp, GUIContent.none);

                // Draw value
                Rect valueRect = new Rect(position.x + position.width * 0.3f, y, position.width * 0.6f - buttonWidth, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);

                // Remove button
                Rect removeRect = new Rect(position.x + position.width - buttonWidth, y, buttonWidth, EditorGUIUtility.singleLineHeight);
                if (GUI.Button(removeRect, "-"))
                {
                    entriesProp.DeleteArrayElementAtIndex(i);
                    break; // because array changed
                }

                y += EditorGUIUtility.singleLineHeight + Padding;
            }

            // Add button at the bottom
            Rect addRect = new Rect(position.x, y, position.width, EditorGUIUtility.singleLineHeight);
            if (GUI.Button(addRect, "+ Add Entry"))
            {
                entriesProp.arraySize++;
                SerializedProperty newEntry = entriesProp.GetArrayElementAtIndex(entriesProp.arraySize - 1);
                newEntry.FindPropertyRelative("key").stringValue = "NewKey";
                // Create new ObservableValue<float>
                SerializedProperty newValue = newEntry.FindPropertyRelative("value");
                newValue.FindPropertyRelative("value").floatValue = 0f;
            }

            EditorGUI.indentLevel--;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
                return EditorGUIUtility.singleLineHeight;

            SerializedProperty entriesProp = property.FindPropertyRelative("entries");
            int count = entriesProp.arraySize;

            // +1 for the "Add Entry" button
            return (EditorGUIUtility.singleLineHeight + Padding) * (count + 1) + EditorGUIUtility.singleLineHeight;
        }
    }
}
