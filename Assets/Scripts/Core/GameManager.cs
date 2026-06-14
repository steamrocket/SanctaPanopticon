using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

// ゲーム全体を統括するマネージャークラス
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    // プレイヤーの状態を保持する変数
    public PlayerState playerState;

    // パーティーの状態を保持する変数
    public PartyState partyState;

    // シスター個人毎の状態を保持する変数
    public List<SisterState> sisterStates;

    // プレイヤーのHPなどの基本ステータス
    public int PlayerHP { get; private set; }
    public int MaxHP = 30; // 仮の最大HP

    // ゲーム進行保持
    public int currentChapter;
    public string currentDungeonID;

    //　プレイヤーの選択結果蓄積
    //　ステータス、進行度、イベントフラグ等
    public Dictionary<string, bool> eventFlags;

    // プレイヤーの選択結果に基づく数値(信仰、淫紋)
    public int faith;
    public int stigma;

    // テスト用ダンジョンデータ（実際にはセーブ・ロード機能で管理されるべき）
    [SerializeField] private DungeonData testDungeon;

    /// <summary>
    /// シングルトンの初期化。ゲーム全体で一つのインスタンスを保持し、シーンを跨いでも破壊されないようにする。
    /// </summary>
    void Awake()
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
    /// プレイヤーのHPを初期化する関数。ゲーム開始時やダンジョン開始時に呼び出される。
    /// </summary>
    public void InitPlayer()
    {
        PlayerHP = MaxHP;
    }

    /// <summary>
    /// プレイヤーにダメージを与える関数。HPを減らし、HPが0以下になった場合はゲームオーバー処理を呼び出す。
    /// </summary>
    /// <param name="amount"></param>
    public void DamagePlayer(int amount)
    {
        PlayerHP -= amount;

        if (PlayerHP <= 0)
        {
            PlayerHP = 0;
            GameOver();
        }
    }

    /// <summary>
    /// 戦闘シーンを開始する関数。BattleManagerに戦闘イベントデータを渡して、戦闘シーンに遷移させる。
    /// </summary>
    public void StartBattle()
    {
    /// </summary>
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOverScene"); // 仮シーン
    }

    /// <summary>
    /// 
    /// </summary>
    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOverScene"); // V[
    }

    //セーブ・ロードの窓口
    //public void SaveGame();
    //public void LoadGame();

    /// <summary>
    /// ゲーム開始時にテスト用ダンジョンを開始する。実際にはセーブ・ロード機能やメインメニューから呼び出されるべき。
    /// </summary>
    private void Start()
    {
        InitPlayer();
        DungeonManager.Instance.StartDungeon(testDungeon);

        //if (DungeonManager.Instance.IsInDungeon)
        //{
        //    DungeonManager.Instance.ResumeAfterBattle();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 選択肢の効果をプレイヤーの状態に適用する関数。信仰や淫紋の変化を反映し、次のイベントに進む。
    ///  EventManagerから渡される選択肢の効果を適用
    /// </summary>
    /// <param name="effects"></param>
    public void ApplyEffects(EffectData[] effects)
    {
        foreach (var effect in effects)
        {
            // faith++, stigma++ など
        }
    }

    /// <summary>
    /// 選択肢の効果をプレイヤーの状態に適用する関数。信仰や淫紋の変化を反映し、次のイベントに進む。 
    /// </summary>
    /// <param name="choice"></param>
    public void ApplyChoice(ChoiceData choice)
    {
        faith += choice.faithChange;
        stigma += choice.stigmaChange;

        Debug.Log("Faith: " + faith);
        Debug.Log("Stigma: " + stigma);

        //DungeonManager.Instance.ProceedNextEvent();
    }

}