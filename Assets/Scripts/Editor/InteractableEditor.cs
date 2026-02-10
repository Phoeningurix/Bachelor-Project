using Interactions;
using UnityEditor;

namespace Editor
{
    // Dieser Editor wurde mit Gemini erstellt
    // Prompt: "My Interactable has two public variables: consumable and usesLeft.
    // i want a custom editor script which hides usesLeft in the inspector if consumable is false"
    [CustomEditor(typeof(Interactable), true)] 
    public class InteractableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // 1. Update the serialized object's representation
            serializedObject.Update();

            // 2. Find our properties
            SerializedProperty consumableProp = serializedObject.FindProperty("consumable");
            SerializedProperty usesLeftProp = serializedObject.FindProperty("usesLeft");
            SerializedProperty interactionRadius = serializedObject.FindProperty("interactionRadius");

            // 3. Draw the 'consumable' toggle first
            EditorGUILayout.PropertyField(consumableProp);

            // 4. Conditional logic: Only show 'usesLeft' if consumable is true
            if (consumableProp.boolValue)
            {
                // Indent it slightly to show it's a child-setting of 'consumable'
                EditorGUILayout.PropertyField(usesLeftProp);
            }
            
            EditorGUILayout.PropertyField(interactionRadius);

            // 5. Apply the changes back to the actual script
            serializedObject.ApplyModifiedProperties();
        }
    }
}