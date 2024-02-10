using UnityEditor;
using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Helpers.ReadOnlyAttributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyValueAttribute))]
    public class ReadOnlyValueDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var previousGUIState = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
