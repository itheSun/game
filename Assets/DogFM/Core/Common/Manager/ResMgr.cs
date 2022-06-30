using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogFM
{
    public class ResMgr : Singleton<ResMgr>
    {
        public T Load<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }

        public T[] LoadAll<T>(string path) where T : UnityEngine.Object
        {
            return Resources.LoadAll<T>(path);
        }

        public T Instantiate<T>(T pref) where T : UnityEngine.Object
        {
            T go = GameObject.Instantiate(pref) as T;
            return go;
        }

        public T Instantiate<T>(string path) where T : UnityEngine.Object
        {
            T pref = Resources.Load<T>(path);
            if (pref is GameObject)
            {
                T go = GameObject.Instantiate<T>(pref);

                return go;
            }
            else
            {
                return pref;
            }
        }

        public void LoadAsync<T>(string prefPath, Action<T> callback) where T : UnityEngine.Object
        {
            GameApp.Instance.StartCoroutine(IE_AsyncInstantiate<T>(prefPath, callback));
        }

        private IEnumerator IE_AsyncInstantiate<T>(string prefPath, Action<T> callback) where T : UnityEngine.Object
        {
            ResourceRequest pref = Resources.LoadAsync<T>(prefPath);
            yield return pref;
            if (pref.asset is GameObject)
            {
                T go = Instantiate(pref.asset) as T;
                callback.Invoke(go);
            }
            else
            {
                callback.Invoke(pref.asset as T);
            }
        }

        public T LoadAndGet<T>(string path) where T : UnityEngine.Object
        {
            GameObject go = Instantiate<GameObject>(path);
            T comp = go.GetComponent<T>();
            return comp;
        }

        public T LoadAndGet<T>(GameObject pref) where T : UnityEngine.Object
        {
            GameObject go = Instantiate(pref);
            T comp = go.GetComponent<T>();
            return comp;
        }

        public void Destroy(GameObject go)
        {
            Destroy(go);
        }

        public void Destroy(GameObject go, float delay)
        {
            Destroy(go, delay);
        }
    }
}