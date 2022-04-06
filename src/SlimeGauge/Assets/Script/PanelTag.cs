using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTag : MonoBehaviour
{
    public TimeSpan TagTimer { get; private set; } = new TimeSpan(0);
    private bool TimerStart = false;
    private DateTime StartTime = DateTime.Now;
    private readonly System.Object LockObject = new System.Object();

    // Start is called before the first frame update
    void Start()
    {
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    /// <summary>
    /// タイマー表示の更新
    /// </summary>
    void UpdateTimer()
    {
        lock (LockObject)
        {
            if (TimerStart)
            {
                TagTimer = DateTime.Now - StartTime;
            }
            transform.Find("TextTimer").GetComponent<Text>().text = TagTimer.ToString(@"hh\:mm\:ss");
        }
    }

    /// <summary>
    /// 開始
    /// </summary>
    public void OnStart()
    {
        StartTime = DateTime.Now;
        TimerStart = true;
        UpdateTimer();
    }

    /// <summary>
    /// 終了
    /// </summary>
    public void OnEnd()
    {
        UpdateTimer();
        TimerStart = false;
    }
}
