using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_slot_seting : MonoBehaviour
{
    public item_slot[] item_slot_obj;//슬롯 5개 
    public item_data_base item_data_base_temp;//데이터베이스
    public save_sc save_sc_temp;
    public trun_manager trun_manger_temp;
    public int item_id_temp = 0;

    public bool pull_item_mounting = false;
    public bool change_item = false;//아이템 체인지 할때 

    public bool end_item_once = false;//끝나고 하나 가져가기
    public int chuse_slot_id = 0;//선택한 id

    void Start()
    {
        item_data_base_temp = gameObject.GetComponent<item_data_base>();
        save_sc_temp = gameObject.GetComponent<save_sc>();
        trun_manger_temp= gameObject.GetComponent<trun_manager>();
        Invoke("update_slot",0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rooting_item(int item_id)//아이템 번호 입력하면 장착 
    {
        item_id_temp = item_id;
        for (int i = 0; i < 5; i++)//중첩할거 있는지 찾는 문
        {
            if (save_sc_temp.save_data.user_have_item[i] == item_id)
            {
                
                if (save_sc_temp.save_data.user_have_item_mounting[i] < 3)
                {
                    save_sc_temp.save_data.user_have_item_mounting[i]++;
                    item_slot_obj[i].mounting_int = save_sc_temp.save_data.user_have_item_mounting[i];
                    save_sc_temp.save_data.bouse_stage_item[0] = 0;
                    save_sc_temp.save_data.bouse_stage_item[1] = 0;
                    save_sc_temp.save_data.bouse_stage_item[2] = 0;
                    StartCoroutine("end_item_drop");
                    return;
                }
                else
                {
                    pull_item_mounting = true;
                    return;
                }
            }
        }
        for (int i = 0; i < 5; i++)//중첩 없으면 자리 찾는 문 
        {
            if (save_sc_temp.save_data.user_have_item[i] == 0)
            {
                save_sc_temp.save_data.user_have_item[i] = item_id;
                save_sc_temp.save_data.user_have_item_mounting[i] = 1;
                item_slot_obj[i].item_data_temp = item_data_base_temp.return_find_item_data(item_id);
                item_slot_obj[i].mounting_int = 1;
                save_sc_temp.save_data.bouse_stage_item[0] = 0;
                save_sc_temp.save_data.bouse_stage_item[1] = 0;
                save_sc_temp.save_data.bouse_stage_item[2] = 0;
                StartCoroutine("end_item_drop");
                return;
            }
        }
        if (save_sc_temp.find_item(item_id) == 0)
        {
            change_item = true;
        }
    }
    public void end_item_get(int slot_id)
    {
        chuse_slot_id = slot_id;
        for (int i = 0; i < 5; i++)
        {
            if (i != slot_id)
            {
                save_sc_temp.save_data.user_have_item[i] = 0;
                save_sc_temp.save_data.user_have_item_mounting[i] = 0;
            }
        }
        save_sc_temp.save_data.user_have_item_mounting[slot_id] = 1;
        save_sc_temp.save_data.user_have_hp_percent = 1;
        save_sc_temp.save_data.user_hp = save_sc_temp.save_data.user_hp_max;
        save_sc_temp.save_data.user_mp = save_sc_temp.save_data.user_mp_max;
        save_sc_temp.save_data.user_now_floor = 0;
        save_sc_temp.save_data.user_now_stage = 0;
        save_sc_temp.save_data.fight_monster_id[0] = 0;
        save_sc_temp.save_data.fight_monster_id[1] = 0;
        save_sc_temp.save_data.fight_monster_id[2] = 0;
        save_sc_temp.save_data.heal_room_stack = 0;
        save_sc_temp.Save();
        trun_manger_temp.sound_temp.change_main();
        load_manager.LoadScene_fast("main_ui");
    }
    public void set_item_explanation(int slot_id)//슬롯 다시 설정 
    {
        change_item = false;
        save_sc_temp.save_data.user_have_item[slot_id] = item_id_temp;
        save_sc_temp.save_data.user_have_item_mounting[slot_id] = 1;
        item_slot_obj[slot_id].item_data_temp = item_data_base_temp.return_find_item_data(item_id_temp);
        item_slot_obj[slot_id].mounting_int = 1;
        StartCoroutine("end_item_drop");
    }
    void update_slot()
    {
        for (int i = 0; i < 5; i++)
        {
            if (save_sc_temp.save_data.user_have_item[i] != 0)
            {
                
                item_slot_obj[i].item_data_temp = item_data_base_temp.return_find_item_data(save_sc_temp.save_data.user_have_item[i]);
                item_slot_obj[i].mounting_int = save_sc_temp.save_data.user_have_item_mounting[i];
                item_slot_obj[i].check_img();
            }
        }
    }
    IEnumerator end_item_drop()
    {
        save_sc_temp.save_data.user_now_stage++;
        save_sc_temp.Save();
        yield return new WaitForSeconds(2f);
        load_manager.LoadScene_fast("in_game_1");
    }
}
