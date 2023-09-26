using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using DataContents;

public class DataManager
{
    public Dictionary<int, DataByLevel> Dict_Stat { get; private set; } = new Dictionary<int, DataByLevel>();
    public Dictionary<eMonster, DataByMonster> Dict_Monster { get; private set; } = new Dictionary<eMonster, DataByMonster>();

    public PlayerData playerData = new PlayerData();

    public Inventorydata invenData = new Inventorydata();

    public void Init()
    {
        Dict_Stat = LoadJson<StatData, int, DataByLevel>("DataByLevel").Make();
        Dict_Monster = LoadJson<MonsterData, eMonster, DataByMonster>("DataByMonster").Make();

        string player = Managers._file.LoadJson("PlayerData");
        if (string.IsNullOrEmpty(player) == false)
            playerData = JsonUtility.FromJson<PlayerData>(player);

        string inven = Managers._file.LoadJson("Inventorydata");
        if (string.IsNullOrEmpty(inven) == false)
            invenData = JsonUtility.FromJson<Inventorydata>(inven);
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        string text = Managers._file.LoadJson(path);
        return JsonUtility.FromJson<Loader>(text);
    }

    public void SaveGameData()
    {
        //인벤토리 매니저 && 플레이어 스탯에 저장 콜 후 저장

        Managers._file.SaveJson<PlayerData>(playerData, "PlayerData");
        Managers._file.SaveJson<Inventorydata>(invenData, "Inventorydata");
    }

}
