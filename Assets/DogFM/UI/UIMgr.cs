using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DogFM;
using System;
using DogFM.MVVM;

public enum UILayer
{
    Common,
}

public enum SceneID
{

}

/// <summary>
/// UI管理类
/// 面板层级管理
/// 进出UI栈显示隐藏面板
/// </summary>
//[RequireComponent(typeof(Canvas))]
//[RequireComponent(typeof(CanvasScaler))]
//[RequireComponent(typeof(GraphicRaycaster))]
public class UIMgr : DntdMonoSingleton<UIMgr>, IObservable<IView>
{
    /// <summary>
    /// 各层根节点
    /// </summary>
    private Dictionary<UILayer, Transform> layerMap = new Dictionary<UILayer, Transform>();

    /// <summary>
    /// 面板路径
    /// </summary>
    private Dictionary<string, string> pathMap = new Dictionary<string, string>();

    // 所有面板的脚本
    private Dictionary<string, IView> viewMap = new Dictionary<string, IView>();

    // ui栈
    private Stack<IView> viewStack = new Stack<IView>();

    public void Init()
    {
        //GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

        //GameObject commonLayer = new GameObject("Common Layer");
        //commonLayer.transform.SetParent(this.transform);
        //commonLayer.transform.localPosition = Vector3.zero;
        //RectTransform rectTransform = commonLayer.AddComponent<RectTransform>();
        //rectTransform.anchorMin = Vector2.zero;
        //rectTransform.anchorMax = Vector2.one;
        //rectTransform.offsetMin = new Vector2(0.0f, 0.0f);
        //rectTransform.offsetMax = new Vector2(0.0f, 0.0f);
        Transform commonLayer = transform.Find<Transform>("Common Layer");
        layerMap.Add(UILayer.Common, commonLayer);


        // 解析json配置文件
        //TableParser tableParser = new TableParser(new JsonTableParser());
        //pathMap = tableParser.Parse<Dictionary<string, string>>(PathUtil.UIConfigPath);
    }

    /// <summary>
    /// 面板索引器
    /// </summary>
    /// <value></value>
    public T GetView<T>(string panelID) where T : IView
    {
        if (!viewMap.ContainsKey(panelID))
        {
            Bug.Throw(string.Format("The panel {0} does not exist", panelID));
        }
        return (T)viewMap[panelID];
    }

    /// <summary>
    /// 面板进栈
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="panelID"></param>
    /// <param name="hide"></param>
    /// <param name="diasble"></param>
    /// <returns></returns>
    public T Push<T>(UILayer layer, string panelID, bool hide = true, bool disasble = true) where T : IView, new()
    {
        if (viewStack == null)
            viewStack = new Stack<IView>();

        if (!viewMap.ContainsKey(panelID))
        {
            GameObject go = ResMgr.Instance.Load<GameObject>(pathMap[panelID]);
            GameObject panel = ResMgr.Instance.Instantiate<GameObject>(go);
            viewMap.Add(panelID, panel.GetComponent<T>());
            panel.transform.SetParent(layerMap[layer]);
            panel.transform.localPosition = Vector3.zero;
            RectTransform rectTransform = panel.GetComponent<RectTransform>();
            rectTransform.offsetMin = new Vector2(0.0f, 0.0f);
            rectTransform.offsetMax = new Vector2(0.0f, 0.0f);
        }

        if (viewStack.Count > 0)
        {
            IView top = viewStack.Peek();
            if (hide)
                top.OnHide();
            if (disasble)
                top.OnBlock();
        }

        T target = (T)viewMap[panelID];
        viewStack.Push(target);
        target.OnShow();
        return target;
    }

    /// <summary>
    /// 面板出栈
    /// </summary>
    public void Pop()
    {
        if (viewStack == null)
        {
            viewStack = new Stack<IView>();
        }

        if (viewStack.Count <= 0)
        {
            return;
        }

        IView panel = viewStack.Pop();
        panel.OnHide();

        if (viewStack.Count > 0)
        {
            IView top = viewStack.Peek();
            top.OnActive();
        }
    }

    public IDisposable Subscribe(IObserver<IView> observer)
    {
        throw new NotImplementedException();
    }
}
