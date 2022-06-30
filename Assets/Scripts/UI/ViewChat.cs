using DogFM;
using DogFM.MVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewChat : BaseView<ViewModelChat>
{
    private Transform sessionRoot;
    private Text worldRecordTxt;
    private InputField messageInput;
    private Button sendBtn;

    private void Awake()
    {
        sessionRoot = transform.Find<Transform>("SessionList/Viewport/Content");
        worldRecordTxt = sessionRoot.Find<Text>("WorldRecordTxt");
        messageInput = transform.Find<InputField>("MessageInput");
        sendBtn = transform.Find<Button>("SendBtn");
        sendBtn.onClick.AddListener(OnSendMessage);

        binder.Add<string>("worldRecord", OnRecordChanged);
    }

    private void OnRecordChanged(string old, string value)
    {
        worldRecordTxt.text += value;
    }

    private void OnSendMessage()
    {
        string message = messageInput.text;
        if (string.IsNullOrEmpty(message))
        {
            return;
        }
        NetworkMgr.Instance.OnSendChatMessage(message);
    }
}
