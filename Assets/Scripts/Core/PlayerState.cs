using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

// プレイヤー(観測者)の状態を管理するクラス
public class PlayerState
{
    public int observationLevel;  // 観測者としての介入度
    public bool secretUnlocked;
}
