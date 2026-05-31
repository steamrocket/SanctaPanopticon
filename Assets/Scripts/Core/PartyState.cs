using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// パーティー（シスター達）全体の状態を管理するクラス
[System.Serializable]
public class PartyState
{
    public List<SisterState> activeSisters;
}
