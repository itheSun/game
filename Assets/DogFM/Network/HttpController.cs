﻿using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using DogFM;
using UnityEngine;

/// <summary>
/// Http封装
/// </summary>
public class HttpController : Singleton<HttpController>
{
    public void UnityGet(string url, Action<string> successCallback, Action failedCallback)
    {
        UnityWebRequest request = new UnityWebRequest(url, "GET");
        DogFM.GameApp.Instance.StartCoroutine(UnityRequest(request, successCallback, failedCallback));
    }

    public void UnityPost(string url, Action<string> successCallback, Action failedCallback, byte[] data)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        DogFM.GameApp.Instance.StartCoroutine(UnityRequest(request, successCallback, failedCallback, data));
    }

    public void DownloadFile(string url, byte[] fileName, Action<string, byte[]> successCallback, Action failedCallback)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        DogFM.GameApp.Instance.StartCoroutine(Downloading(request, fileName, successCallback, failedCallback));
    }

    IEnumerator Downloading(UnityWebRequest request, byte[] data, Action<string, byte[]> successCallback, Action failedCallback)
    {
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Bug.Warning("http请求失败");
            failedCallback.Invoke();
        }
        else
        {
            Dictionary<string, string> headers = request.GetResponseHeaders();
            string fileName = headers["fileName"];
            successCallback.Invoke(fileName, request.downloadHandler.data);
        }
    }

    IEnumerator UnityRequest(UnityWebRequest request, Action<string> successCallback, Action failedCallback, byte[] data = null)
    {
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Bug.Warning("http请求失败");
            failedCallback.Invoke();
        }
        else
        {
            successCallback.Invoke(request.downloadHandler.text);
        }
    }

    [Obsolete]
    public void UnityWWW(string url, Action<byte[]> callback)
    {
        DogFM.GameApp.Instance.StartCoroutine(WWWRequest(url, (data) => callback.Invoke(data)));
    }

    [Obsolete]
    IEnumerator WWWRequest(string url, Action<byte[]> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone)
        {
            Bug.Log("下载完成");
            byte[] bytes = www.bytes;
            callback.Invoke(bytes);
        }
    }
}