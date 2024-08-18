using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class save_sc : MonoBehaviour
{
    public save_user_data save_data = new save_user_data();// 이것만 불러서 사용하면 끝남.

    string data_path;//저장 장소 awake에서 초기화

    save_user_data save_temp = new save_user_data();

    // Start is called before the first frame update
    void Awake()
    {
        data_path = Application.persistentDataPath + "/" + "save_user_data" + ".json";
        Load();
        Save();
    }
    public static save_sc find_save_sc()// 자기 자신 찾기 
    {
        save_sc gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<save_sc>();
        return gm_temp;
    }
    // Update is called once per frame
    // 불러오기
    public void Load()
    {
        save_user_data save_temp = new save_user_data();
        if (!File.Exists(data_path))
        {
            Save();
        }
        if (File.Exists(data_path))
        {
            string json = File.ReadAllText(data_path);

            byte[] data =  System.Convert.FromBase64String(json);
            string j_data = System.Text.Encoding.UTF8.GetString(data);

            save_temp = JsonUtility.FromJson<save_user_data>(j_data);
            
        }
        save_data = save_temp;

        return;
    }
    //저장
    public void Save()
    {
        save_temp = save_data;

        string json = JsonUtility.ToJson(save_temp);
        Byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        string j_data = System.Convert.ToBase64String(data);

        File.WriteAllText(data_path, j_data);
    }
    public float find_upgrade_value(int value_temp)//업그레이드 비례 강해지는것// 업그레이드 종류 0: 공격력 1: 방어력 2: 마법력 3: 체력 4: 골드 획득량
    {
        float answer = 0f;//반환값
        switch (value_temp)
        {
            case 0:
                answer = save_data.user_atk + (save_data.upgrade_atk * 0.5f);
                break;
            case 1:
                answer = save_data.user_defend + (save_data.upgrade_defend * 0.5f);
                break;
            case 2:
                answer = save_data.user_matk + (save_data.upgrade_matk * 0.5f);
                break;
            case 3:
                answer = save_data.user_hp + (save_data.upgrade_hp * 50);
                break;
            case 4:
                answer = save_data.user_gold_more + (save_data.upgrade_more_gold * 20f);
                break;
            case 5:
                answer = save_data.user_mdefend + (save_data.upgrade_mdefend * 0.5f);
                break;
        }
        return answer;
    }
    public int find_item(int id)//아이템 찾는 용도 중첩을 보내준다 
    {
        for (int i = 0; i < save_data.user_have_item.Length; i++)
        {
            Debug.Log(id+"아이템 찾는중");
            Debug.Log(save_data.user_have_item[i]+"있음");
            if (save_data.user_have_item[i] == id) 
            {
                Debug.Log("+" + save_data.user_have_item_mounting[i]);
                return save_data.user_have_item_mounting[i];
            } 
        }
        return 0;
    }
    public void clear_floor_boos()
    {
        if (save_data.user_now_floor == 1)
        {
            if (save_data.clear_stage == 0)
            {
                save_data.clear_stage = 1;
            }
        }else if (save_data.user_now_floor == 2)
        {
            if (save_data.clear_stage == 1)
            {
                save_data.clear_stage = 2;
            }
        }
    }

}

[System.Serializable]
public class save_user_data
{
    //전직
    public float user_job = 0;//0기본 1 전사 2 마법사 1.1~ 이렇게 늘어날 예정
    //기본 스펙 
    public float player_atck_spead_cooltime = 10f;
    public float user_have_hp_percent = 1f;
    public float user_have_mp_percent = 1f;
    public float user_hp = 100;
    public float user_hp_max = 100;
    public float user_atk = 1;
    public float user_mp = 100;
    public float user_mp_max = 100;
    public float user_matk = 1;
    public float user_defend = 1;
    public float user_mdefend = 1;
    public float user_gold_more = 100;//기본 100퍼
    public int user_now_gold = 0;

    //유물
    public int[] user_have_item = { 0, 0, 0, 0, 0 }; //기본 5칸 
    public int[] user_have_item_mounting = { 0, 0, 0, 0, 0 };// 아이템 들어있으면 1 올라갈수록 중첩 

    //스테이지 관련 
    public int[] fight_monster_id = { 0, 0, 0 };//싸우고 있는 몬스터 
    public bool chuse_heal_stage = false;
    public int chuse_stage_save = 0;//선택지 고르는 창 저장
    public int stage_monster_fight = 0;//스테이지 중에 몬스터 잡은 횟수 
    public int clear_stage = 0;// 0 클리어시 추가 클리어 층수에 따라 여러가지 오픈 
    public int user_now_floor = 0;// 1층 부터 현재 3층까지 예정 큰범위 1층 1스테이지 
    public int user_now_stage = 0;//현재 stage; // 10단계 보스 작은 범위  
    public int heal_room_stack = 0;//힐룸 무조껀 나오도록 설정 
    public int[] bouse_stage_item = {0, 0, 0};//보너스 스테이지 들어가면 3개 저장 고른뒤 저장후 나올때 000으로 초기화
    public bool change_job_stage = false;//보스잡고 저장용 

    //업그레이드 쪽
    public int upgrade_atk = 1;//상점에서 단계별로
    public int upgrade_matk = 1;
    public int upgrade_hp = 1;
    public int upgrade_defend = 1;
    public int upgrade_more_gold = 1;//골드 추가 획득 결산시 //상점에서 업그레이드
    public int upgrade_mdefend = 1;

    public bool first_game_start = true; // 처음시작 확인용 
}

