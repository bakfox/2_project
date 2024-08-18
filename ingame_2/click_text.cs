using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class click_text : MonoBehaviour , IPointerClickHandler
{
    public int scenes_index = 0;//0 다음스테이지 1 휴식 2 보상 
    public save_sc save_sc_temp;
    public sound_manager sound_temp;

    public async void OnPointerClick(PointerEventData eventData)
    {
        switch (scenes_index)
        {
            case 0:
                save_sc_temp.save_data.user_now_stage++;
                save_sc_temp.save_data.chuse_stage_save = 0;
                save_sc_temp.Save();
                if (save_sc_temp.save_data.user_now_stage == 10)
                {
                    sound_temp.change_mab_main();
                } 
                load_manager.LoadScene_fast("in_game_1");
                break;
            case 1:
                save_sc_temp.save_data.chuse_heal_stage = true;
                save_sc_temp.save_data.chuse_stage_save = 0;
                save_sc_temp.Save();
                load_manager.LoadScene_fast("in_game_3");
                break;
            case 2:
                save_sc_temp.save_data.chuse_stage_save = 0;
                save_sc_temp.Save();
                load_manager.LoadScene_fast("in_game_1_bonuse");
                break;
        }
    }
}
