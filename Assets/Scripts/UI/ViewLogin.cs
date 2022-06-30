using DogFM;
using DogFM.MVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewLogin : BaseView<ViewModelLogin>
{
    private InputField userName;
    private InputField password;
    private Button loginButton;

    private void Awake()
    {
        binder.Add<string>("userName", OnSetUserName);
        binder.Add<string>("password", OnSetPassword);

        loginButton.onClick.AddListener(OnClickLoginBtn);
    }

    private void OnSetPassword(string old, string value)
    {
        this.userName.text = value;
    }

    private void OnSetUserName(string old, string value)
    {
        this.password.text = value;
    }

    private void OnClickLoginBtn()
    {
        string userName = this.userName.text;
        string password = this.password.text;
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            // 弹窗提示

            return;
        }
        // 向登录服务器发送登录请求
        NetworkMgr.Instance.OnLogin(userName, password);
    }
}
