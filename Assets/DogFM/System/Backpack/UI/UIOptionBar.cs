using DogFM;
using DogFM.Backpack;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ѡ����
/// �ṩһϵ�в�ͬ���ť
/// </summary>
public class UIOptionBar : MonoBehaviour
{
    /// <summary>
    /// ѡ���¼������ص�
    /// </summary>
    public UnityEvent<int> onValueChanged;

    // ������
    [SerializeField] private Image background;
    // ��ť���ڵ�
    [SerializeField] private RectTransform content;
    // ѡ��ģ��Ԥ����
    [SerializeField] private UIOption templateOption;

    // ��ť���ӳ��
    private Dictionary<UIOption, int> optionMap = new Dictionary<UIOption, int>();

    private void Reset()
    {
        background = transform.Find("Background").GetComponent<Image>();
        content = transform.Find("ViewPort/Content").GetComponent<RectTransform>();
        templateOption = transform.Find("ViewPort/Template").GetComponent<UIOption>();
    }

    /// <summary>
    /// ��ʼ��ѡ����
    /// </summary>
    /// <param name="models"></param>
    public void SetContext(List<OptionModel> models)
    {
        for (int i = 0; i < models.Count; i++)
        {
            // ����ģ��Ԥ���壬���ø�����
            GameObject optionGO = GameObject.Instantiate(templateOption.gameObject);
            optionGO.transform.SetParent(content);

            // ������ʽ
            UIOption uiOption = optionGO.GetComponent<UIOption>();
            uiOption.SetContext(models[i]);

            // ��ӵ�ѡ���б�
            optionMap[uiOption] = i;

            // ��Ӽ���
            uiOption.onClick.AddListener((option) => onValueChanged.Invoke(optionMap[option]));
        }
    }
}
