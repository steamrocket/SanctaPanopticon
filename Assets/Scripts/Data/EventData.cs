using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// イベントの種類を定義する列挙型
public enum EventType
{
    Battle,
    Choice,
    H,
    Item,
    Story,
    System
}

// イベントの選択肢を表すクラス
[CreateAssetMenu(fileName = "EventData", menuName = "Event/EventData")]
public class EventData : ScriptableObject
{
    public string eventID;
    public EventType eventType;

    [TextArea]
    public string description;

    public List<ChoiceData> choices;

    public EventCondition condition;
}


