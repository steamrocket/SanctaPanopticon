using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シスター一人一人の状態を管理するクラス
[System.Serializable]
public class SisterState
{
    public string sisterID;
    public int faith;　//信仰
    public int stigma;　//淫紋Lv
    public int demonBond;　//悪魔契約Lv
    public int angelBond;　//天使契約Lv
    public bool isCorrupted;　//堕落フラグ（特定淫紋Lv以上でTrue）
    public int sanity;　//正気度
    public int outfitStage;　//衣装ステージ（0=通常,1=軽破損,2=半裸,3=崩壊）
    public SisterStatusState statusState;　//シスターの現在の状態

}
