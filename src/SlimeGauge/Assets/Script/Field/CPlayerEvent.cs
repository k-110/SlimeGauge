using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerEvent : MonoBehaviour
{
    [SerializeField] GameObject ObjectField;

    /// <summary>
    /// イベント：移動中の衝突時に呼ばれる処理
    /// </summary>
    /// <param name="HitInfo">衝突判定に関する詳細情報</param>
    void OnControllerColliderHit(ControllerColliderHit HitInfo)
    {
        if (HitInfo.gameObject.name.Contains("slime"))
        {
            ObjectField.GetComponent<TagField>().OnTagEnd();
        }
    }
}
