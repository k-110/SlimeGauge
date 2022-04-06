using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagField : MonoBehaviour
{
    [SerializeField] GameObject ModelPlayer;
    [SerializeField] GameObject ModelEnemy;

    private CPlayer MyPlayer = null;
    private readonly List<CEnemy> MyEnemy = new List<CEnemy>();
    private bool TagStart = false;
    private System.Action TagComplete = null;

    private readonly System.Object LockObject = new System.Object();

    // Update is called once per frame
    void Update()
    {
        if (TagStart)
        {
            //敵の増援処理
            EnemyReinforcements();

            //キャラクタを行動させる
            MyPlayer.OnAction(MyEnemy);
            foreach (CEnemy enemy in MyEnemy)
            {
                enemy.OnAction(MyPlayer);
            }
        }
    }

    /// <summary>
    /// フィールドの初期化
    /// </summary>
    public void OnInitial()
    {
        //敵を削除(前回の情報が残っているため)
        foreach (CEnemy enemy in MyEnemy)
        {
            enemy.ObjectClear();
        }
        MyEnemy.Clear();

        //プレイヤーを初期化
        if (MyPlayer == null)
        {
            MyPlayer = new CPlayer(ModelPlayer);
        }
        MyPlayer.OnInitial();     
    }

    /// <summary>
    /// 鬼ごっこの進行開始
    /// </summary>
    /// <param name="Complete">鬼ごっこの終了時に呼び出される処理</param>
    public void OnTagStart(System.Action Complete)
    {
        //終了時に呼び出す処理を保存しておく
        TagComplete = Complete;

        //開始
        TagStart = true;
        Debug.Log(string.Format("OnTagStart"));
    }

    /// <summary>
    /// 鬼ごっこの終了
    /// </summary>
    public void OnTagEnd()
    {
        lock (LockObject)
        {
            if (TagStart)
            {
                Debug.Log(string.Format("OnTagEnd"));
                TagStart = false;
                MyPlayer.OnActionEnd();
                foreach (CEnemy enemy in MyEnemy)
                {
                    enemy.OnActionEnd();
                }

                TagComplete?.Invoke();
            }
        }
    }

    /// <summary>
    /// 敵の増援処理
    /// </summary>
    public void EnemyReinforcements()
    {
        if (MyEnemy.Count < 10)
        {
            if ((60 * MyEnemy.Count) <= transform.Find("PanelTag").GetComponent<PanelTag>().TagTimer.TotalSeconds)
            {
                CEnemy enemy = new CEnemy(ModelEnemy, MyEnemy.Count + 1);
                enemy.OnInitial();
                MyEnemy.Add(enemy);
            }
        }
    }
}
