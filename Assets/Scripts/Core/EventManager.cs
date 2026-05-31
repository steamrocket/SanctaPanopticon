using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// 世界で起きる「出来事」を、すべて同じ形式で処理するクラス（世界の交通整理係）
// 今何が起きているのか？　次に何が起きるのか？　を管理するクラス
//　EventManagerの責務は4つ
//　１．EventData を受け取る
//　2．表示する（UIに投げる）
//　3．選択肢を GameManager に渡す
//　4．次の Event を要求する
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [SerializeField] private EventView eventView;

    [SerializeField] private EventDatabase eventDatabase;

    private EventData currentEvent;

    public void StartEvent(EventData eventData)
    {
        currentEvent = eventData;

        switch (eventData.eventType)
        {
            case EventType.Story:
            case EventType.H:
            case EventType.Item:
                eventView.ShowEvent(eventData);
                break;

            case EventType.Battle:
                StartBattle(eventData);
                break;
        }
    }

    private void StartBattle(EventData battleEvent)
    {
        Debug.Log("ShowEvent Battle: " + battleEvent);

        // 状態変化
        var next = battleEvent.choices[0].nextEventID;
        // 次のイベントをキューに追加する
        DungeonManager.Instance.EnqueueNextEvent(next);

        // 仮実装：戦闘シーン遷移
        SceneManager.LoadScene("BattleScene"); // 仮シーン
    }

    // シングルトンの初期化
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ★ここが入口
    //public void StartEvent(EventData eventData)
    //{
    //    eventView.ShowEvent(eventData);

    //    Debug.Log("ShowEvent called: " + eventData.eventID);
    //}

    // ★選択肢が押されたとき
    public void OnChoiceSelected(ChoiceData choice)
    {
        // 状態変化
        GameManager.Instance.ApplyChoice(choice);

        // 次のイベントがあるなら進む
        if (!string.IsNullOrEmpty(choice.nextEventID))
        {
            StartEventByID(choice.nextEventID);
            DungeonManager.Instance.RequestNextEvent(choice.nextEventID);
        }
        else
        {
            EndEvent();
        }
    }

    // イベントIDからイベントを開始するヘルパー関数
    private void StartEventByID(string eventID)
    {
        EventData next = eventDatabase.GetEvent(eventID);
        StartEvent(next);
    }

    /// <summary>
    /// ゲーム開始時にイベントデータベースを初期化する。
    /// </summary>
    private void Start()
    {
        eventDatabase.Initialize();

    }

    private void EndEvent()
    {
        // ダンジョン進行へ戻す（後で実装）
    }

}
