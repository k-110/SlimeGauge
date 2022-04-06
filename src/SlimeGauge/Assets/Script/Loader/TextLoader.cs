using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TextLoader
{
    /// <summary>
    /// テキストをロードする
    /// ※Addressableを使用
    /// ※読み込みに失敗した場合は「""(空)」となる
    /// </summary>
    /// <param name="TextPath">テキストのパス</param>
    /// <param name="Complete">読み込み完了時に呼び出される処理</param>
    public static void Load(string TextPath, System.Action<string> Complete)
    {
        Debug.Log(string.Format("TextLoader:Start\tPath:{0}", TextPath));
        Addressables.LoadAssetAsync<TextAsset>(TextPath).Completed += text =>
        {
            string Data = "";
            if (text.Status.Equals(AsyncOperationStatus.Succeeded))
            {
                Data = text.Result.text;
            }
            Complete(Data);
            Debug.Log(string.Format("TextLoader:Complete\tPath:{0}", TextPath));
        };
    }
}
