using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DogFM;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DogFM.Backpack
{
    /// <summary>
    /// 背包系统道具槽UI
    /// 用于存放一个道具
    /// </summary>
    public class UISlot : MonoBehaviour, IPointerClickHandler
    {
        // 选择事件回调
        public UnityEvent<Item> onClick;

        // 道具名称
        [SerializeField] new private Text name;
        // 格子背景
        [SerializeField] private Image background;
        // 道具图标
        [SerializeField] private Image Icon;
        // 道具数量
        [SerializeField] private Text count;
        // 道具实例
        [SerializeField] private Item item;

        private void Awake()
        {
            this.name = transform.Find("Name").GetComponent<Text>();
            this.Icon = transform.Find("Background").GetComponent<Image>();
            this.Icon = transform.Find("Icon").GetComponent<Image>();
            this.count = transform.Find("Count").GetComponent<Text>();
        }

        /// <summary>
        /// 填充物品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void SetContext(Item item)
        {
            this.item = item;
            this.name.text = item.Name;
            this.Icon.sprite = ResMgr.Instance.Load<Sprite>(item.Icon);
            this.count.text = item.Count.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(this.item);
        }
    }
}
