using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stats : UI_Base
{
    [SerializeField] Text Level;
    [SerializeField] Text NowExp;
    [SerializeField] Text MaxExp;
    [SerializeField] Text NowHp;
    [SerializeField] Text MaxHp;
    [SerializeField] Text PlusHp;
    [SerializeField] Text NowMp;
    [SerializeField] Text MaxMp;
    [SerializeField] Text PlusMp;
    [SerializeField] Text Damage;
    [SerializeField] Text PlusDamage;
    [SerializeField] Text Defense;
    [SerializeField] Text PlusDefense;
    [SerializeField] Text Gold;

    PlayerStat ps;
    string _format;
    bool _isRunning = false;

    void Start()
    {
        Init();
        Managers._ui.OnSetUIEvent -= UIEvent;
        Managers._ui.OnSetUIEvent += UIEvent;
    }

    public override void Init()
    {
        _format = "{0:#,###}";
        
    }

    void UIEvent()
    {
        if(_isRunning != false)
        {
            StopCoroutine(OnSetUI());
            StartCoroutine(OnSetUI());
        }
        
    }

    IEnumerator OnSetUI()
    {
        ps = PlayerCtrl._inst._stat;
        _isRunning = true;
        while (UI_Inventory.ActivatedInventory)
        {
            float exp = ps.EXP;
            float totalexp = ps.TotalEXP;
            Level.text = string.Format(_format, ps.Level);
            if (exp > 0)
                NowExp.text = string.Format(_format, exp);
            else if (exp == 0)
                NowExp.text = "0";
            else
                NowExp.text = "Error";
            if (totalexp > 0)
                MaxExp.text = string.Format(_format, totalexp);
            else if (exp == 0)
                MaxExp.text = "0";
            else
                MaxExp.text = "Error";
            if (ps.HP > 0)
                NowHp.text = string.Format(_format, Mathf.Min(ps.HP, ps.MaxHP));
            else
                NowHp.text = "0";
            MaxHp.text = string.Format(_format, ps.MaxHP);
            if (ps.MP > 0)
                NowMp.text = string.Format(_format, Mathf.Min(ps.MP, ps.MaxMP));
            else
                NowMp.text = "0";
            MaxHp.text = string.Format(_format, ps.MaxMP);
            Damage.text = string.Format(_format, ps.Damage);
            Defense.text = string.Format(_format, ps.Defense);

            int value = 0;
            if (ps.PlusHP > 0)
            {
                value = Mathf.RoundToInt(ps.PlusHP);
                PlusHp.text = string.Format($"+{_format}", value);
                PlusHp.gameObject.SetActive(true);
            }
            else
            {
                PlusHp.text = "0";
                PlusHp.gameObject.SetActive(false);
            }
            if (ps.PlusMP > 0)
            {
                value = Mathf.RoundToInt(ps.PlusMP);
                PlusMp.text = string.Format($"+{_format}", value);
                PlusMp.gameObject.SetActive(true);
            }
            else
            {
                PlusMp.text = "0";
                PlusMp.gameObject.SetActive(false);
            }
            if (ps.PlusDamage > 0)
            {
                value = Mathf.RoundToInt(ps.PlusDamage);
                PlusDamage.text = string.Format($"+{_format}", value);
                PlusDamage.gameObject.SetActive(true);
            }
            else
            {
                PlusDamage.text = "0";
                PlusDamage.gameObject.SetActive(false);
            }
            if (ps.PlusDefense > 0)
            {
                value = Mathf.RoundToInt(ps.PlusDefense);
                PlusDefense.text = string.Format($"+{_format}", value);
                PlusDefense.gameObject.SetActive(true);
            }
            else
            {
                PlusDefense.text = "0";
                PlusDefense.gameObject.SetActive(false);
            }

            if (ps.Gold > 0)
                Gold.text = string.Format(_format, ps.Gold);
            else
                Gold.text= "0";
            yield return null;
        }
        _isRunning = false;
    }
}
