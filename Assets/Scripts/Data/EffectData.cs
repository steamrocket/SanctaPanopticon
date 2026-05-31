using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Faith, //信仰度
    Stigma, //淫紋度
    Corruption, //堕落度
    Item, //アイテム
    Flag //イベント
}

public enum EffectTarget
{
    Player, 
    Party,
    Sister
} 

// イベントの効果を表すクラス
[System.Serializable]
public class EffectData
{
    public EffectType effectType; //効果の種類
    public EffectTarget target; //効果の対象
    public int value; //効果の値

    //public int faithDelta;　//信仰変化量
    //public int stigmaDelta;　//淫紋変化量
    //public int sanityDelta;　//正気度変化量
    //public int demonBondDelta;　//悪魔契約変化量
    //public int angelBondDelta;　//天使契約変化量
    //public string nextEventID;　//次のイベントID
}
