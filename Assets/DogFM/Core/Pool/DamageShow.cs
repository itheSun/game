using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DogFM;

public enum PoolID
{
    DamageShowTxt,
}


public class DamageShow : DntdMonoSingleton<DamageShow>
{
    private GameObject txtPref;

    private MonoPool txtPool;

    private void Init()
    {
        txtPref = ResMgr.Instance.Load<GameObject>("UI/DamageShowTxt");
        txtPool = MonoPoolController.Instance.New(PoolID.DamageShowTxt, txtPref, 10);
    }

    [SerializeField]
    private float throwMulti = 10f;
    public void Show(Vector2 screenPos, string content, Color color, float time)
    {
        if (txtPool == null)
            Init();

        GameObject txtGo = txtPool.Borrow();
        txtGo.transform.SetParent(GameObject.Find("Canvas").transform);
        txtGo.transform.position = screenPos;
        Text txt = txtGo.GetComponent<Text>();
        txt.color = color;
        txt.text = content;

        Rigidbody2D rig2D = txtGo.GetComponent<Rigidbody2D>();
        rig2D.AddForce(Vector2.up * throwMulti, ForceMode2D.Force);
        DogFM.GameApp.Instance.StartCoroutine(ShowAnim(txtGo, time));
    }

    float tempTime = 0;
    IEnumerator ShowAnim(GameObject txtGo, float time)
    {
        while (tempTime <= time)
        {
            tempTime += Time.deltaTime;
            txtGo.transform.localScale = Vector3.one * ((time - tempTime) / time);
            yield return null;
        }
        tempTime = 0f;
        txtPool.Return(txtGo);
        yield return null;
    }
}