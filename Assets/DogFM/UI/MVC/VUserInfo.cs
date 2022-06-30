using DogFM;
using DogFM.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class VUserInfo : BasePanel
{
    private Text nickNameTxt;
    private Text lvTxt;

    private Button alterNameBtn;

    public OnEventTrigger<string> OnAlterName;

    private void Awake()
    {
        nickNameTxt = transform.Find<Text>("NickNameTxt");
        lvTxt = transform.Find<Text>("LVTxt");
        alterNameBtn = transform.Find<Button>("AlterNameBtn");

        alterNameBtn.onClick.AddListener(OnClickAlterNameBtn);
    }

    private void OnClickAlterNameBtn()
    {
        string value = this.nickNameTxt.text;
        OnAlterName?.Invoke(value);
    }

    public void OnNameUpdate(string value)
    {
        nickNameTxt.text = value;
    }


}
