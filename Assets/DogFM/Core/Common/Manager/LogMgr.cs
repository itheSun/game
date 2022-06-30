using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DogFM
{
    /// <summary>
    /// 日志系统
    /// </summary>
    public static class Bug
    {
        public static void EditorLog(object log)
        {
            Debug.LogFormat("<color=green>{0}</color>", log);
        }

        public static void EditorLog(string format, params object[] args)
        {
            string log = string.Format(format, args);
            Debug.LogFormat("<color=green>{0}</color>", log);
        }

        public static void Log(object log)
        {
            Debug.LogFormat("<color=green>{0}</color>", log);
        }

        public static void Log(string format, params object[] args)
        {
            string log = string.Format(format, args);
            Debug.LogFormat("<color=green>{0}</color>", log);
        }

        public static void Warning(object log)
        {
            Debug.LogFormat("<color=red>{0}</color>", log);
        }

        public static void Warning(string format, params object[] args)
        {
            string log = string.Format(format, args);
            Debug.LogFormat("<color=red>{0}</color>", log);
        }

        public static void Err(object log)
        {
            Debug.LogFormat("<color=red>{0}</color>", log);
        }

        public static void Err(string format, params object[] args)
        {
            string log = string.Format(format, args);
            Debug.LogFormat("<color=red>{0}</color>", log);
        }

        public static void Throw(string log)
        {
            throw new Exception(log);
        }
        public static void Throw(string format, params object[] args)
        {
            throw new Exception(string.Format(format, args));
        }
    }
}