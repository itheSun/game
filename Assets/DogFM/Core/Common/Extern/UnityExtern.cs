using System.Diagnostics;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// unity类扩展
/// </summary>
public static class UnityExtern
{
    public static T Find<T>(this Transform tf, string path)
    {
        return tf.Find(path).GetComponent<T>();
    }

    public static T[] Finds<T>(this Transform tf, string path)
    {
        return tf.Find(path).GetComponents<T>();
    }

    public static T Find<T>(this GameObject go, string path)
    {
        return go.transform.Find(path).GetComponent<T>();
    }

    public static T[] Finds<T>(this GameObject go, string path)
    {
        return go.transform.Find(path).GetComponents<T>();
    }
}