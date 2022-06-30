//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using XLua;
//using UnityEngine.SceneManagement;
//using DogFM;
//using System;

///// <summary>
///// 热更流程
///// </summary>
//public class HotfixController : Singleton<HotfixController>
//{
//    private LuaEnv luaEnv;

//    public void LoadHotFix(Action callback)
//    {
//        if (luaEnv == null)
//            luaEnv = new LuaEnv();
//        string path = PathUtil.HotFixLuaFileSavePath_Editor;
//        if (!Directory.Exists(path))
//        {
//            Bug.Err("不存在热更文件夹{0}", path);
//            return;
//        }
//        string[] luaFiles = Directory.GetFiles(path);
//        int filesCount = luaFiles.Length;
//        for (int i = 0; i < filesCount; i++)
//        {
//            using (StreamReader reader = new StreamReader(luaFiles[i]))
//            {
//                string buff = reader.ReadToEnd();
//                Bug.Log(buff);
//                luaEnv.DoString(buff);
//            }
//        }
//        luaEnv.Dispose();

//        if (callback != null)
//            callback.Invoke();
//    }
//}
