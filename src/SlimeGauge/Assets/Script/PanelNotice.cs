using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelNotice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextLoader.Load("Assets/Text/Notice.txt", (text) => {
            transform.Find("Text").GetComponent<Text>().text = text;
        });
    }

    /// <summary>
    /// Closeボタン押下
    /// </summary>
    public void OnClose()
    {
        Debug.Log("PanelNotice:OnClose");
        transform.gameObject.SetActive(false);
    }
}
