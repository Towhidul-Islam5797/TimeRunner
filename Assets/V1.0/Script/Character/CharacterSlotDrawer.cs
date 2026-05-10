using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CharacterSelector.CharacterSlot))]
public class CharacterSlotDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty character = property.FindPropertyRelative("character");
        SerializedProperty startHour = property.FindPropertyRelative("startHour");
        SerializedProperty endHour = property.FindPropertyRelative("endHour");

        string characterName = character.objectReferenceValue != null
            ? character.objectReferenceValue.name
            : "Unassigned";

        string slotLabel = $"{characterName} | {startHour.intValue}:00 - {endHour.intValue}:59";

        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, new GUIContent(slotLabel), true);
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }
}