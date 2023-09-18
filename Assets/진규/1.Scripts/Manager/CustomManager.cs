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
        // Editor�� Ȱ���Ͽ� EnemyDate.csv���� �Է�
        base.OnInspectorGUI();
        if (GUI.Button(new Rect(0, 0, 100, 20), "Load"))
        {
            string path = EditorUtility.OpenFilePanel("EnemyData",Application.streamingAssetsPath,"csv");
            
            if (path.Length != 0)
            {
                DefineDataEnemy define = FindObjectOfType<DefineDataEnemy>();
                define.eDataList.Clear();
                define.DataSet();
            }
            else
                Debug.Log("�����͸� �����ÿ�");
        }
    }
}
