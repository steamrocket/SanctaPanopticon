using UnityEngine;

/// <summary>
/// ダンジョンの進行を管理するクラス
/// </summary>

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager Instance;

    // 現在のダンジョンの状態を保持する変数。DungeonRunState クラスは、現在のイベントキューや、ダンジョン内でのプレイヤーの位置などを管理するクラスとして実装されるべき。
    private DungeonRunState currentDungeonState;

    // 戦闘シーンから戻ってきたときに、次のイベントを開始するためのフラグ。戦闘シーンから戻ったときに Start() でこのフラグをチェックし、次のイベントを開始する。
    private bool resumeAfterBattle = false;

    // ダンジョン内にいるかどうかのフラグ。これも戦闘シーンから戻ってきたときに、ダンジョン内にいるかどうかを判断するために使用する。
    public bool IsInDungeon = true;

    [SerializeField] private EventDatabase eventDatabase;

    /// <summary>
    /// シングルトンの初期化。ゲーム全体で一つのインスタンスを保持し、シーンを跨いでも破壊されないようにする。
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
    /// ダンジョンを開始する。DungeonData に基づいて DungeonRunState を初期化し、最初のイベントを開始する。
    /// </summary>
    /// <param name="dungeon"></param>
    public void StartDungeon(DungeonData dungeon)
    {
        currentDungeonState = new DungeonRunState();

        foreach (string id in dungeon.initialEventIDs)
        {
            currentDungeonState.Enqueue(id);
        }

        ProceedNextEvent();
    }

    /// <summary>
    /// 戦闘シーンから戻ってきたときに、次のイベントを開始するためのフラグをセットする関数。戦闘シーンから戻ったときに Start() でこのフラグをチェックし、次のイベントを開始する。
    /// </summary>
    public void SetResumeAfterBattle()
    {
        resumeAfterBattle = true;
    }

    /// <summary>
    /// 戦闘シーンから戻ってきたときに呼び出される関数。resumeAfterBattle フラグが true の場合、次のイベントを開始する。
    /// </summary>
    void Start()
    {
        Debug.Log("DungeonManager Start");

        if (resumeAfterBattle)
        {
            Debug.Log("Resume After Battle");

            resumeAfterBattle = false;
            ProceedNextEvent();
        }
    }

    /// <summary>
    /// 現在のダンジョン状態から次のイベントIDを取得し、EventManager に渡してイベントを開始する。
    /// </summary>
    public void ProceedNextEvent()
    {
        if (!currentDungeonState.HasNext())
        {
            Debug.Log("Dungeon Complete!");
            return;
        }

        string nextID = currentDungeonState.Dequeue();
        EventData data = eventDatabase.GetEvent(nextID);

        EventManager.Instance.StartEvent(data);
    }

    /// <summary>
    /// 次のイベントを要求する関数。EventManagerに次のイベントIDを渡して、次のイベントを開始させる。
    /// </summary>
    /// <param name="eventID"></param>
    public void RequestNextEvent(string eventID)
    {
        string nextID = currentDungeonState.Dequeue();
        Debug.Log("Next EventID: " + eventID);

        EventData next = eventDatabase.GetEvent(eventID);


        if (eventID == null)
        {
            Debug.LogError("Event not found: " + nextID);
        }

        EventManager.Instance.StartEvent(next);
    }

    /// <summary>
    /// 戦闘シーンから戻ってきたときに呼び出される関数。戦闘の結果に応じて、次のイベントに進むか、ゲームオーバーにするかを判断する。
    /// </summary>
    public void ResumeAfterBattle()
    {
        ProceedNextEvent();
    }

    /// <summary>
    /// 次のイベントをキューに追加する関数。戦闘シーンから戻ってきたときに、次のイベントIDをキューに追加して、次のイベントを開始させる。
    /// </summary>
    /// <param name="id"></param>
    public void EnqueueNextEvent(string id)
    {
        currentDungeonState.Enqueue(id);
    }
}
