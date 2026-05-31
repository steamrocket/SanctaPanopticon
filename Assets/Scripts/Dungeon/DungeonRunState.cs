using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ダンジョンの進行状態を管理するクラス(データ専用)
[System.Serializable]
public class DungeonRunState
{
    public Queue<string> eventQueue = new Queue<string>();

    // ダンジョンのイベントをキューに追加するための関数
    public void Enqueue(string eventID)
    {
        eventQueue.Enqueue(eventID);
    }

    // ダンジョンのイベントを順番に処理するための関数
    public string Dequeue()
    {
        if (eventQueue.Count > 0)
            return eventQueue.Dequeue();

        return null;
    }

    // 次のイベントがあるかどうかを確認する関数
    public bool HasNext()
    {
        return eventQueue.Count > 0;
    }
}
