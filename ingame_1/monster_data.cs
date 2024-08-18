
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class monster_data : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler 
{
    public int monster_statuse = 0;// 상태 체크용 0 기본 1 공격 2맞음 

    public int monster_id;//몬스터 id
    public Image hp_img;//hp용 img
    public TextMeshProUGUI hp_text;//체력 적을 텍스트
    public GameObject hit_obj;
    public TextMeshProUGUI hit_text;

    public int hit_value = 0;// 0 물릭 1마법 맞을때 
    public enum Type { atck , matck }// 공격형 , 마법형 
    public string monster_name;
    public Type Monster_type;//타입 
    public float monster_atck_spead_cooltime;//
    public float monster_hp;
    public float monster_max_hp;
    public float monster_mp;
    public float monster_max_mp;
    public float monster_mdefend;
    public float monster_defend;
    public float monster_atck;
    public float monster_matck;
    public Sprite monster_img;
    public float[] monster_skill_dmg;

    public float skill_dmg_temp;//스킬 데미지 저장용

    public float monster_cooltiem;
    public bool monster_death = false;

    monster_ui monster_data_ui;
    public trun_manager trun_manager_temp;
    public float dmg_temp;//받은 데미지 
    save_sc save_temp;
    // Start is called before the first frame update
    void Start()
    {
        monster_data_ui = GameObject.FindGameObjectWithTag("monster_ui").GetComponent<monster_ui>();
        trun_manager_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<trun_manager>();
        hit_text = hit_obj.GetComponent<TextMeshProUGUI>();
        check_stage_more_power();
        set_item();
        set_hp();
        set_corutine();
    }
    
    // Update is called once per frame
    void Update()
    {
        check_die();
    }
    void check_die()
    {
        if (monster_hp <= 0)
        {
            monster_death = true;
            trun_manager_temp.live_monster--;
            save_temp.save_data.stage_monster_fight++;
            gameObject.SetActive(false);
        } 
    }
    void check_stage_more_power()// 층수 비례 더강해지도록 
    {
        save_temp = save_sc.find_save_sc();
        monster_hp = monster_hp * ( save_temp.save_data.user_now_stage * 0.15f * (save_temp.save_data.user_now_floor) * 3);
        monster_max_hp = monster_max_hp * (save_temp.save_data.user_now_stage * 0.15f * (save_temp.save_data.user_now_floor) * 3);
        monster_mp = monster_mp * (save_temp.save_data.user_now_stage * 0.15f * (save_temp.save_data.user_now_floor) * 3);
        monster_max_mp = monster_max_mp * (save_temp.save_data.user_now_stage * 0.15f * (save_temp.save_data.user_now_floor) * 3);
        monster_mdefend = monster_mdefend * (save_temp.save_data.user_now_stage * 0.1f * (save_temp.save_data.user_now_floor) * 3);
        monster_defend = monster_defend * (save_temp.save_data.user_now_stage * 0.1f * (save_temp.save_data.user_now_floor) * 3);
        monster_atck = monster_atck * (save_temp.save_data.user_now_stage * 0.15f * (save_temp.save_data.user_now_floor ) * 3);
        monster_matck = monster_matck * (save_temp.save_data.user_now_stage * 0.15f * (save_temp.save_data.user_now_floor ) * 3);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        monster_data_ui.off_data_ui();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        monster_data_ui.set_monster_data(gameObject);
    }
    IEnumerator monster_atck_coltiem()
    {
        monster_cooltiem = monster_atck_spead_cooltime;
        while (monster_cooltiem > 0.0f)
        {
            if (!trun_manager.game_stop)
            { 
                monster_cooltiem -= Time.deltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
        trun_manager_temp.monster_atck_trun(gameObject);
    }
    public IEnumerator hit_anim()
    {
        float check_dmg = 0;
        trun_manager_temp.changer_main_came();
        monster_statuse = 2;
        hit_obj.SetActive(true);
        if (hit_value == 0)
        {
            dmg_temp = trun_manager_temp.player_atck_mechanism();
            Debug.Log(dmg_temp);
            check_dmg = (dmg_temp * skill_dmg_temp) / monster_defend;
            Debug.Log("test:" + skill_dmg_temp);
            monster_hp = monster_hp - check_dmg;
        }
        else
        {
            dmg_temp = trun_manager_temp.player_matck_mechanism();
            check_dmg = (dmg_temp * skill_dmg_temp) / monster_mdefend;
            Debug.Log("test:" + check_dmg);
            monster_hp = monster_hp - check_dmg;
        }
        trun_manager_temp.re_coltime_player();
        set_hp();
        hit_text.SetText( "-" + check_dmg.ToString("n1"));
        yield return new WaitForSeconds(0.5f);
        hit_obj.SetActive(false);
        StopCoroutine("hit_anim");
    }
    public void set_hp()
    {
        hp_text.SetText("" + monster_hp + "");
        hp_img.fillAmount = monster_hp / monster_max_hp;
    }
    void set_item()
    {
        
        switch (save_temp.find_item(8))//방어력 감소
        {
            case 0:
                monster_defend += 0;
                break;
            case 1:
                monster_defend -= monster_defend * 20 / 100;
                break;
            case 2:
                monster_defend -= monster_defend * 35 / 100;
                break;
            case 3:
                monster_defend -= monster_defend * 50 / 100;
                break;
        }
        switch (save_temp.find_item(4))//마법 방어력 감소
        {
            case 0:
                monster_mdefend += 0;
                break;
            case 1:
                monster_mdefend -= monster_mdefend * 20 / 100;
                break;
            case 2:
                monster_mdefend -= monster_mdefend * 35 / 100;
                break;
            case 3:
                monster_mdefend -= monster_mdefend * 50 / 100;
                break;
        }
    }
    public void set_corutine()
    {
        StartCoroutine("monster_atck_coltiem");
    }
    public float atkc_Certain_check_monster(int i)
    {
        float atkc_temp;
        switch (i)
        {
            case 0:
                atkc_temp = monster_skill_dmg[0];
                break;
            case 1:
                atkc_temp = monster_skill_dmg[1];
                break;
            case 2: 
                atkc_temp = monster_skill_dmg[2];
                break;
            default: 
                atkc_temp = 0;
                break;
        }
        return atkc_temp;
    }
}
