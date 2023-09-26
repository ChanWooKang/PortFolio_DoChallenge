using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    #region [ Enums ]
    public enum eCursor
    {
        Default = 0,
    }

    public enum eScene
    {
        Unknown,
        MainScene   = 0,
        GameScene   = 1,
    }

    public enum eSound
    {
        Unknown,
        BGM,
        SFX
    }

    public enum eLayer
    {
        UI      = 5,
        Ground  = 6,
        Player  = 7,
        Block   = 8,
        Disable = 9,
        Monster = 10,
        Item    = 11
    }

    public enum eTag
    {
        Ground,
        Player,
        Floor,
        Interact
    }

    public enum MouseEvent 
    {
        Click,
        Press,
        PointerDown,
        PointerUp,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        DragBegin,
        DragEnd,
        Drop,
    }

    public enum CameraMode
    {
        Quater,
    }

    public enum PoolType
    {
        Monster,
        RootItem,
        Effect
    }

    public enum eInteract
    {
        Unknown = 0,
        Item,
    }

    public enum eStat
    {
        HP  = 0,
        MP,
        Damage,
        Defense,
        Max_Cnt
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Die
    }

    public enum eSkill
    {
        Unknown = 0,
        Slash,
        Heal,
        
        Dodge,
    }

    public enum eMonster
    {
        Unknown = 0,
        Cactus,
        Mushroom,
        Slime,
        TurtleShell,
        Max_Cnt
    }

    public enum eCombo
    {
        Hit1,
        Hit2
    }

    public enum eItem
    {
        Unknown = 0,
        Gold,
        Equipment,
        Potion,
        Odd,
    }

    public enum eEquipment
    {
        Helm    = 0,
        Chest,
        Arm,
        Leg,
        Shield,
        Weapon,
        Max_Cnt
    }

    #endregion [ Enums ]

    #region [ Interface ]

    public interface ILoader<Key, Value>
    {
        Dictionary<Key, Value> Make();
    }

    public interface IFSMState<T>
    {
        void Enter(T m);
        void Execute(T m);
        void Exit(T m);
    }

    #endregion [ Interface ]

    #region [ Struct ]

    [System.Serializable]
    public struct STAT 
    {
        public eStat statType;
        public string statName;
        public string descName;
        public float sValue;
    }

    #endregion [ Struct ]

    #region [ Class ]

    [System.Serializable]
    public class PoolUnit
    {
        public string name;
        public PoolType type;
        public GameObject prefab;
        public int amount;
        int curAmount;
        public int CurAmount { get { return curAmount; } set { curAmount = value; } }
    }

    [System.Serializable]
    public class ItemWithWeight
    {
        // SOItem

        public int weight;
    }

    #endregion [ Class ]
}
