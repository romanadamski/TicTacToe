using System;
using UnityEditor;

[CustomEditor(typeof(GameSettingsSO))]
public class GameSettingsSOInspector : Editor
{
    private SerializedProperty playerTurnTime;
    private SerializedProperty horizontalNodes;
    private SerializedProperty verticalNodes;
    private SerializedProperty winNodes;

    private void OnEnable()
    {
        playerTurnTime = serializedObject.FindProperty("playerTurnTime");
        horizontalNodes = serializedObject.FindProperty("horizontalNodes");
        verticalNodes = serializedObject.FindProperty("verticalNodes");
        winNodes = serializedObject.FindProperty("winNodes");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        playerTurnTime.floatValue = EditorGUILayout.FloatField("Player turn time", playerTurnTime.floatValue);
        horizontalNodes.intValue = EditorGUILayout.IntSlider("Horizontal nodes", horizontalNodes.intValue, 3, 10);
        verticalNodes.intValue = EditorGUILayout.IntSlider("Vertical nodes", verticalNodes.intValue, 3, 10);
        winNodes.intValue = EditorGUILayout.IntSlider("Win nodes", winNodes.intValue, 3, Math.Max(horizontalNodes.intValue, verticalNodes.intValue));

        serializedObject.ApplyModifiedProperties();
    }
}
