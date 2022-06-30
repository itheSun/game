using UnityEngine;

namespace DogFM
{
    public static class ChainExtern
    {
        /// <summary>
        /// Transfrom 链式扩展
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="pos"></param>
        /// <returns></returns>

        public static Transform Position(this Transform tf, Vector3 pos)
        {
            tf.position = pos;
            return tf;
        }

        public static Transform LocalPosition(this Transform tf, Vector3 pos)
        {
            tf.localPosition = pos;
            return tf;
        }
        public static Transform Roration(this Transform tf, Quaternion rotation)
        {
            tf.rotation = rotation;
            return tf;
        }

        public static Transform LocalRotation(this Transform tf, Quaternion rotation)
        {
            tf.localRotation = rotation;
            return tf;
        }

        public static Transform LocalScale(this Transform tf, Vector3 scale)
        {
            tf.localScale = scale;
            return tf;
        }

        /// <summary>
        /// RectTrasnform链式
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static RectTransform Position(this RectTransform tf, Vector3 pos)
        {
            tf.position = pos;
            return tf;
        }

        public static RectTransform LoaclPosition(this RectTransform tf, Vector3 pos)
        {
            tf.localPosition = pos;
            return tf;
        }

        public static RectTransform Roration(this RectTransform tf, Quaternion rotation)
        {
            tf.rotation = rotation;
            return tf;
        }

        public static RectTransform LocalRotation(this RectTransform tf, Quaternion rotation)
        {
            tf.localRotation = rotation;
            return tf;
        }

        public static RectTransform LocalScale(this RectTransform tf, Vector3 scale)
        {
            tf.localScale = scale;
            return tf;
        }
    }
}