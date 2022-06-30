using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageUnit : MonoBehaviour
{
    private Image background;
    private Image headImg;
    private Text nickNameTxt;
    private Text messageTxt;

    private void Awake()
    {
        this.background = transform.Find<Image>("Background");
        this.headImg = transform.Find<Image>("HeadImg");
        this.nickNameTxt = transform.Find<Text>("NickNameTxt");
        this.messageTxt = transform.Find<Text>("MessageTxt");
    }

    private void SetMessage(Sprite sprite, string nickName, string message)
    {
        this.headImg.sprite = sprite;
        this.nickNameTxt.text = nickName;
        this.messageTxt.text = message;
    }
}
