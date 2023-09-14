using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor (typeof(EnemyTest))]
public class CustomManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.Button(new Rect(0, 0, 100, 20), "Load"))
        {
            EditorUtility.OpenFilePanel("","","");
        }
    }
}
