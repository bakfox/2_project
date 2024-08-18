using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tuto_manger : MonoBehaviour
{
    public GameObject[] player_obj_ui;//플레이어 관려 obj
    public GameObject test_monster;//몬스터 테스트용
    GameObject monster_obj_temp;
    public GameObject[] mab_obj;//맵 오브젝트

    public GameObject help_main_ai;
    public Vector3[] help_postion;//도우미 위치 
    
    public int tutorial_steck = 0;//이거 올라갈수록 발동 

    public bool atck_tuto = false;

    trun_manager trun_manager_temp;
    save_sc save_sc_temp;

    public sound_manager sound_temp;
    // Start is called before the first frame update
    void Start()
    {
        trun_manager_temp = gameObject.GetComponent<trun_manager>();
        check_tutorial_steck();
        save_sc_temp = save_sc.find_save_sc();
    }
    public void test_monster_spawn()
    {
        monster_obj_temp = Instantiate(test_monster) as GameObject;
        Transform monster_obj_tsf = monster_obj_temp.GetComponent<Transform>();
        monster_obj_tsf.position = new Vector3(4, -1.05f, -2.95f);
        trun_manager_temp.monster_obj[0] = monster_obj_temp;
        trun_manager_temp.live_monster++;
    }
    public void Update()
    {
        if (help_main_ai.GetComponent<helper_main_ai>().text_end)
        {
            if (Input.anyKeyDown)
            {
                if (!atck_tuto)
                {
                    tutorial_steck++;
                    check_tutorial_steck();
                }

            }
        }  
    }
    public void check_tutorial_steck()
    {
        switch (tutorial_steck)
        {
            case 0:
                tuto_1();
                break;
            case 1:
                tuto_2();
                break;
            case 2:
                tuto_3();
                break;
            case 3:
                tuto_4();
                break;
            case 4:
                tuto_5();
                break;
            case 5:
                tuto_6();
                break;
            case 6: 
                tuto_7();
                break;
            case 7:
                tuto_8();
                break;
            case 8:
                tuto_9();
                break;
            case 9:
                tuto_10();
                break;
            case 10:
                tuto_11();
                break;
            case 11:
                tuto_12();
                break;
            case 12:
                tuto_13();
                break;
            case 13:
                tuto_14();
                break;
            case 14:
                tuto_15();
                break;
            case 15:
                tuto_16();
                break;
            case 16:
                tuto_17();
                break;
            case 17:
                tuto_18();
                break;
            case 18:
                tuto_19();
                break;
            case 19:
                tuto_20();
                break;
            case 20:
                tuto_21();
                break;
            case 21:
                tuto_22();
                break;
            case 22:
                tuto_23();
                break;
            case 23:
                tuto_24();
                break;
            case 24:
                tuto_25();
                break;
            case 25:
                tuto_26();
                break;
            case 26:
                tuto_27();
                break;
            case 27:
                tuto_28();
                break;
            case 28:
                tuto_29();
                break;
            case 29:
                tuto_30();
                break;
            case 30:
                tuto_31();
                break;
            case 31:
                tuto_32();
                break;
            case 32:
                tuto_33();
                break;
            case 33:
                tuto_34();
                break;
            case 34:
                tuto_35();
                break;
            case 35:
                tuto_36();
                break;
            case 36:
                tuto_37();
                break;
            case 37:
                tuto_38();
                break;
            case 38:
                tuto_39();
                break;
            case 39:
                find_job();
                break;

        }
    }
    public void tuto_1()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("안녕 만나서 반가워 나는 너의 ..", 3, 1);
    }
    public void tuto_2()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("뭐라고 할까.. .", 1, 2);
    }
    public void tuto_3()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("도우미야 ! ", 2, 1);
    }
    public void tuto_4()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("너는 세상의 색깔을 되돌려 놓기 위해 이곳에 온 거야! ", 2, 4);
    }
    public void tuto_5()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("자 이제 너의 모습을 보여줄께! ", 3, 3);
    }
    public void tuto_6()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("얍..! ", 2, 4);
        trun_manager.game_stop = true;
        player_obj_ui[2].SetActive(true);
    }
    public void tuto_7()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("이곳에서의 너의 모습이야!  ", 1, 0);
    }
    public void tuto_8()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("그리고 여기의 모든 곳에서는 시간의 간섭을 받아!  ", 0, 0);
    }
    public void tuto_9()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("얍..! ", 2, 4);
        player_obj_ui[1].SetActive(true);
    }
    public void tuto_10()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("위에 시간이 보이지?  ", 1, 1);
    }
    public void tuto_11()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("이것이 너의 시간이야 ", 2 , 1);
    }
    public void tuto_12()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("이 시간이 다 지나면 너의 턴이와! ", 2, 4);
    }
    public void tuto_13()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("턴이 오면 너는 공격을 할 수 있어! ", 3, 3);
    }
    public void tuto_14()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("얍! ", 2, 4);
        player_obj_ui[0].SetActive(true);
    }
    public void tuto_15()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("저 게이지는 너의 체력과 마나를 나타내고 있어! ", 0, 4);
    }
    public void tuto_16()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("체력은 다 닳으면 ..! ", 1, 4);
    }
    public void tuto_17()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("일정량의 골드를 얻고 입구로 돌아가! ", 2, 2);
    }
    public void tuto_18()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("마나는 휴식을 취할 때 일정량 체력으로 변환할 수 있어! ", 0, 0);
    }
    public void tuto_19()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("다 닳아도 죽는 건 아니니까 걱정하지 마! ", 3, 4);
    }
    public void tuto_20()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("연습해 볼 몬스터가 필요한데..! ", 1, 1);
    }
    public void tuto_21()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("이 녀석이 좋겠군! ", 0, 1);
    }
    public void tuto_22()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("얍..! ", 2, 4);
        test_monster_spawn();
        mab_obj[0].SetActive(false);
        mab_obj[1].SetActive(true);
    }
    public void tuto_23()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("몬스터들도 마찬가지로 내부적인 쿨타임을 가지고 있어! ", 2, 1);

    }
    public void tuto_24()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("자 이제 공격을 한번 해볼까? ", 2, 1);
    }
    public void tuto_25()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("공격과 방어는 화면 중앙에 나오는 선택지를 선택하면 돼! ", 0, 3);
    }
    public void tuto_26()
    {
        atck_tuto = true;
        trun_manager_temp.atck_player();
    }
    public void tuto_27()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("잘했어! 공격에는 시간제한이 없으니까! 천천히 골라! ", 3, 4);
    }
    public void tuto_28()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("자 이제 방어를 해볼까? ", 2, 3);
    }
    public void tuto_29()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("몬스터가 공격을 해올 때는 총 세 가지의 선택지가 있어! ", 0, 3);
    }
    public void tuto_30()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("그냥 맞기 방어 회피가 있어! ", 3, 3);
    }
    public void tuto_31()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("방어는 1/2 대미지를 받고 ", 0, 3);
    }
    public void tuto_32()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("회피는 피하거나 모든 대미지를 받아 ", 2, 3);
    }
    public void tuto_33()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("방어 선택지는 10초라는 제한 시간이 있으니! 빨리 정해야 해! ", 1, 3);
    }
    public void tuto_34()
    {
        atck_tuto = true;
        trun_manager.game_stop = false;
        trun_manager_temp.tuto_monster_atck_trun(monster_obj_temp);
    }
    public void tuto_35()
    {
        trun_manager_temp.atck_player_obj.SetActive(false);
        trun_manager.game_stop = true;
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("잘했어 앞으로도 이렇게하면 충분해! ", 1 , 4);
        
    }
    public void tuto_36()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("튜토리얼은 이걸로 끝이야! ", 2, 1);
    }
    public void tuto_37()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("나 못 본다고 서운해하지 말고! ", 3, 2);
    }
    public void tuto_38()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("계속 뒤에서 알려줄꺼야! ", 2, 0);
    }
    public void tuto_39()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("그럼 이따봐! ", 3, 1);
    }
    public void find_job()//마지막 직업 고르로 가기
    {
        save_sc_temp.save_data.user_have_hp_percent = 1;
        save_sc_temp.save_data.user_have_mp_percent = 1;
        save_sc_temp.save_data.first_game_start = false;
        save_sc_temp.save_data.user_now_floor = 1;
        save_sc_temp.save_data.user_now_stage = 0;
        save_sc_temp.Save();
        sound_temp.change_change_job_main();
        load_manager.LoadScene_fast("in_game_chuse_job");
    }
}
