using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class text_spawn : MonoBehaviour
{
    public TextMeshProUGUI stage_text_texxt;
    public GameObject text_obj_prefab;

    text_string text_string_cs;

    public GameObject[] text_obj_list; //텍스트 오브젝트 
    save_sc save_sc_temp;
    // Start is called before the first frame update
    void Start()
    {
        save_sc_temp = save_sc.find_save_sc();
        text_string_cs = this.gameObject.GetComponent<text_string>();
        stage_select();
        make_text_frist();  
    }

    void make_text_frist()//랜덤으로 3개의 선택지가 정해짐
    {
        int i = 0;
        if (save_sc_temp.save_data.user_now_stage != 9)
        {
            
            if (save_sc_temp.save_data.chuse_stage_save == 0)
            {
                i = (random_sc.random_gacha(3)+1);
                if (i <= 1 )
                {
                    save_sc_temp.save_data.heal_room_stack += 1;//스택 증가  
                }
                else
                {
                    save_sc_temp.save_data.heal_room_stack = 0;//나오면 초기화
                }

                if (save_sc_temp.save_data.heal_room_stack == 4)//4번 천장 기능
                {
                    i = 2;//2로 고정 
                    save_sc_temp.save_data.heal_room_stack = 0;//나오면 초기화
                }
                save_sc_temp.save_data.chuse_stage_save = i;
                save_sc_temp.Save();
            }
            else// 세이브 데이터에 있으면 실행 
                i = save_sc_temp.save_data.chuse_stage_save;
        }
        else//보스방 고정 
        {
            i = 1;
        }
        

        for (int i_1 = 0; i_1 < i;i_1++)//
        {
            GameObject text_obj = Instantiate(text_obj_prefab) as GameObject;
            click_text text_obj_click_sc = text_obj.GetComponent<click_text>();

            text_obj_click_sc.save_sc_temp = save_sc_temp;//스폰에 있는 save 템프 사용 
            text_obj_click_sc.scenes_index = i_1;
            text_obj_click_sc.sound_temp = gameObject.GetComponent<sound_manager>();

            RectTransform text_obj_rect = text_obj.GetComponent<RectTransform>();
            TextMeshProUGUI text_textmesh = text_obj.gameObject.GetComponentInChildren<TextMeshProUGUI>();

            text_obj.name = "text_obj_" + i_1;
            text_obj.transform.parent = GameObject.FindWithTag("text_spawn").transform;

            text_obj_rect.localPosition = new Vector3(0, (400 - 200 * (i_1 + 1)), 0);
            string[] string_temp = text_string_cs.find_stage_string();
            text_textmesh.SetText(string_temp[i_1]);
            text_obj_list[i_1] = text_obj;
        }
    }
    void stage_select()
    {
        stage_text_texxt.SetText("-"+save_sc_temp.save_data.user_now_stage+"번째 방입니다."+"-");
    }
}
