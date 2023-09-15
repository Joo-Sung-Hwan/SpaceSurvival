using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor (typeof(DefineDataEnemy))]
public class CustomManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.Button(new Rect(0, 0, 100, 20), "Load"))
        {
            string path = EditorUtility.OpenFilePanel("EnemyData",Application.streamingAssetsPath,"csv");
            if (path.Length != 0)
            {
                DefineDataEnemy.Instance.DataInfo();
                //DefineDataEnemy.Instance.text = Resources.Load(path) as TextAsset;
            }
            else
                Debug.Log("데이터를 넣으시오");
        }
    }
}
