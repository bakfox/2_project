using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class trun_manager : MonoBehaviour
{
    public helper_main_ai help_ai_sc_temp;//����� ���� ai���纻

    public TextMeshProUGUI expect_dmg_monster;//���Ͱ� ������ ���� ������
    public TextMeshProUGUI expect_dmg_player;//�÷��̾ ������ ���� ������ 

    public List<GameObject> atck_monster_order;//���� ������� 
    public GameObject atck_player_obj;//�÷��̾ �����Ҷ� obj
    public GameObject defend_player_obj;//���Ͱ� �����Ҷ� obj
    public GameObject[] monster_btn;//���ҵ� �����̿��� ���� üũ��
    public TextMeshProUGUI[] monster_hp_text;//���� ü�� 
    int check_player_defend = 0;//�������� ���Ұ��� üũ 0�׳� ���� 1��� 2 ȸ�� 

   public TextMeshProUGUI special_count_text;//�����̼� ī��Ʈ 
   // public TextMeshProUGUI nomal_skil_text;//�����̼� 
    public TextMeshProUGUI stage_text_ui;//�������� text

    public int live_monster = 0;//���� ������ ���� ������ ����.
    public GameObject[] monster_obj;//������ ���� ���� 
    public GameObject stop_ui;//���⶧ ui

    public static bool game_stop = false;// ���� ���߸� 
    public GameObject player_obj;//�÷��̾� ������Ʈ 
    save_sc  save_sc_temp;

    public GameObject next_stage_ui;//���� �������� ui;
    public clear_gold clear_gold_temp;
    public GameObject end_gold_obj;//������ ��� ȹ��
    public GameObject end_ui;//������ ui
    public GameObject end_select_item;//������ ����
    public GameObject end_btn_text_obj;//��ư 
    public item_slot_seting item_slot_seting_temp;//���ÿ� ����

    public int player_special_atck_cooltime = 0;//5�� ������ �ߵ� Ȥ�� ������� ���ϼ� �ֵ�.
    int player_special_atkc_max = 5;

    public bool atck_order = false;// ���Ͱ� �����Ҷ� true�� ����
    public Image defend_ui;

    float wait_tiem_limt = 5f;
    float wait_time;

    public bool player_die = false;//������ ��� �ൿ ����
    [SerializeField]
    bool check_redie = false;// ������ true�� ��ȯ �ٽ� ��Ȱ ����

    public int atkc_Certain_monster_temp = 0;//���� ���� ����ġ //���� �������� �ٸ� ��Ŀ���� �־ ���� 

    monster_data m_temp;//���� �������� ���� ������ ����.

    public CinemachineFreeLook main_came;// ���� ķ 
    public CinemachineVirtualCamera atck_came;//���� ķ

    int help_ai_text_nomal = 0; // �ΰ��� ���� ��һ�Ȳ
    bool monster_atck_help_text = false;// ���� �ߵ�

    public bool tutorial_mode = false;//�̰� ������ Ʃ�丮��� 
    public bool tutorial_end_ui_check = false;//Ʃ�丮���� esc�� �����°� �����ִ�
    tuto_manger tuto_manager_temp;//Ʃ�� �Ŵ��� 

    public bool gold_end_get = false;//������ ��� ����
    // Start is called before the first frame update
    public sound_manager sound_temp;
    void Start()
    {
        save_sc_temp = save_sc.find_save_sc();
        if (!tutorial_mode)
        {
            player_obj = GameObject.FindGameObjectWithTag("Player");// �÷��̾� �±�
            StartCoroutine("start_corutin");
        }
    }
    IEnumerator start_corutin()
    {
        stage_text_ui.SetText(save_sc_temp.save_data.user_now_floor + " - " + save_sc_temp.save_data.user_now_stage);
        yield return new WaitForSeconds(1f);
        check_monster_help();
    }
    void check_monster_help()//���ö� ���� ���� 
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
                            Debug.Log("�� ����ο�?");
                            end_save_game();
                        }
                    }
                }
            }
            
        }
      
    }
    void FixedUpdate()//���� ���� �� üũ�� ���ؼ� ��� 
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
    public void tuto_monster_atck_trun(GameObject atck_obj)//Ʃ�丮�� ���� ���� 
    {
        atck_monster_order.Add(atck_obj);
        m_temp = atck_monster_order[0].GetComponent<monster_data>();
        atck_monster_order.RemoveAt(0);
        atck_monster_order.TrimExcess();
        atck_order = true;
        defend_ui_player();
    }
    public void monster_atck_trun(GameObject atck_obj)//���� ����
    {
        atck_monster_order.Add(atck_obj);
    }
    public void defend_ui_player()//defend ȭ�� ���� Ÿ�̸� �۵� 
    {
        Debug.Log("��� �ߵ�");
        if (m_temp.Monster_type == monster_data.Type.matck)// ���� ������ 
        {
            expect_dmg_monster.SetText((m_temp.monster_matck * m_temp.atkc_Certain_check_monster(atkc_Certain_monster_temp)) / player_obj.GetComponent<player_status>().player_mdefend + ": " + "����");
        }else
        expect_dmg_monster.SetText((m_temp.monster_atck * m_temp.atkc_Certain_check_monster(atkc_Certain_monster_temp)) / player_obj.GetComponent<player_status>().player_defend + ": "+"����");

        defend_player_obj.SetActive(true);
        StartCoroutine("chose_defend_evasion");
    }
    public void dont_chose_defend()//���� ����
    {
        defend_player_obj.SetActive(false);
        check_player_defend = 0;//���� ����  
        StopCoroutine("chose_defend_evasion");
        m_temp.monster_statuse = 1;//���� ������� ���� 

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
    }
    public void defend()//��� 50�� ����
    {
        defend_player_obj.SetActive(false);
        check_player_defend = 1;//����
        StopCoroutine("chose_defend_evasion");
        m_temp.monster_statuse = 1;//���� ������� ���� 

        if (tutorial_mode)
        {
            tuto_manager_temp = gameObject.GetComponent<tuto_manger>();
            tuto_manager_temp.atck_tuto = false;
            tuto_manager_temp.tutorial_steck++;
            tuto_manager_temp.check_tutorial_steck();
        }
    }
    public void evasion( )//ȸ�� ����Ȯ���� ������ ��ȿȭ �÷��� �� ������ 
    {
        defend_player_obj.SetActive(false);
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                check_player_defend = 0;//�����Ѱ�
                break;
            default:
                check_player_defend = 2;//ȸ���Ѱ� 
                special_atck_count();
                break;
        }
        StopCoroutine("chose_defend_evasion");
        m_temp.monster_statuse = 1;//���� ������� ���� 

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
        Debug.Log("������");
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
    public void special_atck_count()//ī��Ʈ ���°� ��Ŀ����
    {
        if (player_special_atck_cooltime < player_special_atkc_max)
        {
            player_special_atck_cooltime++;
            special_count_text.SetText(player_special_atck_cooltime + " / "+ player_special_atkc_max);
        }
    }
    public void atck_player()//�÷��̾� ���ݽ� �ߵ�
    {
        player_status player_st_temp = player_obj.GetComponent<player_status>();

        int[] type_temp = player_st_temp.player_atck_type(save_sc_temp.save_data.user_job);
        float[] maginf_temp = player_st_temp.player_atck_magnification(save_sc_temp.save_data.user_job);
        if (player_special_atck_cooltime == 5)
        {
            
            if (type_temp[1] == 0)
            {
                expect_dmg_player.SetText(player_st_temp.player_atck * maginf_temp[1]+ ": "+"����" );
            }
            else
                expect_dmg_player.SetText(player_st_temp.player_matck * maginf_temp[1] +": "+"����");
        }
        else 
        {
            if (type_temp[0] == 0)
            {
                expect_dmg_player.SetText(player_st_temp.player_atck * maginf_temp[0] + ": " + "����");
            }
            else
                expect_dmg_player.SetText(player_st_temp.player_matck * maginf_temp[0] + ": " + "����");
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
                monster_hp_text[i_temp].SetText("ü�� : " + (monster_temp.monster_hp / monster_temp.monster_max_hp * 100) + " %");
                if (monster_temp.monster_death)
                {
                    monster_btn[i_temp].SetActive(false);
                }
            }
            else
                break;
        }
    }
    public void first_monster()//���� ������ 1/2/3
    {
        GameObject atck_temp = player_obj.GetComponent<player_status>().atck();// ���ÿ�����Ʈ ���� �� 
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
        GameObject atck_temp = player_obj.GetComponent<player_status>().atck();// ���ÿ�����Ʈ ���� �� 
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
        GameObject atck_temp = player_obj.GetComponent<player_status>().atck();// ���ÿ�����Ʈ ���� �� 
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

    public float player_atck_mechanism()//������ ��� 
    {
        float atck_temp = 0;
        atck_temp = player_obj.GetComponent<player_status>().player_atck;
        return atck_temp;
    }
    public float player_matck_mechanism()//������ ���
    {
        float matck_temp = 0;
        matck_temp = player_obj.GetComponent<player_status>().player_matck;
        return matck_temp;
    }
    // ������ ���� ui���� 
    public void end_game()//hp mp �ʱ�ȭ ���� ������ �������� �ʱ�ȭ 
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
    public void end_save_game()// ������ ����
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
    IEnumerator end_game_corutin()//end ������ ���� 
    {
        yield return null;
        item_slot_seting_temp.end_item_once = true;
        end_select_item.SetActive(true);
        StopCoroutine("end_game_corutin");
    }
    public void next_stage()//������������ 
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

    public bool check_player_die()//�÷��̾� ��� üũ 
    {
        player_status player_temp = player_obj.GetComponent<player_status>();
        if (player_temp.player_hp <= 0)
        {
            if (check_redie == false)
            {
                switch (save_sc_temp.find_item(2))//���
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
    public void Resume()//����ϱ�
    {
        stop_ui.SetActive(false);
        if (!tutorial_mode)
        {
            game_stop = false;
        }   
    }

    public void Pause()//esc������
    {
        stop_ui.SetActive(true);
        if (!tutorial_mode)
        {
            game_stop = true;
        }

    }
    public void retun_menu()//�޴��� ���ư��� ��� 
    {
        sound_temp.change_main();
        load_manager.LoadScene_fast("main_ui");
    }
    
    public void re_coltime_player()//�÷��̾� �ٽ� ��Ÿ�� ������
    {
        player_obj.GetComponent<player_status>().StartCoroutine("player_atck_cooltime") ;
    }
    public void re_coltime_monster()//���� ��Ÿ�� �ٽ� ������ ��� �� // �÷��̾� ������ ��� ��Ŀ���� 
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
                break;//����
            case 1:
                dmg_temp = dmg_temp / 2;
                break;//���
            case 2:
                dmg_temp = 0;
                break;//ȸ��
        }
        player_obj.GetComponent<player_status>().anim_hit();
        player_obj.GetComponent<player_status>().player_hp -= dmg_temp;
        player_obj.GetComponent<player_status>().anim_hit();
        atkc_Certain_monster_temp = 0;

        atck_order = false;//�ʱ�ȭ 
        m_temp.set_corutine();
    }


    public void help_monster_text_change(int monster_id)//���� ���� ���̵� �޾Ƽ� ���
    {
        if (save_sc_temp.save_data.user_now_stage == 10)
        {
            switch (monster_id)
            {
                case 1:
                    help_ai_sc_temp.set_text_and_anim("���� ���� ���� ������ ������ �Ұž�!", 1, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 2:
                    help_ai_sc_temp.set_text_and_anim("���� �ð����� ȸ���� �����!", 1, 1);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 3:
                    help_ai_sc_temp.set_text_and_anim("ü���� �������̾�!", 0, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
            }
        }
        else
        {
            switch (monster_id)
            {
                case 1:
                    help_ai_sc_temp.set_text_and_anim("���� �����̾� �ʵ� ����� ���� �� �־� !", 3, 3);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 2:
                    help_ai_sc_temp.set_text_and_anim("������ ������ �ϴ� �����̾� ������ !", 1, 3);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 3:
                    help_ai_sc_temp.set_text_and_anim("������ ������� ���� ���̾� !", 0, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 4:
                    help_ai_sc_temp.set_text_and_anim("������ ������ �ݰ��ϴ� �༮�̾� !", 2, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 5:
                    help_ai_sc_temp.set_text_and_anim("å�� ������� ���� ������ �ɷ��־ �׷��� !", 2, 1);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 6:
                    help_ai_sc_temp.set_text_and_anim("����� �༮�� ������ ���� ������ !", 3, 0);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 7:
                    help_ai_sc_temp.set_text_and_anim("���ĸ� �༮�� ������ �ݰ��� �ϴ� ������ !", 1, 4);
                    help_ai_sc_temp.trun_off_helper();
                    break;
                case 8:
                    help_ai_sc_temp.set_text_and_anim("�����ϱ� ���� ��ƾ� �� !", 1, 2);
                    help_ai_sc_temp.trun_off_helper();

                    break;
            }
        }
        
    }
}
