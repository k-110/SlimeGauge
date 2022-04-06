using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy : CCharactor
{
    protected override float MoveSpeed { get; } = 8.0f;
    protected override Vector3 StartingPosition { get; } = new Vector3(30, 0, 30);

    private readonly int EnemyID;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="CharaModel">ベースとなるモデル</param>
    public CEnemy(GameObject CharaModel, int CharaID) : base(GameObject.Instantiate(CharaModel))
    {
        EnemyID = CharaID;
        Debug.Log(string.Format("CEnemy:{0}", EnemyID));
    }

    /// <summary>
    /// キャラクタを行動させる
    /// </summary>
    /// <param name="MyPlayer">プレイヤー</param>
    public void OnAction(CPlayer MyPlayer)
    {
        switch (EnemyID)
        {
            case 2:
                SetDestination(MyPlayer.GetPosition() + Enemy02Config);
                break;
            case 3:
                SetDestination(MyPlayer.GetPosition() + Enemy03Config);
                break;
            case 4:
                SetDestination(MyPlayer.GetPosition() + Enemy04Config);
                break;
            case 5:
                SetDestination(MyPlayer.GetPosition() + Enemy05Config);
                break;
            case 6:
                SetDestination(RoundToRange(MyPlayer.GetPosition(), Enemy06Config));
                break;
            case 7:
                SetDestination(RoundToRange(MyPlayer.GetPosition(), Enemy07Config));
                break;
            case 8:
                SetDestination(RoundToRange(MyPlayer.GetPosition(), Enemy08Config));
                break;
            case 9:
                SetDestination(RoundToRange(MyPlayer.GetPosition(), Enemy09Config));
                break;
            case 10:
                SetDestination(RoundToRange(MyPlayer.GetPosition(), Enemy10Config));
                break;
            default:
                SetDestination(MyPlayer.GetPosition());
                break;
        }
        base.OnAction();
    }

    private Vector3 Enemy02Config = new Vector3(3, 0, 0);
    private Vector3 Enemy03Config = new Vector3(0, 0, 3);
    private Vector3 Enemy04Config = new Vector3(-3, 0, 0);
    private Vector3 Enemy05Config = new Vector3(0, 0, -3);
    private Rect Enemy06Config = new Rect(-50, 0, 50, 50);
    private Rect Enemy07Config = new Rect(0, 0, 50, 50);
    private Rect Enemy08Config = new Rect(0, -50, 50, 50);
    private Rect Enemy09Config = new Rect(-50, -50, 50, 50);
    private Rect Enemy10Config = new Rect(-25, -25, 50, 50);

    /// <summary>
    /// 位置を範囲に内に丸める
    /// </summary>
    /// <param name="Pos">位置</param>
    /// <param name="Range">範囲</param>
    /// <returns>丸めた位置</returns>
    private Vector3 RoundToRange(Vector3 Pos, Rect Range)
    {
        if (Pos.x < Range.x) { Pos.x = Range.x; }
        else if ((Range.x + Range.width) < Pos.x) { Pos.x = Range.x + Range.width; }
        if (Pos.z < Range.y) { Pos.z = Range.y; }
        else if ((Range.y + Range.height) < Pos.z) { Pos.z = Range.x + Range.height; }
        return Pos;
    }
}
