using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DogFM
{
    /// <summary>
    /// 定时回调
    /// </summary>
    public class Timer : DntdMonoSingleton<Timer>
    {
        // 计时协程字典
        private Dictionary<TimerTask, Coroutine> taskMap = new Dictionary<TimerTask, Coroutine>();

        /// <summary>
        /// 获取计时任务
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public TimerTask GetTask(string taskName)
        {
            foreach (var task in taskMap.Keys)
            {
                if (task.id == taskName)
                {
                    return task;
                }
            }
            return null;
        }

        /// <summary>
        /// 开启一个新的计时器
        /// </summary>
        /// <param name="timerTask"></param>
        /// <returns></returns>
        public TimerTask New(TimerTask timerTask)
        {
            taskMap[timerTask] = GameApp.Instance.StartCoroutine(Timing(timerTask));
            return timerTask;
        }

        /// <summary>
        /// 开启一个新的计时器
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="timerTask"></param>
        /// <returns></returns>
        public TimerTask New(float delayTime, TimerTask timerTask)
        {
            GameApp.Instance.StartCoroutine(ReadyToTiming(delayTime, timerTask));
            return timerTask;
        }

        /// <summary>
        /// 准备开始计时（延迟计时）
        /// </summary>
        /// <param name="delayTime"></param>
        /// <param name="timerTask"></param>
        /// <returns></returns>
        IEnumerator ReadyToTiming(float delayTime, TimerTask timerTask)
        {
            yield return new WaitForSeconds(delayTime);
            taskMap[timerTask] = GameApp.Instance.StartCoroutine(Timing(timerTask));
        }

        /// <summary>
        /// 计时ing
        /// </summary>
        /// <param name="timerTask"></param>
        /// <returns></returns>
        IEnumerator Timing(TimerTask timerTask)
        {
            float totleTime = 0;
            float pieceTime = 0;
            if (timerTask.onStart != null) timerTask.onStart.Invoke();

            while (timerTask.DurationTime < 0 ? true : totleTime < timerTask.DurationTime)
            {
                totleTime += Time.deltaTime;
                pieceTime += Time.deltaTime;
                if (timerTask.IntervalTime >= 0 && pieceTime >= timerTask.IntervalTime)
                {
                    if (timerTask.onUpdate != null)
                    {
                        timerTask.onUpdate.Invoke();
                    }
                    pieceTime = 0f;
                }

                yield return null;
            }
            if (timerTask.onEnd != null) timerTask.onEnd.Invoke();

            taskMap.Remove(timerTask);
        }

        /// <summary>
        /// 关闭存在的计时器
        /// </summary>
        /// <param name="timerTask"></param>
        public void StopTimer(TimerTask timerTask)
        {
            if (timerTask == null) return;
            if (taskMap.ContainsKey(timerTask))
            {
                GameApp.Instance.StopCoroutine(taskMap[timerTask]);
                taskMap.Remove(timerTask);
            }
        }

        /// <summary>
        /// 关闭存在的计时器
        /// </summary>
        /// <param name="timerTask"></param>
        public void StopAllTimer()
        {
            foreach (var task in taskMap)
            {
                GameApp.Instance.StopCoroutine(task.Value);
                taskMap.Remove(task.Key);
            }
        }
    }

}