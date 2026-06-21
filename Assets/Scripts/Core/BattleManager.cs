using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 戦闘シーンを管理するクラス。プレイヤーの攻撃、敵の攻撃、勝利条件などを処理する。
/// </summary>
public class BattleManager : MonoBehaviour
{
    [SerializeField] private string standaloneAfterBattleEventID = "Abbey_AfterBattle";

    int enemyHP = 10; // 仮の敵HP。実際にはイベントデータから取得するべき。

    /// <summary>
    /// プレイヤーの攻撃処理。攻撃力はランダムで決定され、敵のHPを減少させる。敵のHPが0以下になった場合は勝利処理を呼び出す。
    /// </summary>
    public void Attack()
    {
        enemyHP -= Random.Range(4, 8);

        if (enemyHP <= 0)
        {
            Win();
            return;
        }

        //EnemyTurn();
    }

    /// <summary>
    /// 敵の攻撃処理。攻撃力はランダムで決定され、プレイヤーのHPを減少させる。プレイヤーのHPが0以下になった場合はゲームオーバー処理を呼び出す。
    /// </summary>
    void EnemyTurn()
    {
        int damage = Random.Range(3, 6);
        GameManager.Instance.DamagePlayer(damage);
    }

    /// <summary>
    /// 勝利処理。戦闘に勝利した場合の処理を行う。現在は仮実装として、戦闘シーンからメインシーンに遷移する。
    /// </summary>
    void Win()
    {
        Debug.Log("Battle Win");

        bool hasActiveDungeon = DungeonManager.Instance != null && DungeonManager.Instance.HasActiveDungeon;

        if (!hasActiveDungeon)
        {
            DungeonManager.QueueEventOnNextDungeonStart(standaloneAfterBattleEventID);
        }

        // SceneManager.LoadScene("MainScene");

        // DungeonManager.Instance.ResumeAfterBattle();

        if (hasActiveDungeon)
        {
            //DungeonManager.Instance.ResumeAfterBattle();
            DungeonManager.Instance.SetResumeAfterBattle();
        }

        SceneManager.LoadScene("MainScene");
    }
}
