using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class clear_gold : MonoBehaviour
{
    save_sc save_sc_temp;
    public GameObject gold_obj;
    public bool more_gold = false;//±§∞Ì Ω√√ªΩ√ 
    public void gold_mechanism()
    {
        gold_obj = gameObject;
        save_sc_temp = save_sc.find_save_sc();
        float gold = (save_sc_temp.save_data.user_now_floor * (save_sc_temp.save_data.stage_monster_fight * 100)) * (save_sc_temp.find_upgrade_value(4)/100);
        if (more_gold)
        {
            if (gold == 0)
            {
                gold = 300;
            }
            else
            gold = gold * 3;
        }
        save_sc_temp.save_data.stage_monster_fight = 0;
        TextMeshProUGUI gold_text = gold_obj.GetComponent<TextMeshProUGUI>();
        gold_text.SetText("»πµÊ ∞ÒµÂ : "+gold+"+");
        save_sc_temp.save_data.user_now_gold += (int)gold;
    }
}
