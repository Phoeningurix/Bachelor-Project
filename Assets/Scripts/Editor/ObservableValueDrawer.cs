using UnityEditor;
using UnityEngine;
using Utils;
using Utils.Observables;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ObservableValue<>))]
    public class ObservableValueDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProp = property.FindPropertyRelative("value");
            EditorGUI.PropertyField(position, valueProp, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProp = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProp, label, true);
        }
    }
}