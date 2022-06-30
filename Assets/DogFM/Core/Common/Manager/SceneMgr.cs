using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DogFM
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public class SceneMgr : DntdMonoSingleton<SceneMgr>
    {
        private static SceneID last;
        private static SceneID next;

        private float progress = 0;

        public float Progress { get => progress; }

        public void AsyncLoadScene(SceneID last, SceneID next)
        {
            if (SceneManager.GetSceneByName(last.ToString()) == null || SceneManager.GetSceneByName(next.ToString()) == null)
            {
                Bug.Err("请检查场景ID{0} 或 {1} 是否正确", last, next);
                return;
            }
            SceneMgr.last = last;
            SceneMgr.next = next;
            GameApp.Instance.StartCoroutine(AsyncLoading());
        }

        private float toProgress = 0;
        IEnumerator AsyncLoading()
        {
            /// <summary>
            /// 跳转中间场景
            /// </summary>
            /// <returns></returns> 
            AsyncOperation loading = SceneManager.LoadSceneAsync(Constant.SceneID_Loading);
            yield return loading;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(next.ToString());
            nextScene.allowSceneActivation = false;
            while (nextScene.progress < 0.9f)
            {
                toProgress = (int)(nextScene.progress * 100);
                while (progress < toProgress)
                {
                    progress++;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForEndOfFrame();
            }
            toProgress = 100;
            while (progress < toProgress)
            {
                progress++;
                yield return new WaitForEndOfFrame();
            }
            nextScene.allowSceneActivation = true;
        }
    }
}
