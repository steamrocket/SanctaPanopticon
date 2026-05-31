using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dungeon/DungeonData")]
public class DungeonData : ScriptableObject
{
    public string dungeonID; // ダンジョンの識別子
    public string dungeonName; // ダンジョンの名前

    [TextArea]
    public string description; // ダンジョンの説明  

    public List<string> initialEventIDs;
}
