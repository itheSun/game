﻿/*-------------------------------------------------------------------------
 * 作者：@白泽
 * 联系方式：xzjH5263@163.com
 * 创建时间：2022/6/28 15:48:59
 * 描述：
 *  -------------------------------------------------------------------------*/

using DogFM.Backpack;
using DogFM.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.DogFM.System.Backpack.UI
{
    /// <summary>
    /// 道具详情面板
    /// </summary>
    public class UIItemDetail : MonoBehaviour
    {
        // 减少使用数量
        private Button reduceBtn;
        // 增加使用数量
        private Button addBtn;
        // 使用
        private Button useBtn;
        // 出售
        private Button sellBtn;
        // 关闭按钮
        private Button closeBtn;
        // 数量条
        private Slider slider;

        private void Awake()
        {
            reduceBtn = transform.Find<Button>("ReduceBtn");
            addBtn = transform.Find<Button>("AddBtn");
            useBtn = transform.Find<Button>("UseBtn");
            sellBtn = transform.Find<Button>("SellBtn");
            closeBtn = transform.Find<Button>("CloseBtn");

            slider = transform.Find<Slider>("Slider");

            reduceBtn.onClick.AddListener(OnReduceCount);
            addBtn.onClick.AddListener(OnAddCount);
            useBtn.onClick.AddListener(OnUseItem);
            //closeBtn.onClick.AddListener();
        }

        private void OnUseItem()
        {
            
        }

        private void OnAddCount()
        {
            
        }

        private void OnReduceCount()
        {
            
        }
    }
}
