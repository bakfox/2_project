using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_menu_manager : MonoBehaviour
{
    [SerializeField]
    GameObject[] move_objs;//오브젝트 움직일거
    [SerializeField]
    Vector3[] postion_purpose;//움직일 목표거리 0~2 목표 위치 3~5현재 위치 
    [SerializeField]
    ParticleSystem[] partcl_objs;//파티클  

    public GameObject restart_ui_obj;//이어하거나 물어보기 
    public GameObject gold_ui;//골드 ui
    public GameObject gold_more_ui;
    public clear_gold clear_gold_temp;

    bool restart_game_click_key = false;//다시시작할때 키 입력 받을 준비..
    float run_Time_max = 1f;
    // Start is called before the first frame update
    save_sc save_temp;

    public sound_manager sound_temp;
    void Start()
    {
        save_temp = save_sc.find_save_sc();
        partcl_objs[0].Stop();
        partcl_objs[1].Stop();
        StartCoroutine("move_obj");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            end_game();
        }
        if (restart_game_click_key)
        {
            if (Input.anyKeyDown)
            {
                restart_game_click_key = false;
                sound_temp.change_change_job_main();
                load_manager.LoadScene_fast("in_game_chuse_job");

            }
        }
    }
    IEnumerator move_obj()
    {
        yield return new WaitForSeconds(run_Time_max);
        Debug.Log("test_1");
        float run_time = 0.0f;
        while (run_time < run_Time_max)
        {
            run_time += Time.deltaTime;
            move_objs[0].transform.localPosition = Vector3.Lerp(postion_purpose[3], postion_purpose[0], run_time / run_Time_max);
            RectTransform rect_1_temp = move_objs[1].GetComponent<RectTransform>();
            RectTransform rect_2_temp = move_objs[2].GetComponent<RectTransform>();
            rect_1_temp.localPosition = Vector3.Lerp(postion_purpose[4], postion_purpose[1], run_time / run_Time_max);
            rect_2_temp.localPosition = Vector3.Lerp(postion_purpose[5], postion_purpose[2], run_time / run_Time_max);
            yield return new WaitForFixedUpdate();
        }
        run_time = 0f;
        while (run_time < run_Time_max)
        {
            run_time += Time.deltaTime;
            move_objs[3].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, (run_time * 255));
            yield return new WaitForFixedUpdate();
        }
        partcl_objs[0].Play();
        partcl_objs[1].Play();
        StopCoroutine("move_obj");
        yield return null;
    }//버튼 용도
    public void end_game()//끌때
    {
        Application.Quit();
    }
    public void start_game()
    {
        if (save_temp.save_data.first_game_start == true)
        {
            sound_temp.change_mab_main();
            load_manager.LoadScene_fast("in_game_tuto");
        }
        else if (save_temp.save_data.user_now_stage == 0)
        {
            save_temp.save_data.user_have_hp_percent = 1;
            save_temp.save_data.user_have_mp_percent = 1;
            save_temp.save_data.user_now_stage = 0;
            save_temp.save_data.user_now_floor = 1;
            save_temp.save_data.user_job = 0;
            save_temp.save_data.chuse_heal_stage = false;
            for (int i_1 = 0; i_1 < 3; i_1++)
            {
                save_temp.save_data.bouse_stage_item[i_1] = 0;
                save_temp.save_data.fight_monster_id[i_1] = 0;
            }
            save_temp.save_data.heal_room_stack = 0;
            save_temp.save_data.stage_monster_fight = 0;
            save_temp.save_data.chuse_stage_save = 0;
            save_temp.Save();
            sound_temp.change_change_job_main();
            load_manager.LoadScene_fast("in_game_chuse_job");
        }
        else    
        {
            restart_ui_obj.SetActive(true);
        }
            
    }
    public void continuing_game()//이어하기용
    {
        if (save_temp.save_data.bouse_stage_item[0] != 0)
        {
            sound_temp.change_mab_main();
            load_manager.LoadScene_fast("in_game_1_bonuse");
        }
        else if (save_temp.save_data.chuse_stage_save != 0)
        {
            sound_temp.change_mab_main();
            load_manager.LoadScene_fast("in_game_2");
        }
        else if (save_temp.save_data.chuse_heal_stage)
        {
            sound_temp.change_mab_main();
            load_manager.LoadScene_fast("in_game_3");
        }
        else if (save_temp.save_data.change_job_stage)
        {
            sound_temp.change_change_job_main();
            load_manager.LoadScene_fast("in_game_chuse_job");
        }
        else
        {
            sound_temp.change_mab_main();
            load_manager.LoadScene_fast("in_game_1");
        }
            

    }
    public void go_shop()
    {
        sound_temp.change_main();
        load_manager.LoadScene_fast("main_gacha");
    }
    public void restart()//새로하기용 
    {
        restart_ui_obj.SetActive(false);   
        gold_more_ui.SetActive(true);
    }
    public void gold_get()
    {
        gold_more_ui.SetActive(false);
        clear_gold_temp.gold_mechanism();
        save_temp.save_data.user_have_hp_percent = 1;
        save_temp.save_data.user_have_mp_percent = 1;

        save_temp.save_data.user_now_stage = 0;
        save_temp.save_data.user_now_floor = 1;

        save_temp.save_data.user_job = 0;
        for (int i = 0; i < 5; i++)
        {
            save_temp.save_data.user_have_item_mounting[i] = 0;
            save_temp.save_data.user_have_item[i] = 0;
        }
        save_temp.save_data.chuse_heal_stage = false;
        for (int i_1 = 0; i_1 < 3; i_1++)
        {
            save_temp.save_data.bouse_stage_item[i_1] = 0;
            save_temp.save_data.fight_monster_id[i_1] = 0;
        }
        save_temp.save_data.heal_room_stack = 0;
        save_temp.save_data.stage_monster_fight = 0;
        save_temp.save_data.chuse_stage_save = 0;
        save_temp.Save();
        gold_ui.SetActive(true);
        restart_game_click_key = true;
    }
}
