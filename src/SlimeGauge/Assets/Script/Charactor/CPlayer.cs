using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CCharactor
{
    protected override float MoveSpeed { get; } = 10.0f;
    protected override Vector3 StartingPosition { get; } = new Vector3(0, 0, 0);

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="CharaModel">ベースとなるモデル</param>
    public CPlayer(GameObject CharaModel) : base(CharaModel)
    {
        //ベースクラスのコンストラクタで処理しているので何もしなくて良い
    }

    /// <summary>
    /// キャラクタを行動させる
    /// </summary>
    /// <param name="MyEnemy">敵のリスト</param>
    public void OnAction(List<CEnemy> MyEnemy)
    {
        //TODO:
        KeyOperation();
        base.OnAction();
    }
}
