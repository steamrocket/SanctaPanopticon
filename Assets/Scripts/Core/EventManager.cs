using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
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

    // 現在のイベントを保持する変数。現在進行中のイベントのデータを保持するために使用される。
    [SerializeField] private EventView eventView;
    // イベントデータベースへの参照。イベントIDからイベントデータを取得するために使用される。
    [SerializeField] private EventDatabase eventDatabase;

    private EventData currentEvent;

    /// <summary>
    /// EventView をバインドする関数。EventView は、イベントの内容を表示するためのUIコンポーネントであり、EventManager はこの関数を通じて EventView を参照できるようになる。
    /// </summary>
    /// <param name="view"></param>
    public void BindEventView(EventView view)
    {
        eventView = view;
    }

    /// <summary>
    /// イベントを開始する関数。EventData を受け取り、その内容に応じて適切な処理を行う。イベントの種類に応じて、EventView に表示するか、戦闘シーンに遷移するかなどの処理が行われる。
    /// </summary>
    /// <param name="eventData"></param>
    public void StartEvent(EventData eventData)
    {
        if (eventData == null)
        {
            Debug.LogError("Cannot start event because EventData is null.");
            return;
        }

        
        currentEvent = eventData;

        switch (eventData.eventType)
        {
            case EventType.Story:
            case EventType.H:
            case EventType.Item:
                // eventView.ShowEvent(eventData);
                ShowEventView(eventData);
                break;

            case EventType.Battle:
                StartBattle(eventData);
                break;
        }
    }

    /// <summary>
    /// 戦闘イベントを開始する関数。Battle イベントが開始されたときに呼び出され、戦闘シーンに遷移する処理を行う。また、戦闘イベントの次のイベントIDを取得し、DungeonManager に次のイベントを要求する処理も行う。
    /// </summary>
    /// <param name="battleEvent"></param>
    private void StartBattle(EventData battleEvent)
    {
        Debug.Log("ShowEvent Battle: " + battleEvent);

        // 
        // var next = battleEvent.choices[0].nextEventID;
        // 
        // DungeonManager.Instance.EnqueueNextEvent(next);

        string nextEventID = GetBattleNextEventID(battleEvent);
        if (!string.IsNullOrEmpty(nextEventID) && DungeonManager.Instance != null)
        {
            DungeonManager.Instance.EnqueueNextEvent(nextEventID);
        }

        SceneManager.LoadScene("BattleScene");
    }

    /// <summary>
    /// イベントを EventView に表示する関数。EventData を受け取り、その内容を EventView に表示するための処理を行う。EventView がまだ参照されていない場合は、シーン内から EventView を検索して参照を取得する処理も行う。
    /// </summary>
    /// <param name="eventData"></param>
    private void ShowEventView(EventData eventData)
    {
        if (eventView == null)
        {
            // eventView = FindObjectOfType<EventView>();
            eventView = FindObjectOfType<EventView>(true);
        }

        if (eventView == null)
        {
            Debug.LogError("EventView not found for event: " + eventData.eventID);
            return;
        }

        eventView.ShowEvent(eventData);
    }

    /// <summary>
    /// 戦闘イベントの次のイベントIDを取得する関数。Battle イベントの EventData を受け取り、その中から次のイベントIDを取得する処理を行う。次のイベントIDが存在しない場合や、空の場合はエラーログを出力する。
    /// </summary>
    /// <param name="battleEvent"></param>
    /// <returns></returns>
    private string GetBattleNextEventID(EventData battleEvent)
    {
        if (battleEvent.choices == null || battleEvent.choices.Count == 0)
        {
            Debug.LogError("Battle event has no next event choice: " + battleEvent.eventID);
            return null;
        }

        string nextEventID = battleEvent.choices[0].nextEventID;
        if (string.IsNullOrEmpty(nextEventID))
        {
            Debug.LogError("Battle event nextEventID is empty: " + battleEvent.eventID);
        }

        // 
        // SceneManager.LoadScene("BattleScene"); // 
        return nextEventID;
    }

    /// <summary>
    /// EventManager のインスタンスを初期化する関数。シングルトンパターンを使用して、EventManager のインスタンスが複数存在しないようにする処理を行う。また、シーンが切り替わっても EventManager のインスタンスが破棄されないようにする処理も行う。
    /// </summary>
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

    /// <summary>
    /// シーンがロードされたときに呼び出される関数。SceneManager.sceneLoaded イベントに登録されており、シーンが切り替わるたびに呼び出される。シーンが切り替わった後に EventView を再度検索して参照を取得する処理を行う。
    /// </summary>
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// シーンがアンロードされたときに呼び出される関数。SceneManager.sceneLoaded イベントから登録を解除する処理を行う。これにより、シーンが切り替わった後に OnSceneLoaded 関数が呼び出されないようにする。
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// シーンがロードされたときに呼び出される関数。SceneManager.sceneLoaded イベントに登録されており、シーンが切り替わるたびに呼び出される。シーンが切り替わった後に EventView を再度検索して参照を取得する処理を行う。
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        eventView = FindObjectOfType<EventView>(true);
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
            // DungeonManager.Instance.RequestNextEvent(choice.nextEventID);
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
