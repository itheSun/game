﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;

namespace DogFM
{
    /// <summary>
    /// 打包AB工具类
    /// </summary>
    public class AssetBundleTool : Editor
    {
        /// <summary>
        /// 菜单栏打包AssetBundle选项
        /// </summary>
        [MenuItem("DogFM/HotFix/BuildAssetBundles", false, 3)]
        public static void BuildAssetbundles()
        {
            string rootPath = PathUtil.AssetBundleRootPath;
            string buildPath = PathUtil.AssetBundleBuildPath;
            string versionFilePath = PathUtil.VersionFilePath;
            string verifyFilePath = PathUtil.VerifyFilePath;
            // 检查是否存在需要打包AB的路径
            if (!Directory.Exists(rootPath))
            {
                Bug.Err("不存在AssetBundle打包路径{0}", rootPath);
                return;
            }
            // 更新版本文件
            using (FileStream versionFile = new FileStream(versionFilePath, FileMode.Create))
            {
                versionFile.SetLength(0);
                versionFile.Position = 0;
                byte[] buffer = Encoding.UTF8.GetBytes(Constant.NextVersion);
                versionFile.Write(buffer, 0, buffer.Length);
                Bug.Log("Build version file success");
            }
            // 清空旧的校验文件
            using (FileStream verifyFile = new FileStream(verifyFilePath, FileMode.Create))
            {
                verifyFile.SetLength(0);
                verifyFile.Position = 0;
            }
            ClearOldAssetBundles();
            Pack(rootPath);
            BuildAssetBundles(buildPath, BuildTarget.StandaloneWindows);
            Bug.Log("Build AssetBundle success");
            BuildVerifyFile(buildPath);
            Bug.Log("Build verify file success");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 清除之前保留的AB名
        /// </summary>
        private static void ClearOldAssetBundles()
        {
            string[] assetBundles = AssetDatabase.GetAllAssetBundleNames();
            foreach (string assetBundle in assetBundles)
            {
                AssetDatabase.RemoveAssetBundleName(assetBundle, true);
            }
        }

        /// <summary>
        /// 递归遍历打包AB
        /// </summary>
        /// <param name="path"></param>
        private static void Pack(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] is DirectoryInfo)
                {
                    Pack(files[i].FullName);
                }
                else
                {
                    if (!files[i].FullName.EndsWith(".meta"))
                    {
                        SetName(files[i].FullName);
                    }
                }
            }
        }

        /// <summary>
        /// 设置AB名称：文件路径名
        /// </summary>
        /// <param name="file"></param>
        private static void SetName(string file)
        {
            string path = PathUtil.Format(file);
            string assetPath = "Assets" + path.Substring(PathUtil.AssetsPath.Length);
            AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);

            string namePath = path.Substring(PathUtil.AssetsPath.Length + 1);
            namePath = namePath.Substring(namePath.IndexOf("/") + 1);
            string name = namePath.Replace(PathUtil.Suffix(namePath), "unity3d");
            assetImporter.assetBundleName = name;
        }

        /// <summary>
        /// 打包AB
        /// </summary>
        /// <param name="buildPath"></param>
        /// <param name="target"></param>
        public static void BuildAssetBundles(string buildPath, BuildTarget target)
        {
            if (Directory.Exists(buildPath))
            {
                Directory.Delete(buildPath, true);
            }
            Directory.CreateDirectory(buildPath);
            BuildPipeline.BuildAssetBundles(buildPath, BuildAssetBundleOptions.None, target);
        }

        public static void BuildVerifyFile(string path)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] is DirectoryInfo)
                {
                    BuildVerifyFile(files[i].FullName);
                }
                else
                {
                    if (!files[i].FullName.EndsWith(".meta"))
                    {
                        string fullName = PathUtil.Format(files[i].FullName);
                        string assetPath = fullName.Substring(PathUtil.AssetsPath.Length + 1);
                        assetPath = assetPath.Substring(assetPath.IndexOf('/') + 1);
                        string md5 = MD5Helper.Encode(fullName);
                        using (FileStream verifyFile = new FileStream(PathUtil.VerifyFilePath, FileMode.OpenOrCreate))
                        {
                            byte[] bytes = new byte[verifyFile.Length];
                            verifyFile.Read(bytes, 0, bytes.Length);
                            string Log = string.Format("{0}|{1}\n", assetPath, md5);
                            byte[] buffer = Encoding.UTF8.GetBytes(Log);
                            verifyFile.Position = bytes.Length;
                            verifyFile.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
        }
    }
}
