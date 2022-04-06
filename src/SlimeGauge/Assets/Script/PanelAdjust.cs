using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //TODO:
    }

    /// <summary>
    /// Closeボタン押下
    /// </summary>
    public void OnClose()
    {
        Debug.Log("PanelAdjust:OnClose");
        transform.gameObject.SetActive(false);
    }
}
