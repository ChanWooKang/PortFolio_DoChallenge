using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using DataContents;

public class InventoryManager : MonoBehaviour
{

    #region [ Data ]

    static InventoryManager _uniqueInstance;
    public static bool ActiveChangeEquip = false;
    public Action<eEquipment, SOItem, bool> OnChangeEvent;
    public Dictionary<eEquipment, SOItem> dict_Equip = new Dictionary<eEquipment, SOItem>();
    public SOItem[] items;
    #endregion [ Data ]


    #region [ UI Component ]
    UI_Inventory inven;
    UI_Equipment equip;


    #endregion [ UI Component ]

    #region [ Property ]

    public static InventoryManager _inst { get { return _uniqueInstance; } }

    #endregion [ Property ]

    void Awake()
    {
        _uniqueInstance = this;
    }

    void Start()
    {
        Init();
        OnChangeEvent -= ChangeEquipment;
        OnChangeEvent += ChangeEquipment;
    }

    public void Init()
    {
        equip = FindObjectOfType<UI_Equipment>();
        inven = FindObjectOfType<UI_Inventory>();
        InventoryLoad();
        inven.CloseInventory();
    }

    void ChangeEquipment(eEquipment type, SOItem item, bool isWear= true)
    {
        if (item == null)
            return;
        if (ActiveChangeEquip)
            return;

        StopCoroutine(ChangeCoroutine(type, item, isWear));
        StartCoroutine(ChangeCoroutine(type, item, isWear));

    }

    IEnumerator ChangeCoroutine(eEquipment type, SOItem item, bool isWear = true)
    {
        yield return null;
        ActiveChangeEquip = true;
        if(isWear == false)
        {
            //장비 해제 하기
            if(dict_Equip.ContainsKey(type) && dict_Equip[type] == item)
            {
                List<STAT> list = dict_Equip[type].sList;
                for(int i = 0; i < list.Count; i++)
                {
                    PlayerCtrl._inst._stat.AddPlusStat(list[i].statType, -list[i].sValue);
                }
            }
            dict_Equip[type] = null;
            AddInvenItem(item);
            AddEquipItem(type, null);
        }
        else
        {
            // 장비 해제 할거 있으면 해제 하고 장비 장착
            SOItem tempItem = null;
            List<STAT> tempStat = new List<STAT>();
            if(item != null && dict_Equip.ContainsKey(type))
            {
                //장착 되어 있으면 장착 해제 후 임시 데이터 저장
                if(dict_Equip[type] != null)
                {
                    tempItem = dict_Equip[type];
                    //장비 추가 스텟 차감
                    if(tempItem.sList != null)
                    {
                        tempStat = tempItem.sList;
                        for(int i = 0; i < tempStat.Count; i++)
                        {
                            PlayerCtrl._inst._stat.AddPlusStat(tempStat[i].statType, -tempStat[i].sValue);
                        }
                    }

                    //장착 할 아이템 추가 스텟 적용
                    if(item.sList != null)
                    {
                        tempStat = item.sList;
                        for (int i = 0; i < tempStat.Count; i++)
                        {
                            PlayerCtrl._inst._stat.AddPlusStat(tempStat[i].statType, tempStat[i].sValue);
                        }
                    }

                    if (tempItem != null)
                        AddInvenItem(tempItem);
                    AddEquipItem(type, item);
                    dict_Equip[type] = item;
                }
            }

        }

        //UI변경 해야 하는 UI스크립트 체크 후 처리
        Managers._ui.OnSetUIEvent?.Invoke();
        yield return new WaitForSeconds(1.0f);
        ActiveChangeEquip = false;
    }

    public bool CheckSlotFull(SOItem _item, int cnt = 1)
    {
        

        if (inven.CheckSlotFull(_item, cnt))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddInvenItem(SOItem _item, int cnt = 1)
    {
        inven.AcquireItem(_item, cnt);
    }
    
    public void AddEquipItem(eEquipment type, SOItem item)
    {
        equip.AcquireItem(type, item);
    }

    public void OnUsePotion(SOItem _item)
    {
        if(_item.sList != null)
        {
            for(int i = 0; i < _item.sList.Count; i++)
            {
                PlayerCtrl._inst.UsePotion(_item.sList[i].statType, _item.sList[i].sValue);
                //OnChangeStat?.Invoke();
            }
        }
    }

    #region [ Save & Load ]

    public Inventorydata InventorySave()
    {
        Inventorydata saveData = new Inventorydata();
        UI_Slot[] invenSlots = inven.GetInvenSlots();
        for(int i = 0; i < invenSlots.Length; i++)
        {
            if(invenSlots[i].item != null)
            {
                saveData.InvenArrayNumber.Add(i);
                saveData.InvenItemName.Add(invenSlots[i].item.Name);
                saveData.InvenItemCount.Add(invenSlots[i].itemCount);
            }
        }
        foreach(var data in dict_Equip)
        {
            if(data.Value != null)
            {
                saveData.EquipArrayNumber.Add((int)data.Key);
                saveData.EquipItemName.Add(data.Value.Name);
            }
        }

        return saveData;
    }

    public void InventoryLoad()
    {
        equip.SettingSlot();
        inven.ResetAllSlots();


        if(Managers._data.invenData != null)
        {
            Inventorydata SaveData = Managers._data.invenData;
            int i = 0;
            for (; i < SaveData.InvenArrayNumber.Count; i++)
            {
                //인벤 로드
                inven.LoadToInven(SaveData.InvenArrayNumber[i], SaveData.InvenItemName[i], SaveData.InvenItemCount[i]);
            }

            for (i = 0; i < SaveData.EquipArrayNumber.Count; i++)
            {
                //장비 로드
                eEquipment index = (eEquipment)SaveData.EquipArrayNumber[i];
                equip.LoadToEquip(index, SaveData.EquipItemName[i]);
            }
        }

        dict_Equip = equip.GetEquipSlot();
    }

    #endregion [ Save & Load ]
}
