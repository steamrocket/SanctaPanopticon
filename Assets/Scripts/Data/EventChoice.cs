using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// イベントの選択肢を表すクラス
[System.Serializable]
public class EventChoice
{
    public string choiceText;　//選択肢テキスト
    public EffectData[] effects;　//選択肢の効果
    public string nextEventID;　//次のイベントID
}
