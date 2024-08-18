using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class monster_ui : MonoBehaviour
{
    //몬스터 정보 저장
    float monster_atck;
    float monster_hp;
    float monster_defend;
    float monster_atckspead;
    float monster_matck;
    Sprite monster_img;

    //몬스터 정보 보여줄 텍스트 이미지들
    public TextMeshProUGUI atck;
    public TextMeshProUGUI atckspead;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI defend;
    public TextMeshProUGUI matck;
    public Image img;

    public GameObject actvit_obj;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void set_monster_data(GameObject monster_temp)
    {
        on_data_ui();
        monster_data monster_temp_data = monster_temp.GetComponent<monster_data>();

        monster_atck = monster_temp_data.monster_atck;
        monster_hp = monster_temp_data.monster_hp;
        monster_defend = monster_temp_data.monster_defend;
        monster_atckspead = monster_temp_data.monster_atck_spead_cooltime;
        monster_matck = monster_temp_data.monster_matck;
        monster_img = monster_temp_data.monster_img;
        //설정 
        atck.SetText("공격력 : " + monster_atck);
        atckspead.SetText("공격 쿨타임 : " + monster_atckspead);
        hp.SetText("체력 : " + monster_hp);
        defend.SetText("방어력 : " + monster_defend);
        matck.SetText("마법력 : " + monster_matck);
        img.sprite = monster_img;
    }
    public void on_data_ui()
    {
        if (!trun_manager.game_stop)
        {
            actvit_obj.SetActive(true);
        }

    }
    public void off_data_ui()
    { 
        actvit_obj.SetActive(false);
    }
 
}
