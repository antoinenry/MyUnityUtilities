using UnityEditor;
using UnityEngine;

// A property drawer similar to Vector2 drawer
public abstract class SegmentPropertyDrawer : PropertyDrawer
{
    readonly private float smallLabelWidth = 15f;
    readonly private float smallFieldSpacing = 10f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw label
        EditorGUI.LabelField(position, label);
        // Remaining space
        float standardLabelWidth = EditorGUIUtility.labelWidth;
        float fieldWidth = position.width - standardLabelWidth;
        float fieldStart = position.x + standardLabelWidth;
        // Special field for when Nan is explicit (Segment<T> can be Nan without T being a Nan-able type)
        SerializedProperty isNanProperty = property.FindPropertyRelative("isNan");
        if (isNanProperty != null && isNanProperty.propertyType == SerializedPropertyType.Boolean && isNanProperty.boolValue == true)
        {
            Rect isNanGUIPosition = position;
            isNanGUIPosition.x = fieldStart;
            IsNanGUI(isNanGUIPosition, isNanProperty);
        }
        else
        {
            // Temporarly change label width for smaller fields
            EditorGUIUtility.labelWidth = smallLabelWidth;
            // Divide remaining width for two number fields
            Rect smallFieldPosition = position;
            smallFieldPosition.x = fieldStart;
            smallFieldPosition.width = fieldWidth / 2f;
            // Draw first small field
            SmallFieldGUI(smallFieldPosition, property, "a", "A");
            // Move position and draw second small field
            smallFieldPosition.x += (fieldWidth + smallFieldSpacing) / 2f;
            SmallFieldGUI(smallFieldPosition, property, "b", "B");

            // Restore label width
            EditorGUIUtility.labelWidth = standardLabelWidth;
        }        
    }

    protected virtual void IsNanGUI(Rect position, SerializedProperty isNanProperty)
    {
        isNanProperty.boolValue = EditorGUI.Toggle(position, "Is Nan", isNanProperty.boolValue);
    }

    protected abstract void SmallFieldGUI(Rect position, SerializedProperty property, string relativePropertyName, string label);
}

[CustomPropertyDrawer(typeof(SegmentInt))]
public class SegmentIntPropertyDrawer : SegmentPropertyDrawer
{
    protected override void SmallFieldGUI(Rect position, SerializedProperty property, string relativePropertyName, string label)
    {
        SerializedProperty relativeProperty = property.FindPropertyRelative(relativePropertyName);
        bool isNan = property.FindPropertyRelative("isNan").boolValue;
        relativeProperty.intValue = EditorGUI.IntField(position, label, relativeProperty.intValue, EditorStyles.numberField);
    }
}

[CustomPropertyDrawer(typeof(SegmentFloat))]
public class SegmentFloatPropertyDrawer : SegmentPropertyDrawer
{
    protected override void SmallFieldGUI(Rect position, SerializedProperty property, string relativePropertyName, string label)
    {
        SerializedProperty relativeProperty = property.FindPropertyRelative(relativePropertyName);
        relativeProperty.floatValue = EditorGUI.FloatField(position, label, relativeProperty.floatValue);
    }
}
