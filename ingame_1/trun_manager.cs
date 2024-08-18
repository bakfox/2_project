using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class trun_manager : MonoBehaviour
{
    public helper_main_ai help_ai_sc_temp;//도우미 메인 ai복사본

    public TextMeshProUGUI expect_dmg_monster;//몬스터가 때릴때 예상 데미지
    public TextMeshProUGUI expect_dmg_player;//플레이어가 때릴때 예상 데미지 

    public List<GameObject> atck_monster_order;//들어가는 순서대로 
    public GameObject atck_player_obj;//플레이어가 공격할때 obj
    public GameObject defend_player_obj;//몬스터가 공격할때 obj
    public GameObject[] monster_btn;//디팬드 유아이에서 몬스터 체크용
    public TextMeshProUGUI[] monster_hp_text;//몬스터 체력 
    int check_player_defend = 0;//막을건지 피할건지 체크 0그냥 맞음 1방어 2 회피 

   public TextMeshProUGUI special_count_text;//스패이셜 카운트 
   // public TextMeshProUGUI nomal_skil_text;//스페이셜 
    public TextMeshProUGUI stage_text_ui;//스테이지 text

    public int live_monster = 0;//몬스터 스폰시 증가 죽으면 감소.
    public GameObject[] monster_obj;//생성된 몬스터 모음 
    public GameObject stop_ui;//멈출때 ui

    public static bool game_stop = false;// 게임 멈추면 
    public GameObject player_obj;//플레이어 오브젝트 
    save_sc  save_sc_temp;

    public GameObject next_stage_ui;//다음 스테이즈 ui;
    public clear_gold clear_gold_temp;
    public GameObject end_gold_obj;//끝날때 골드 획득
    public GameObject end_ui;//끝날때 ui
    public GameObject end_select_item;//아이템 선택
    public GameObject end_btn_text_obj;//버튼 
    public item_slot_seting item_slot_seting_temp;//셋팅용 템프

    public int player_special_atck_cooltime = 0;//5번 때리면 발동 혹은 유물들로 줄일수 있따.
    int player_special_atkc_max = 5;

    public bool atck_order = false;// 몬스터가 공격할때 true로 변경
    public Image defend_ui;

    float wait_tiem_limt = 5f;
    float wait_time;

    public bool player_die = false;//죽으면 모든 행동 중지
    [SerializeField]
    bool check_redie = false;// 죽으며 true로 변환 다시 부활 방지

    public int atkc_Certain_monster_temp = 0;//몬스터 공격 가중치 //보스 전용으로 다른 메커니즘 넣어서 변경 

    monster_data m_temp;//현재 선택중인 몬스터 데이터 템프.

    public CinemachineFreeLook main_came;// 메인 캠 
    public CinemachineVirtualCamera atck_came;//공격 캠

    int help_ai_text_nomal = 0; // 인게임 셋팅 평소상황
    bool monster_atck_help_text = false;// 방어시 발동

    public bool tutorial_mode = false;//이거 켜지면 튜토리얼로 
    public bool tutorial_end_ui_check = false;//튜토리얼중 esc로 나가는거 도와주는
    tuto_manger tuto_manager_temp;//튜토 매니저 

    public bool gold_end_get = false;//끝나고 골드 받음
    // Start is called before the first frame update
    public sound_manager sound_temp;
    void Start()
    {
        save_sc_temp = save_sc.find_save_sc();
        if (!tutorial_mode)
        {
            player_obj = GameObject.FindGameObjectWithTag("Player");// 플레이어 태그
            StartCoroutine("start_corutin");
        }
    }
    IEnumerator start_corutin()
    {
        stage_text_ui.SetText(save_sc_temp.save_data.user_now_floor + " - " + save_sc_temp.save_data.user_now_stage);
        yield return new WaitForSeconds(1f);
        check_monster_help();
    }
    void check_monster_help()//나올때 까지 실행 
    {
        int i_max = 0;
        for (int i_temp = 0; i_temp < 3; i_temp++)
        {
            if (monster_obj[i_temp] != null)
            {
                i_max++;
            }
        }
        int i = Random.Range(0, i_max);
         help_monster_text_change(monster_obj[i].GetComponent<monster_data>().monster_id);

    }
    // Update is called once per frame
    void Update()
    {
        if (tutorial_mode)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (tutorial_end_ui_check = false)
                {
                    Resume();
                    tutorial_end_ui_check = true;
                }
                else
                {
                    Pause();
                    tutorial_end_ui_check = false;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (game_stop)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        if (gold_end_get)
        {
            if (Input.anyKeyDown)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (save_sc_temp.save_data.user_have_item[i] != 0)
                    {
                        Debug.Log(save_sc_temp.save_data.user_have_item[i]);
                        end_gold_obj.SetActive(false);
                        gold_end_get = false;
                        StartCoroutine("end_game_corutin");
                        break;
                    }
                    else if (i == 4)
                    {
                        if (save_sc_temp.save_data.user_have_item[i] == 0)
                        {
                            Debug.Log("왜 여기로옴?");
                            end_save_game();
                        }
                    }
                }
            }
            
        }
      
    }
    void FixedUpdate()//오직 몬스터 턴 체크를 위해서 사용 
    {
        if (!tutorial_mode)
        {
            check_player_die();


            if (!player_die)
            {
                if (!atck_order)
                {
                    if (atck_monster_order.Count != 0)
                    {
                        m_temp = atck_monster_order[0].GetComponent<monster_data>();
                        atck_monster_order.RemoveAt(0);
                        atck_monster_order.TrimExcess();
                        atck_order = true;
                        defend_ui_player();
                    }
                }
            }
            if (live_monster == 0)
            {
                next_stage_ui.SetActive(true);
            }

        }

    }
    public void tuto_monster_atck_trun(GameObject atck_obj)//튜토리얼 전용 공격 
    {
        atck_monster_order.Add(atck_obj);
        m_temp = atck_monster_order[0].GetComponent<monster_data>();
        atck_monster_order.RemoveAt(0);
        atck_monster_order.TrimExcess();
        atck_order = true;
        defend_ui_player();
    }
    public void monster_atck_trun(GameObject atck_obj)//몬스터 공격
    {
        atck_monster_order.Add(atck_obj);
    }
    public void defend_ui_player()//defend 화면 오픈 타이머 작동 
    {
        Debug.Log("방어 발동");
        if (m_temp.Monster_type == monster_data.Type.matck)// 예상 데미지 
        {
            expect_dmg_monster.SetText((m_temp.monster_matck * m_temp.atkc_Certain_check_monster(atkc_Certain_monster_temp)) / player_obj.GetComponent<player_status>().player_mdefend + ": " + "마법");
        }else
        expect_dmg_monster.SetText((m_temp.monster_atck * m_temp.atkc_Certain_check_monster(atkc_Certain_monster_temp)) / player_obj.GetComponent<player_status>().player_defend + ": "+"물리");

        defend_player_obj.SetActive(true);
        StartCoroutine("chose_defend_evasion");
    }
    public void dont_chose_defend()//선택 안함
    {
        defend_player_obj.SetActive(false);
        check_player_defend = 0;//선택 안함  
        StopCoroutine("chose_defend_evasion");
        m_temp.monster_statuse = 1;//공겨 모션으로 변경 

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
    }
    public void defend()//방어 50퍼 뎀감
    {
        defend_player_obj.SetActive(false);
        check_player_defend = 1;//막음
        StopCoroutine("chose_defend_evasion");
        m_temp.monster_statuse = 1;//공겨 모션으로 변경 

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
    }
    public void evasion( )//회피 일정확률로 데미지 무효화 플러스 궁 게이지 
    {
        defend_player_obj.SetActive(false);
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                check_player_defend = 0;//실패한거
                break;
            default:
                check_player_defend = 2;//회피한거 
                special_atck_count();
                break;
        }
        StopCoroutine("chose_defend_evasion");
        m_temp.monster_statuse = 1;//공겨 모션으로 변경 

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
    }
    IEnumerator chose_defend_evasion()
    {
        wait_time = wait_tiem_limt;
        Debug.Log("시작함");
        while (wait_time > 0.0f)
        {
            if (!game_stop)
            {
                wait_time -= Time.deltaTime;
            }
            defend_ui.fillAmount = wait_time / wait_tiem_limt;

            yield return new WaitForFixedUpdate();
        }
        dont_chose_defend();
    }
    public void special_atck_count()//카운트 차는거 매커니즘
    {
        if (player_special_atck_cooltime < player_special_atkc_max)
        {
            player_special_atck_cooltime++;
            special_count_text.SetText(player_special_atck_cooltime + " / "+ player_special_atkc_max);
        }
    }
    public void atck_player()//플레이어 공격시 발동
    {
        player_status player_st_temp = player_obj.GetComponent<player_status>();

        int[] type_temp = player_st_temp.player_atck_type(save_sc_temp.save_data.user_job);
        float[] maginf_temp = player_st_temp.player_atck_magnification(save_sc_temp.save_data.user_job);
        if (player_special_atck_cooltime == 5)
        {
            
            if (type_temp[1] == 0)
            {
                expect_dmg_player.SetText(player_st_temp.player_atck * maginf_temp[1]+ ": "+"물리" );
            }
            else
                expect_dmg_player.SetText(player_st_temp.player_matck * maginf_temp[1] +": "+"마법");
        }
        else 
        {
            if (type_temp[0] == 0)
            {
                expect_dmg_player.SetText(player_st_temp.player_atck * maginf_temp[0] + ": " + "물리");
            }
            else
                expect_dmg_player.SetText(player_st_temp.player_matck * maginf_temp[0] + ": " + "마법");
        }

        atck_player_obj.SetActive(true);

        for (int i_monster_length = 0; i_monster_length <3; i_monster_length++)
        {
            
            if (monster_obj[i_monster_length] == null)
            {
                monster_btn[i_monster_length].SetActive(false);
            }
        }
        
        for (int i_temp = 0; i_temp < monster_obj.Length; i_temp++)//
        {
            if (monster_obj[i_temp] != null)
            {
                monster_data monster_temp = monster_obj[i_temp].GetComponent<monster_data>();
                Debug.Log(monster_obj[0].name);
                monster_hp_text[i_temp].SetText("체력 : " + (monster_temp.monster_hp / monster_temp.monster_max_hp * 100) + " %");
                if (monster_temp.monster_death)
                {
                    monster_btn[i_temp].SetActive(false);
                }
            }
            else
                break;
        }
    }
    public void first_monster()//몬스터 선택지 1/2/3
    {
        GameObject atck_temp = player_obj.GetComponent<player_status>().atck();// 어택오브젝트 생성 후 
        atck_temp.GetComponent<atck_target>().target_obj = monster_obj[0];
        atck_player_obj.SetActive(false);
        if (!tutorial_mode)
        {
            atck_came.Follow = atck_temp.transform;
            main_came.Priority = -2;
        }

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
        if (player_special_atck_cooltime != 5)
        {
            special_atck_count();
        }
    }
    public void second_monster()
    {
        GameObject atck_temp = player_obj.GetComponent<player_status>().atck();// 어택오브젝트 생성 후 
        atck_temp.GetComponent<atck_target>().target_obj = monster_obj[1];
        atck_player_obj.SetActive(false);

        if (!tutorial_mode)
        {
            atck_came.Follow = atck_temp.transform;
            main_came.Priority = -2;
        }

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
        if (player_special_atck_cooltime != 5)
        {
            special_atck_count();
        }
    }
    public void third_monster()
    {
        GameObject atck_temp = player_obj.GetComponent<player_status>().atck();// 어택오브젝트 생성 후 
        atck_temp.GetComponent<atck_target>().target_obj = monster_obj[2];
        atck_player_obj.SetActive(false);

        if (!tutorial_mode)
        {
            atck_came.Follow = atck_temp.transform;
            main_came.Priority = -2;
        }

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
        if (player_special_atck_cooltime != 5)
        {
            special_atck_count();
        }
    }
    public void changer_main_came()
    {
        if (!tutorial_mode)
        {
            main_came.Priority = 0;
            atck_came.Follow = null;
        }
        
    }

    public float player_atck_mechanism()//물리딜 계산 
    {
        float atck_temp = 0;
        atck_temp = player_obj.GetComponent<player_status>().player_atck;
        return atck_temp;
    }
    public float player_matck_mechanism()//마법딜 계산
    {
        float matck_temp = 0;
        matck_temp = player_obj.GetComponent<player_status>().player_matck;
        return matck_temp;
    }
    // 끝나고 나서 ui관련 
    public void end_game()//hp mp 초기화 게임 끝나면 스테이지 초기화 
    {
        end_ui.SetActive(true);
        game_stop = true;
        tutorial_mode = true;
    }
    public void get_gold()
    {
        clear_gold_temp.gold_mechanism();
        end_ui.SetActive(false);
        end_gold_obj.SetActive(true);
        gold_end_get = true;
        
    }
    public void end_save_game()// 끝나고 저장
    {
        save_sc_temp.save_data.user_hp = save_sc_temp.save_data.user_hp_max;//hp 
        save_sc_temp.save_data.user_mp = save_sc_temp.save_data.user_mp_max;
        save_sc_temp.save_data.user_now_floor = 0;
        save_sc_temp.save_data.user_now_stage = 0;
        save_sc_temp.save_data.fight_monster_id[0] = 0;
        save_sc_temp.save_data.fight_monster_id[1] = 0;
        save_sc_temp.save_data.fight_monster_id[2] = 0;
        save_sc_temp.save_data.heal_room_stack = 0;
        save_sc_temp.Save();
        retun_menu();
    }
    IEnumerator end_game_corutin()//end 아이템 고르기 
    {
        yield return null;
        item_slot_seting_temp.end_item_once = true;
        end_select_item.SetActive(true);
        StopCoroutine("end_game_corutin");
    }
    public void next_stage()//다음스테이지 
    {
        player_status player_sc_temp = player_obj.GetComponent<player_status>();
        save_sc_temp.save_data.user_have_hp_percent = player_sc_temp.player_hp / player_sc_temp.player_hpmax;
        save_sc_temp.save_data.user_have_mp_percent = player_sc_temp.player_mp / player_sc_temp.player_mpmax;

        save_sc_temp.save_data.fight_monster_id[0] = 0;
        save_sc_temp.save_data.fight_monster_id[1] = 0;
        save_sc_temp.save_data.fight_monster_id[2] = 0;

        if (save_sc_temp.save_data.user_now_stage == 10)
        {
            switch (save_sc_temp.save_data.user_now_floor)
            {
                case 0:
                    save_sc_temp.save_data.user_now_floor = 1;
                    save_sc_temp.save_data.user_now_stage = 1;
                    save_sc_temp.Save();
                    break;
                case 1:
                    save_sc_temp.save_data.user_now_floor = 2;
                    save_sc_temp.save_data.user_now_stage = 0;
                    save_sc_temp.save_data.stage_monster_fight = save_sc_temp.save_data.stage_monster_fight * 2;
                    save_sc_temp.save_data.change_job_stage = true;
                    save_sc_temp.clear_floor_boos();
                    save_sc_temp.Save();
                    sound_temp.change_change_job_main();
                    load_manager.LoadScene_fast("in_game_chuse_job");
                    break;
                case 2:
                    save_sc_temp.save_data.user_now_floor = 3;
                    save_sc_temp.save_data.user_now_stage = 0;
                    save_sc_temp.save_data.stage_monster_fight = save_sc_temp.save_data.stage_monster_fight * 2;
                    save_sc_temp.clear_floor_boos();
                    save_sc_temp.Save();
                    load_manager.LoadScene_fast("in_game_making");
                    break;
            }
        }
        else
        {
            save_sc_temp.Save();
            load_manager.LoadScene_fast("in_game_2");
        }
    }

    public bool check_player_die()//플레이어 목숨 체크 
    {
        player_status player_temp = player_obj.GetComponent<player_status>();
        if (player_temp.player_hp <= 0)
        {
            if (check_redie == false)
            {
                switch (save_sc_temp.find_item(2))//목숨
                {
                    case 0:
                        end_game_player_die_seting();
                        return player_die;
                    case 1:
                        player_temp.player_hp = 0;
                        player_temp.player_hp += player_temp.player_hpmax * 10 / 100;
                        player_temp.resual_rection();
                        check_redie = true;
                        player_die = false;
                        return player_die;
                    case 2:
                        player_temp.player_hp = 0;
                        player_temp.player_hp += player_temp.player_hpmax * 20 / 100;
                        player_temp.resual_rection();
                        check_redie = true;
                        player_die = false;
                        return player_die;
                    case 3:
                        player_temp.player_hp = 0;
                        player_temp.player_hp += player_temp.player_hpmax * 30 / 100;
                        player_temp.resual_rection();
                        check_redie = true;
                        player_die = false;
                        return player_die;

                }
            }
            else
                end_game_player_die_seting();
        }
        else
            player_die = false;
        return player_die;
    }
    public void end_game_player_die_seting()
    {
        if (!player_die)
        {
            end_game();
        }
        player_die = true;

    }
    public void Resume()//계속하기
    {
        stop_ui.SetActive(false);
        if (!tutorial_mode)
        {
            game_stop = false;
        }   
    }

    public void Pause()//esc누르면
    {
        stop_ui.SetActive(true);
        if (!tutorial_mode)
        {
            game_stop = true;
        }

    }
    public void retun_menu()//메뉴로 돌아갈때 사용 
    {
        sound_temp.change_main();
        load_manager.LoadScene_fast("main_ui");
    }
    
    public void re_coltime_player()//플레이어 다시 쿨타임 돌리기
    {
        player_obj.GetComponent<player_status>().StartCoroutine("player_atck_cooltime") ;
    }
    public void re_coltime_monster()//몬스터 쿨타임 다시 데미지 계산 후 // 플레이어 데미지 계산 메커니즘 
    {
        float dmg_temp = 0;
        if (m_temp.Monster_type == monster_data.Type.atck)
        {
            dmg_temp = m_temp.monster_atck * m_temp.atkc_Certain_check_monster(atkc_Certain_monster_temp) / player_obj.GetComponent<player_status>().player_defend;
        }
        else
            dmg_temp = m_temp.monster_matck * m_temp.atkc_Certain_check_monster(atkc_Certain_monster_temp) / player_obj.GetComponent<player_status>().player_mdefend ;
        switch (check_player_defend)
        {
            case 0:
                dmg_temp = dmg_temp;
                break;//실패
            case 1:
                dmg_temp = dmg_temp / 2;
                break;//방어
            case 2:
                dmg_temp = 0;
                break;//회피
        }
        player_obj.GetComponent<player_status>().anim_hit();
        player_obj.GetComponent<player_status>().player_hp -= dmg_temp;
        player_obj.GetComponent<player_status>().anim_hit();
        atkc_Certain_monster_temp = 0;

        atck_order = false;//초기화 
        m_temp.set_corutine();
    }


    public void help_monster_text_change(int monster_id)//몬스터 관련 아이디 받아서 출력
    {
        if (save_sc_temp.save_data.user_now_stage == 10)
        {
            switch (monster_id)
            {
                case 1:
                    help_ai_sc_temp.set_text_and_anim("일정 공격 이후 강력한 공격을 할거야!", 1, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 2:
                    help_ai_sc_temp.set_text_and_anim("일정 시간마다 회복을 사용해!", 1, 1);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 3:
                    help_ai_sc_temp.set_text_and_anim("체력이 높은편이야!", 0, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
            }
        }
        else
        {
            switch (monster_id)
            {
                case 1:
                    help_ai_sc_temp.set_text_and_anim("약한 돌덩이야 너도 충분히 잡을 수 있어 !", 3, 3);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 2:
                    help_ai_sc_temp.set_text_and_anim("강력한 공격을 하는 버섯이야 조심해 !", 1, 3);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 3:
                    help_ai_sc_temp.set_text_and_anim("유령은 마방력이 높은 편이야 !", 0, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 4:
                    help_ai_sc_temp.set_text_and_anim("슬라임 때리면 반격하는 녀석이야 !", 2, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 5:
                    help_ai_sc_temp.set_text_and_anim("책은 마방력이 높아 마법이 걸려있어서 그런가 !", 2, 1);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 6:
                    help_ai_sc_temp.set_text_and_anim("물고기 녀석은 방어력이 높아 조심해 !", 3, 0);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 7:
                    help_ai_sc_temp.set_text_and_anim("해파리 녀석은 때리면 반격을 하니 조심해 !", 1, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 8:
                    help_ai_sc_temp.set_text_and_anim("공격하기 전에 잡아야 해 !", 1, 2);
                    help_ai_sc_temp.trun_off_helper();

                    break;
            }
        }
        
    }
}
