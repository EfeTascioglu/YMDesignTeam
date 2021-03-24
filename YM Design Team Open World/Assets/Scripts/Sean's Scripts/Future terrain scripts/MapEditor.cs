using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor (typeof(GenerateMap))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GenerateMap gm = (GenerateMap)target;
        if (EditorGUILayout.Toggle(false))
        {
            gm.GenerateNewMap();
        }
        

    }
}
