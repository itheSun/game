using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

/// <summary>
/// 定时回调任务
/// </summary>
public class TimerTask
{
    // 任务名称
    public string id;
    // 计时持续时间
    private float durationTime;
    // 计时间隔（指定间隔多久触发一次事件）
    private double intervalTime;

    // 计时开始、每帧、结束回调
    public Action onStart;
    public Action onUpdate;
    public Action onEnd;

    public float DurationTime { get => durationTime; }
    public double IntervalTime { get => intervalTime; }

    /// <summary>
    /// 创建一个必须明确周期，每帧执行的计时器，以保证能够自动结束
    /// </summary>
    /// <param name="taskName"></param>
    public TimerTask(float durationTime)
    {
        this.id = "LimitTask";
        this.durationTime = durationTime;
        this.intervalTime = 0;
    }

    public TimerTask(float durationTime, Action callback)
    {
        this.id = "LimitTask";
        this.durationTime = durationTime;
        this.intervalTime = 0;
        this.onEnd += callback;
    }

    /// <summary>
    /// 创建一个无限时间、每帧执行的计时任务
    /// </summary>
    /// <param name="taskName"></param>
    public TimerTask(string taskName)
    {
        this.id = taskName;
        this.durationTime = -1;
        this.intervalTime = 0;
    }

    /// <summary>
    /// 创建一个有限时间、每帧执行的计时任务
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="durationTime"></param>
    public TimerTask(string taskName, float durationTime)
    {
        this.id = taskName;
        this.durationTime = durationTime;
        this.intervalTime = 0;
    }

    /// <summary>
    /// 创建一个无限时间、固定帧执行的计时任务
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="intervalTime"></param>
    public TimerTask(string taskName, double intervalTime)
    {
        this.id = taskName;
        this.durationTime = -1;
        this.intervalTime = intervalTime;
    }

    /// <summary>
    /// 创建一个有限时间、固定帧执行的计时任务
    /// </summary>
    /// <param name="taskName"></param>
    /// <param name="durationTime"></param>
    /// <param name="intervalTime"></param>
    public TimerTask(string taskName, float durationTime, float intervalTime)
    {
        this.id = taskName;
        this.durationTime = durationTime;
        this.intervalTime = intervalTime;
    }
}

