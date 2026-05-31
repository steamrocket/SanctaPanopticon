using System;
using UnityEngine;

//EventData が持つ「選択肢の中身」
[Serializable]
public class ChoiceData
{
    public string choiceText;

    // 選択時の効果（後で拡張）
    public int faithChange;
    public int stigmaChange;

    // 次のイベント
    public string nextEventID;

    // 効果の詳細（将来的には構造体やクラスにする予定）
    public EffectData[] effects;
}