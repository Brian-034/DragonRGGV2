﻿using UnityEditor;


// TODO consider changing to a property drawer
[CustomEditor(typeof(CameraRaycaster))]
public class CameraRaycasterEditor : Editor
{
    bool isLayerPrioritiesUnfolded = true; // store the UI state

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Serialize cameraRaycaster instance

        isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");
        if (isLayerPrioritiesUnfolded)
        {
            EditorGUI.indentLevel++;
            {
                BindArraySize();
                BindArrayElements();
                //PrintString(); 
            }
            EditorGUI.indentLevel--;
            
        }

        serializedObject.ApplyModifiedProperties(); // De-serialize back to cameraRaycaster (and create undo point)
    }

    //for future ref only
    //void PrintString()
    //{
    //    var currentText = serializedObject.FindProperty("stringToPrint");
    //    // string currentText = "StringToPrint";
    //    //string requiredStringToPrint = EditorGUILayout.TextField("StringToPrint: ", currentText);
    //    currentText.stringValue = EditorGUILayout.TextField("StringToPrint: ", currentText.stringValue);

    //}

    void BindArraySize()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
        if (requiredArraySize != currentArraySize)
        {
            serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;
        }
    }

    void BindArrayElements()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        for (int i = 0; i < currentArraySize; i++)
        {
            var prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));
            prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);
        }
    }
}
