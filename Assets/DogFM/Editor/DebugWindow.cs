using DogFM;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DebugWindow : EditorWindow
{
    [MenuItem("DogFM/Debug", false, 1)]
    private static void AddWindow()
    {
        DebugWindow myWindow = (DebugWindow)EditorWindow.GetWindow(typeof(DebugWindow), false, "Debug", true);//创建窗口
        myWindow.Show();
    }

    public static string dataTablePath = "";
    private void Reset()
    {
        // 读取编辑器配置缓存文件
        if (!File.Exists(PathUtil.dataClassSavePath))
        {
            File.Create(PathUtil.dataClassSavePath);
        }
        using (StreamReader sr = new StreamReader(PathUtil.dataClassSavePath))
        {
            string line = sr.ReadLine();
            dataTablePath = line;
        }
    }

    private float minTimeScale = 10;
    private float maxTimeScale = 0.1f;
    private float timeScale = 1.0f;

    private void OnGUI()
    {
        timeScale = EditorGUILayout.Slider("TimeScale", timeScale, minTimeScale, maxTimeScale);
        Time.timeScale = timeScale;

        string _dataTablePath = EditorGUILayout.TextField("数据表", dataTablePath);
        _dataTablePath = PathUtil.Format(_dataTablePath);
        if (!Directory.Exists(_dataTablePath))
        {
            Debug.LogFormat("Directory {0} is not exist!", _dataTablePath);
        }
        else
        {
            dataTablePath = _dataTablePath;
            using (StreamWriter writer = new StreamWriter(PathUtil.dataClassSavePath))
            {
                writer.BaseStream.Position = 0;
                writer.WriteLine(_dataTablePath);
            }
        }
    }
}
