using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class player_status : MonoBehaviour
{
    public float player_job;//플레이어 직업
    public RuntimeAnimatorController[] player_anim;//애니메이션들 
    public GameObject[] player_atck_set;//공격 모션 이펙트들
    public GameObject[] player_atck_sp_set;//궁국기 이펙트

    public GameObject player_hpmax_obj;// 오브젝트들
    public GameObject player_hp_obj;
    public GameObject player_mpmax_obj;
    public GameObject player_mp_obj;
    public TextMeshProUGUI player_hp_text;//text
    public TextMeshProUGUI player_mp_text;
    public GameObject resurrection_effect_obj;//부활 이펙트 오브젝트

    public hp_up_effect hp_effect_sc;//이펙트 용도.
    public mp_up_effect mp_effect_sc;

    public Sprite[] player_sp;
    Animator player_animator;

    //저장용 정보가 아님.
    //플레이어 정보 
    public float player_atck_spead;
    public float player_hp;
    public float player_hpmax;
    public float player_mp;
    public float player_mpmax;
    public float player_atck;
    public float player_matck;
    public float player_defend;
    public float player_mdefend;

    //public bool player_die_fals = false;//죽으면 true로

    save_sc save_sc_temp;
    trun_manager trun_manager_temp;

    //쿨타임
    [SerializeField]
    float player_cooltime;
    public Image player_cooltime_img;
    public TextMeshProUGUI player_cooltime_text;

    //체력 마나 ui
    Image hp_img;
    Image mp_img;
    RectTransform hpmax_tr;
    RectTransform hp_tr;
    RectTransform mpmax_tr;
    RectTransform mp_tr;
    // Start is called before the first frame update
    void Start()
    {
        update_player_data();
        start_cooltime();
        if (!trun_manager_temp.tutorial_mode)
        {
            StartCoroutine("player_atck_cooltime");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //player_die_fals = trun_manager_temp.check_player_die();
    }
    #region 처음에 받아오는것
    public void change_job_sprite()
    {
        player_animator = gameObject.GetComponent<Animator>();
        switch (player_job)
        {
            case 0:
                //gameObject.GetComponent<SpriteRenderer>().sprite = player_sp[0];
                player_animator.runtimeAnimatorController = player_anim[0];
                break;
            case 1:
                //gameObject.GetComponent<SpriteRenderer>().sprite = player_sp[1];
                player_animator.runtimeAnimatorController = player_anim[1];
                break;
            case 2:
                //gameObject.GetComponent<SpriteRenderer>().sprite = player_sp[2];
                player_animator.runtimeAnimatorController = player_anim[2];
                break;
        }
    }
    public void update_player_data()//기본 정보 받아오기 
    {
        save_sc_temp = save_sc.find_save_sc();
        trun_manager_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<trun_manager>();
        if (hp_img == null)
        {
            hp_img = player_hp_obj.GetComponent<Image>();
            mp_img = player_mp_obj.GetComponent<Image>();

            hpmax_tr = player_hpmax_obj.GetComponent<RectTransform>();
            hp_tr = player_hp_obj.GetComponent<RectTransform>();
            mpmax_tr = player_mpmax_obj.GetComponent<RectTransform>();
            mp_tr = player_mp_obj.GetComponent<RectTransform>();
        }
        switch (save_sc_temp.find_item(7))//최대 체력 증가//최대 체력만 따로 먼저 확인
        {
            case 0:
                player_hpmax += 0;
                break;
            case 1:
                player_hpmax += player_hpmax * 20 / 100;
                break;
            case 2:
                player_hpmax += player_hpmax * 35 / 100;
                break;
            case 3:
                player_hpmax += player_hpmax * 50 / 100;
                break;
        }
        player_job = save_sc_temp.save_data.user_job;
        player_hpmax = save_sc_temp.find_upgrade_value(3);
        player_mpmax = save_sc_temp.save_data.user_mp_max;
        player_hp = player_hpmax * save_sc_temp.save_data.user_have_hp_percent; 
        player_mp = player_mpmax * save_sc_temp.save_data.user_have_hp_percent;
        player_atck = save_sc_temp.find_upgrade_value(0);
        player_defend = save_sc_temp.find_upgrade_value(1);
        player_matck = save_sc_temp.find_upgrade_value(2);
        player_mdefend = save_sc_temp.find_upgrade_value(5);
        player_atck_spead = save_sc_temp.save_data.player_atck_spead_cooltime;
        Debug.Log(player_hpmax);
        change_job_sprite();
        //아이템 장착 체크 
        switch (save_sc_temp.find_item(1))//공격력 증가
        {
            case 0:
                player_atck += 0;
            break;
            case 1:
                player_atck += player_atck * 20 / 100;
                break;
            case 2:
                player_atck += player_atck * 35 / 100;
                break;
            case 3:
                player_atck += player_atck * 50 / 100;
                break;
        }
        switch (save_sc_temp.find_item(3))//방어력 증가
        {
            case 0:
                player_defend += 0;
                break;
            case 1:
                player_defend += player_defend * 20 / 100;
                break;
            case 2:
                player_defend += player_defend * 35 / 100;
                break;
            case 3:
                player_defend += player_defend * 50 / 100;
                break;
        }
        
        switch (save_sc_temp.find_item(7))//마법 방어력 증가
        {
            case 0:
                player_mdefend += 0;
                break;
            case 1:
                player_mdefend += player_mdefend * 20 / 100;
                break;
            case 2:
                player_mdefend += player_mdefend * 35 / 100;
                break;
            case 3:
                player_mdefend += player_mdefend * 50 / 100;
                break;
        }
        switch (save_sc_temp.find_item(9))//공격 쿨타임
        {
            case 0:
                player_atck_spead -= 0;
                break;
            case 1:
                player_atck_spead -= player_atck_spead * 10 / 100;
                break;
            case 2:
                player_atck_spead -= player_atck_spead * 20 / 100;
                break;
            case 3:
                player_atck_spead -= player_atck_spead * 40 / 100;
                break;
        }
        switch (save_sc_temp.find_item(5))//체력회복
        {
            case 0:
                player_hp += 0;
                break;
            case 1:
                player_hp += player_hpmax * 10 / 100;
                hp_effect_sc.heal_to_start(player_hp * 10 / 100);
                if (player_hp > player_hpmax)
                {
                    Debug.Log("힐됨");
                    player_hp = player_hpmax;
                }
                break;
            case 2:
                player_hp += player_hpmax * 20 / 100;
                hp_effect_sc.heal_to_start(player_hp * 20 / 100);
                if (player_hp > player_hpmax)
                {
                    player_hp = player_hpmax;
                }
                break;
            case 3:
                player_hp += player_hpmax * 30 / 100;
                hp_effect_sc.heal_to_start(player_hp * 30 / 100);
                if (player_hp > player_hpmax)
                {
                    player_hp = player_hpmax;
                }
                break;
        }
        switch (save_sc_temp.find_item(10))//마나회복
        {
            case 0:
                player_mp += 0;
                break;
            case 1:
                player_mp += player_mpmax * 10 / 100;
                mp_effect_sc.heal_to_start(player_mp * 10f / 100f);
                if (player_mp > player_mpmax)
                {
                    player_mp = player_mpmax;
                }
                break;
            case 2:
                player_mp += player_mpmax * 20 / 100;
                mp_effect_sc.heal_to_start(player_mp * 20f / 100f);
                if (player_mp > player_mpmax)
                {
                    player_mp = player_mpmax;
                }

                break;
            case 3:
                player_mp += player_mpmax * 30 / 100;
                mp_effect_sc.heal_to_start(player_mp * 30f / 100f);
                if (player_mp > player_mpmax)
                {
                    player_mp = player_mpmax;
                }
                break;
        }
        //길이의 비례해서 옆쪽으로 길게 
        hpmax_tr.sizeDelta = new Vector2(275 + player_hpmax * 0.05f, 40f);
        hp_tr.sizeDelta = new Vector2(275 + player_hpmax * 0.05f, 40f);
        mpmax_tr.sizeDelta = new Vector2(275 + player_mpmax * 0.05f, 40f);
        mp_tr.sizeDelta = new Vector2(275 + player_mpmax * 0.05f, 40f);
        check_hp();
    }
    #endregion//
    public void start_cooltime()//튜토리얼시 실행하면 쿨타임 돌아감 
    {
        StartCoroutine("player_atck_cooltime");
    }
    public IEnumerator player_atck_cooltime()
    {
        player_cooltime = player_atck_spead;
        while (player_cooltime > 0.0f)
        {
            if (!trun_manager.game_stop)
            {
                if (!trun_manager_temp.player_die)
                {
                    player_cooltime -= Time.deltaTime;
                }
                
            }
            player_cooltime_img.fillAmount = player_cooltime / player_atck_spead;

            int cooltiem_int = Mathf.FloorToInt(player_cooltime);
            if (cooltiem_int >= 0)
            {
                player_cooltime_text.SetText("" + cooltiem_int + "");
            }
            else
                player_cooltime_text.SetText("");

            yield return new WaitForFixedUpdate();
        }
        trun_manager_temp.atck_player();
    }
    public GameObject atck()
    {
        anim_atck();

        float[] player_magni_temp = player_atck_magnification(player_job);
        int[] player_type_temp = player_atck_type(player_job);
        
        GameObject atck_obj_temp = null;
        if (trun_manager_temp.player_special_atck_cooltime == 5)//1 스페이셜 
        {
            switch (player_job)
            {
                case 0:
                    atck_obj_temp = Instantiate(player_atck_sp_set[0]);
                    atck_obj_temp.GetComponent<atck_specel>().skill_dmg = player_magni_temp[1];
                    atck_obj_temp.GetComponent<atck_specel>().atck_vaalue = player_type_temp[1];
                    trun_manager_temp.player_special_atck_cooltime = 0;
                    break;
                case 1:
                    atck_obj_temp = Instantiate(player_atck_sp_set[1]);
                    atck_obj_temp.GetComponent<atck_a_specail>().skill_dmg = player_magni_temp[1];
                    atck_obj_temp.GetComponent<atck_a_specail>().atck_vaalue = player_type_temp[1];
                    trun_manager_temp.player_special_atck_cooltime = 0;
                    break;
                case 2:
                    atck_obj_temp = Instantiate(player_atck_sp_set[2]);
                    atck_obj_temp.GetComponent<m_a_sp_1>().skill_dmg = player_magni_temp[1];
                    atck_obj_temp.GetComponent<m_a_sp_1>().atck_vaalue = player_type_temp[1];
                    trun_manager_temp.player_special_atck_cooltime = 0;
                    break;
            }
        }
        else//0 이 일반 
        {
            switch (player_job)
            {
                case 0:
                    atck_obj_temp = Instantiate(player_atck_set[0]);
                    atck_obj_temp.GetComponent<atck_nomal>().skill_dmg = player_magni_temp[0];
                    atck_obj_temp.GetComponent<atck_nomal>().atck_value = player_type_temp[0];
                    break;
                case 1:
                    atck_obj_temp = Instantiate(player_atck_set[1]);
                    atck_obj_temp.GetComponent<atck_a_nomal>().skill_dmg = player_magni_temp[0];
                    atck_obj_temp.GetComponent<atck_a_nomal>().atck_vaalue = player_type_temp[0];
                    break;
                case 2:
                    atck_obj_temp = Instantiate(player_atck_set[2]);
                    atck_obj_temp.GetComponent<m_a_nomal_1>().skill_dmg = player_magni_temp[0];
                    atck_obj_temp.GetComponent<m_a_nomal_1>().atck_vaalue = player_type_temp[0];
                    break;
            }
            
        }
        Debug.Log("atck_obj_temp");
        return atck_obj_temp;
    }
    public void anim_hit()//맞음 애니메이션
    {
        player_animator.SetTrigger("dmg");
        check_hp();
    }
    public void anim_atck()//공격 애니메이션 
    {
        player_animator.SetTrigger("atck");
        check_hp();
    }
    public void resual_rection()//부활
    {
        resurrection_effect_obj.SetActive(true);
        check_hp();
    }
    void check_hp()//체력 확인용 //나중에 다시 확인.
    { 
        hp_img.fillAmount = 1f + (player_hp - player_hpmax) / 100f;
        mp_img.fillAmount = 1f + (player_mp - player_mpmax) / 100f;
        player_hp_text.SetText("" + player_hp + "");
        player_mp_text.SetText("" + player_mp + "");
    }
    public float[] player_atck_magnification(float i)//i == 직업 코드 0 무직 1 검사 등 확인용
    {
        float[] atck_dmg = { 0, 0 };// 0 일반 공격 1 스페이셜 공격 2종류 
        switch (i)
        {
            case 0:
                atck_dmg[0] = 2f;
                atck_dmg[1] = 5f;
                break;
            case 1:
                atck_dmg[0] = 5f;
                atck_dmg[1] = 6f;
                break;
            case 2:
                atck_dmg[0] = 5f;
                atck_dmg[1] = 12f;
                break;
        }
        return atck_dmg;
    }
    public int[] player_atck_type(float i)//i == 직업 코드 0 물리 1 마법
    {
        int[] atck_type = { 0, 0 };// 0물리 1 마법
        switch (i)
        {
            case 0:
                atck_type[0] = 0;
                atck_type[1] = 1;
                break;
            case 1:
                atck_type[0] = 0;
                atck_type[1] = 0;
                break;
            case 2:
                atck_type[0] = 1;
                atck_type[1] = 1;
                break;
        }
        return atck_type;
    }
}

