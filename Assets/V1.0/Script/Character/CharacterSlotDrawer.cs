#region Summary
/// <summary>
/// This script is a custom property drawer for the CharacterSelector.CharacterSlot class. It customizes
/// the way the CharacterSlot properties are displayed in the Unity Inspector. The drawer shows the character's name along with the start and end hours in a single line, making it easier to read and manage character slots.
/// Details:
/// - The drawer retrieves the character, startHour, and endHour properties from the CharacterSlot
/// - It constructs a label that combines the character's name and the time range.
/// Usage:
/// 1. Attach this script to the Unity project.
/// 2. Ensure that the CharacterSelector.CharacterSlot class is defined and has the properties "
/// character", "startHour", and "endHour".
/// Note : This script uses UnityEditor and is intended for use within the Unity Editor only. It will not be included in builds.
/// </summary>
#endregion
#region
//using UnityEditor;
//using UnityEngine;

//[CustomPropertyDrawer(typeof(CharacterSelector.CharacterSlot))]
//public class CharacterSlotDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        SerializedProperty character = property.FindPropertyRelative("character");
//        SerializedProperty startHour = property.FindPropertyRelative("startHour");
//        SerializedProperty endHour = property.FindPropertyRelative("endHour");

//        string characterName = character.objectReferenceValue != null
//            ? character.objectReferenceValue.name
//            : "Unassigned";

//        string slotLabel = $"{characterName} | {startHour.intValue}:00 - {endHour.intValue}:59";

//        EditorGUI.BeginProperty(position, label, property);
//        EditorGUI.PropertyField(position, property, new GUIContent(slotLabel), true);
//        EditorGUI.EndProperty();
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return EditorGUI.GetPropertyHeight(property, true);
//    }
//}
#endregion