using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Define;

public class UI_Information : UI_Base
{
    enum GameObjects
    {
        HP,
        MP,
        EXP,
        Level
    }

    //Image hp_img;
    //Image mp_img;
    //Image exp_img;
    //Text level_text;

    [SerializeField] Image hp_img;
    [SerializeField] Image mp_img;
    [SerializeField] Image exp_img;
    [SerializeField] Text level_text;

    PlayerStat ps;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        //Bind<GameObject>(typeof(GameObjects));
        //hp_img = GetObject((int)GameObjects.HP).GetComponent<Image>();
        //mp_img = GetObject((int)GameObjects.MP).GetComponent<Image>();
        //exp_img = GetObject((int)GameObjects.EXP).GetComponent<Image>();
        //level_text = GetObject((int)GameObjects.Level).GetComponent<Text>();

        StartCoroutine(SetUI());
    }

    void SettingUI()
    {
        if (ps == null)
            ps = PlayerCtrl._inst._stat;

        float hp = ps.HP / ps.MaxHP;
        float mp = ps.MP / ps.MaxMP;
        float exp = ps.EXP / ps.TotalEXP;

        level_text.text = ps.Level.ToString();
        hp_img.fillAmount = hp;
        mp_img.fillAmount = mp;
        exp_img.fillAmount = exp;
    }

    IEnumerator SetUI()
    {
        if (ps == null)
            ps = PlayerCtrl._inst._stat;
        while (true)
        {
            if (Managers._scene.CurrentScene.CurrScene != eScene.GameScene)
                break;
            float hp = ps.HP / ps.MaxHP;
            float mp = ps.MP / ps.MaxMP;
            float exp = ps.EXP / ps.TotalEXP;

            level_text.text = ps.Level.ToString();
            hp_img.fillAmount = hp;
            mp_img.fillAmount = mp;
            exp_img.fillAmount = exp;
            yield return null;
        }
    }
}
