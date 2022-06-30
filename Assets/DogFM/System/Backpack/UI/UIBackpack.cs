using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogFM;
using UnityEngine.UI;
using DogFM.MVVM;
using System;

namespace DogFM.Backpack
{
    /// <summary>
    /// 背包系统UI
    /// </summary>
    public class UIBackpack : MonoBehaviour
    {
        // 主背景
        [SerializeField] private Image background;
        // 分类选项栏
        [SerializeField] private UIOptionBar optionBar;
        // UI道具槽预制体
        [SerializeField] private UISlot templateSlot;
        // UI道具槽列表
        [SerializeField] private List<UISlot> slots;

        // Start is called before the first frame update
        void Awake()
        {
            background = transform.Find("Background").GetComponent<Image>();

            optionBar.onValueChanged.AddListener(OnSelectItemType);
        }

        /// <summary>
        /// 切换背包显示道具类别
        /// </summary>
        /// <param name="index"></param>
        private void OnSelectItemType(int index)
        {
            // 向道具系统请求该类别的所有道具
            Item[] items = GameApp.inventorySystem.GetItemsByType((ItemType)index);
            int itemCount = items.Length;
            int slotCount = slots.Count;
            // 填充道具
            for (int i = 0; i < itemCount && i < slotCount; ++i)
            {
                slots[i].SetContext(items[i]);
            }
            // UI槽不足，新增UI槽
            for (int i = slotCount; i < itemCount; ++i)
            {
                GameObject slotGO = GameObject.Instantiate(templateSlot).gameObject;
                UISlot slot = slotGO.GetComponent<UISlot>();
                slot.SetContext(items[i]);
                slot.onClick.AddListener(OnShowItemDetail);
                slots.Add(slot);
            }

            // UI槽剩余，移除监听
            for (int i = itemCount; i < slotCount; ++i)
            {
                slots[i].onClick.RemoveListener(OnShowItemDetail);
            }
        }

        private void OnShowItemDetail(Item item)
        {

        }
    }
}

