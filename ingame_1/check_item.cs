using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class check_item : MonoBehaviour
{

    public GameObject heal_buttn_obj;//힐 버튼 오브젝트
    [SerializeField]
    float player_hp;
    float player_mp;
    [SerializeField]
    float player_hpmax;
    float player_mpmax;
    [SerializeField]
    hp_up_effect hp_Up_Effect;//이펙트 
    Image hp_img;
    Image mp_img;
    save_sc save_sc_temp;
    sound_manager sound_temp;
    // Start is called before the first frame update
    void Start()
    {
        save_sc_temp = save_sc.find_save_sc();
        Invoke("update_player_data", 0.2f);
        sound_temp = this.GetComponent<sound_manager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    public void update_player_data()//데이터 기본적으로 받아오고 
    {

        player_hpmax = save_sc_temp.save_data.user_hp_max;
        player_mpmax = save_sc_temp.save_data.user_mp_max;

        player_hp = player_hpmax * save_sc_temp.save_data.user_have_hp_percent;
        player_mp = player_mpmax * save_sc_temp.save_data.user_have_hp_percent;

        check_img();
    }
    public void reselt_mk()//쉬는곳 매커니즘 마나 소모 
    {
        heal_buttn_obj.SetActive(false);
        if (player_hp <= player_hpmax - player_hpmax / 2.5f)//체력이 최대 힐량을 받을정도 일시 
        {
            if (player_mp >= player_mpmax / 5f)//필요 마나량이 있을시 
            {
                float f_index = player_hpmax / (2.5f / ((player_mpmax / 5f) / (player_mpmax / 5f))); //쉬는곳 메커니즘
                player_hp += f_index;
                player_mp -= player_mpmax / 5f;
                hp_Up_Effect.heal_to_start(f_index);
                check_img();
            }
            else//부족할시 
            {
                float f_index = player_hpmax / (2.5f / (player_mp / (player_mpmax / 5f))); //쉬는곳 메커니즘
                player_hp += f_index;
                player_mp -= player_mp;
                hp_Up_Effect.heal_to_start(f_index);
                check_img();
            }
        }
        else//체력이 최대 힐량에 못 미치다.
        {
            if (player_mp >= (player_mpmax / 5f) * ((player_hpmax - player_hp) / (player_hpmax / 2.5f)))//필요 마나량이 있을시 
            {
                float f_h_index = (player_mpmax / 5f) * ((player_hpmax - player_hp) / (player_hpmax / 2.5f));
                float f_index_2 = player_hpmax / (2.5f / (f_h_index / (player_mpmax / 5f))); //쉬는곳 메커니즘
                player_hp += f_index_2;
                player_mp -= f_h_index;
                hp_Up_Effect.heal_to_start(f_index_2);
                check_img();
            }
            else//부족할시 
            {
                float f_h_index = player_mp * ((player_hpmax - player_hp) / (player_hpmax / 2.5f));
                float f_index_2 = player_hpmax / (2.5f / (f_h_index / (player_mpmax / 5f))); //쉬는곳 메커니즘
                player_hp += f_index_2;
                player_mp -= f_h_index;
                hp_Up_Effect.heal_to_start(f_index_2);
                check_img();
            }

        }
    }
        void check_img()
        {
            save_sc_temp.save_data.user_have_hp_percent = player_hp / player_hpmax;
            save_sc_temp.save_data.user_have_mp_percent = player_mp / player_mpmax;
        }
   public void next_stage()
   {
        save_sc_temp.save_data.user_now_stage++;
        save_sc_temp.Save();
        if (save_sc_temp.save_data.user_now_stage == 10)
        {
            sound_temp.change_mab_main();
        }
        load_manager.LoadScene_fast("in_game_1");
    }
 }

