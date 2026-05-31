using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//条件クラス
[System.Serializable]
public class EventCondition
{
    public int minFaith;　//最低信仰値
    public int minStigma;　//最低淫紋Lv
    public bool requireCorrupted;　//堕落必須フラグ
}
