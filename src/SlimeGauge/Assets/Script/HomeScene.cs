using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : MonoBehaviour
{
    /// <summary>
    /// 鬼ごっこを開始
    /// </summary>
    public void StartTag()
    {
        Debug.Log("HomeScene:StartTag");
        transform.Find("PanelMenu").gameObject.SetActive(false);
        transform.GetComponent<TagField>().OnInitial();
        transform.Find("PanelTag").GetComponent<PanelTag>().OnStart();
        transform.GetComponent<TagField>().OnTagStart(EndTag);
    }

    /// <summary>
    /// 鬼ごっこを終了
    /// </summary>
    public void EndTag()
    {
        Debug.Log("HomeScene:EndTag");
        transform.Find("PanelTag").GetComponent<PanelTag>().OnEnd();
        transform.Find("PanelMenu").gameObject.SetActive(true);
    }

    /// <summary>
    /// PanelAdjustを表示
    /// </summary>
    public void ViewPanelAdjust()
    {
        Debug.Log("HomeScene:ViewPanelAdjust");
        transform.Find("PanelAdjust").gameObject.SetActive(true);
    }

    /// <summary>
    /// PanelNoticeを表示
    /// </summary>
    public void ViewPanelNotice()
    {
        Debug.Log("HomeScene:ViewPanelNotice");
        transform.Find("PanelNotice").gameObject.SetActive(true);
    }
}
