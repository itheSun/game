using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// 路径工具类
/// </summary>
public static class PathUtil
{
    /// <summary>
    /// 基路径
    /// </summary>
    public static readonly string AssetsPath = Application.dataPath;
    public static readonly string StreamingPath = Application.streamingAssetsPath;
    public static readonly string PersistentDataPath = Application.persistentDataPath;

    public static readonly string editorConfigCachePath = StreamingPath + "/Config/EditorConfig.txt";
    public static readonly string dataClassSavePath = AssetsPath + "/Scripts/Data";
    /// <summary>
    /// 打包AssetBundles
    /// </summary>
    public static readonly string AssetBundleRootPath = AssetsPath + "/Resources";
    public static readonly string AssetBundleBuildPath = StreamingPath + "/Hotfix/AssetBundles";
    public static readonly string HotFixABFileSavePath_Editor = StreamingPath + "/Hotfix/AssetBundles/Res";
    public static readonly string HotFixLuaFileSavePath_Editor = StreamingPath + "/Hotfix/Lua";

    /// <summary>
    /// 热更新
    /// </summary>
    public static readonly string VersionFilePath = StreamingPath + "/Hotfix/VersionFile.txt";
    public static readonly string VerifyFilePath = StreamingPath + "/Hotfix/VerifyFile.txt";
    public static readonly string UIConfigPath = StreamingPath + "/Config/UIPath.json";

    /// <summary>
    /// 路径格式化
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Format(string path)
    {
        return path.Replace("\\", "/");
    }

    /// <summary>
    /// 获取文件后缀
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Suffix(string path)
    {
        int index = path.LastIndexOf('.');
        return path.Substring(index + 1);
    }

    /// <summary>
    /// 路径合并
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <returns></returns>
    public static string Combine(string arg1, string arg2)
    {
        return string.Format("{0}/{1}", arg1, arg2);
    }

    /// <summary>
    /// 路径合并
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <param name="arg3"></param>
    /// <returns></returns>
    public static string Combine(string arg1, string arg2, string arg3)
    {
        return string.Format("{0}/{1}/{2}", arg1, arg2, arg3);
    }
}
