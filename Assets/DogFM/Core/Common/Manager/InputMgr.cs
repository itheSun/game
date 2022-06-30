using System;
using UnityEngine;

namespace DogFM
{
    /// <summary>
    /// 输入管理器
    /// </summary>
    public static class InputMgr
    {
        public static void Init()
        {
            /// <summary>
            /// 读取键位配置文件
            /// </summary>
            /// <value></value>
        }

        public static float Forward
        {
            get
            {
                return Input.GetAxis(Constant.Input_Vertical);
            }
        }

        public static float Right
        {
            get
            {
                return Input.GetAxis(Constant.Input_Horizontal);
            }
        }

        public static bool Jump
        {
            get
            {
                return Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), Constant.Input_Jump));
            }
        }

        public static float MouseX
        {
            get
            {
                return Input.GetAxis(Constant.Input_Mouse_X);
            }
        }

        public static float MouseY
        {
            get
            {
                return Input.GetAxis(Constant.Input_Mouse_Y);
            }
        }

        public static bool MouseLeft
        {
            get
            {
                return Input.GetMouseButton(Constant.Input_Mouse_Left);
            }
        }

        public static bool MouseRight
        {
            get
            {
                return Input.GetMouseButtonDown(Constant.Input_Mouse_Right);
            }
        }

        public static bool MouseLeftDown
        {
            get
            {
                return Input.GetMouseButtonDown(Constant.Input_Mouse_Left);
            }
        }

        public static bool MouseRightDown
        {
            get
            {
                return Input.GetMouseButtonDown(Constant.Input_Mouse_Right);
            }
        }
    }
}