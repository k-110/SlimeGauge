using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CCharactor
{
    private readonly GameObject MyModel = null;
    private readonly Animator MyAnime = null;
    private readonly CharacterController MyCtrl = null;

    private readonly float FieldH = 50.0f;
    private readonly float FieldV = 50.0f;
    private readonly float ContactDistance = 0.1f;

    protected abstract float MoveSpeed { get; }
    protected abstract Vector3 StartingPosition { get; }

    private readonly int StateWait = 0;
    private readonly int StateWalk = 1;

    private readonly System.Object LockObject = new System.Object();
    private DateTime StartTime = DateTime.Now;
    private Vector3 Destination = Vector3.zero;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="CharaModel">ベースとなるモデル</param>
    public CCharactor(GameObject CharaModel)
    {
        MyModel = CharaModel;
        MyModel.SetActive(true);
        MyAnime = MyModel.transform.GetComponent<Animator>();
        MyCtrl = MyModel.GetComponent<CharacterController>();
    }

    /// <summary>
    /// ゲームオブジェクトのクリア
    /// </summary>
    public void ObjectClear()
    {
        UnityEngine.Object.Destroy(MyModel);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void OnInitial()
    {
        MyModel.transform.position = StartingPosition;
        SetState(StateWait);
        SetDestination(StartingPosition);
        StartTime = DateTime.Now;
    }

    /// <summary>
    /// キャラクタを行動させる
    /// </summary>
    public void OnAction()
    {
        //配置の変更(MyModel.transform.position = StartingPosition)と
        //移動(MyCtrl.SimpleMove(MyModel.transform.forward * MoveSpeed))が
        //競合して配置の変更が無効になったような動作をするため時間を開ける
        if (1 <= (DateTime.Now - StartTime).Seconds)
        {
            Move();
        }
    }

    /// <summary>
    /// キャラクタの行動を終了する
    /// </summary>
    public void OnActionEnd()
    {
        Stop();
    }

    /// <summary>
    /// 同一の位置にいるか？
    /// </summary>
    /// <param name="Pos">位置</param>
    /// <returns>判定結果</returns>
    public bool IsSamePosition(Vector3 Pos)
    {
        float Distance = (GetPosition() - Pos).magnitude;
        return (Distance < ContactDistance);
    }

    /// <summary>
    /// キャラクタの位置を取得
    /// </summary>
    /// <returns>キャラクタの位置</returns>
    public Vector3 GetPosition()
    {
        Vector3 pos = MyModel.transform.position;
        return pos;
    }

    /// <summary>
    /// 移動する位置を設定
    /// 指定がフィールド外の場合は、フィールドの端に丸める
    /// </summary>
    /// <param name="Pos">移動する位置</param>
    public void SetDestination(Vector3 Pos)
    {
        if (Pos.x < -FieldH) { Pos.x = -FieldH; }
        else if (FieldH < Pos.x) { Pos.x = FieldH; }

        if (Pos.z < -FieldV) { Pos.z = -FieldV; }
        else if (FieldV < Pos.z) { Pos.z = FieldV; }

        Destination = Pos;
    }

    /// <summary>
    /// キャラクタを停止する
    /// </summary>
    public void Stop()
    {
        lock (LockObject)
        {
            if (!GetState().Equals(StateWait))
            {
                MyCtrl.SimpleMove(Vector3.zero);
                SetState(StateWait);
                SetDestination(GetPosition());
            }
        }
    }

    /// <summary>
    /// 設定した位置へ移動する
    /// </summary>
    public void Move()
    {
        bool NoNeedToMove;
        lock (LockObject)
        {
            NoNeedToMove = IsSamePosition(Destination);
            if (!NoNeedToMove)
            {
                if (!GetState().Equals(StateWalk))
                {
                    SetState(StateWalk);
                }
                MyModel.transform.rotation = Quaternion.LookRotation(GetDirection());
                MyCtrl.SimpleMove(MyModel.transform.forward * MoveSpeed);
            }
        }
        if (NoNeedToMove)
        {
            Stop();
        }
    }

    /// <summary>
    /// 状態の取得
    /// </summary>
    /// <returns>状態</returns>
    private int GetState()
    {
        return MyAnime.GetInteger("State");
    }

    /// <summary>
    /// 状態の設定
    /// </summary>
    /// <param name="Status">状態</param>
    private void SetState(int Status)
    {
        MyAnime.SetInteger("State", Status);
    }

    /// <summary>
    /// 進行方向を取得
    /// </summary>
    /// <returns>進行方向</returns>
    private Vector3 GetDirection()
    {
        Vector3 Acc = Destination - GetPosition();
        return Acc;
    }

    /// <summary>
    /// キー操作
    /// ※デバッグ用の処理
    /// </summary>
    public void KeyOperation()
    {
        float keyH = Input.GetAxis("Horizontal");
        float keyV = Input.GetAxis("Vertical");
        if ((keyH != 0) || (keyV != 0))
        {
            Vector3 Difference = new Vector3(keyH, 0 , keyV);
            SetDestination(GetPosition() + Difference);
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            SetDestination(GetPosition());
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                SetDestination(hit.point);
            }
        }
    }
}
