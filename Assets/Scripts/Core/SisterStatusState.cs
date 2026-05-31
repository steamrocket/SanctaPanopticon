using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SisterStatusState : MonoBehaviour
{
    // シスターの現在の状態
    public enum SisterStatusStateInfo
    {
        Active,　//通常状態
        DemonSlave,　//悪魔奴隷状態
        AngelProperty,　//天使所有物状態
        Lost　//喪失状態
    }
}
