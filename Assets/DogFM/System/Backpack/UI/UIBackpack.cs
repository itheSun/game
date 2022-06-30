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
    /// ����ϵͳUI
    /// </summary>
    public class UIBackpack : MonoBehaviour
    {
        // ������
        [SerializeField] private Image background;
        // ����ѡ����
        [SerializeField] private UIOptionBar optionBar;
        // UI���߲�Ԥ����
        [SerializeField] private UISlot templateSlot;
        // UI���߲��б�
        [SerializeField] private List<UISlot> slots;

        // Start is called before the first frame update
        void Awake()
        {
            background = transform.Find("Background").GetComponent<Image>();

            optionBar.onValueChanged.AddListener(OnSelectItemType);
        }

        /// <summary>
        /// �л�������ʾ�������
        /// </summary>
        /// <param name="index"></param>
        private void OnSelectItemType(int index)
        {
            // �����ϵͳ������������е���
            Item[] items = GameApp.inventorySystem.GetItemsByType((ItemType)index);
            int itemCount = items.Length;
            int slotCount = slots.Count;
            // ������
            for (int i = 0; i < itemCount && i < slotCount; ++i)
            {
                slots[i].SetContext(items[i]);
            }
            // UI�۲��㣬����UI��
            for (int i = slotCount; i < itemCount; ++i)
            {
                GameObject slotGO = GameObject.Instantiate(templateSlot).gameObject;
                UISlot slot = slotGO.GetComponent<UISlot>();
                slot.SetContext(items[i]);
                slot.onClick.AddListener(OnShowItemDetail);
                slots.Add(slot);
            }

            // UI��ʣ�࣬�Ƴ�����
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

