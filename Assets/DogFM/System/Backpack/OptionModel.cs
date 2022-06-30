﻿/*-------------------------------------------------------------------------
 * 作者：@白泽
 * 联系方式：xzjH5263@163.com
 * 创建时间：2022/6/28 0:08:14
 * 描述：
 *  -------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DogFM.Backpack
{
    public class OptionModel
    {
        // 名称
        [SerializeField] private string name;
        // 背景
        [SerializeField] private Sprite background;
        // 图标
        [SerializeField] private Sprite icon;

        public string Name { get => name; }
        public Sprite Background { get => background; }
        public Sprite Icon { get => icon; }

        public OptionModel(string name, Sprite background, Sprite icon)
        {
            this.name = name;
            this.background = background;
            this.icon = icon;
        }
    }
}
