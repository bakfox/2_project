using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class save_sc : MonoBehaviour
{
    public save_user_data save_data = new save_user_data();// �̰͸� �ҷ��� ����ϸ� ����.

    string data_path;//���� ��� awake���� �ʱ�ȭ

    save_user_data save_temp = new save_user_data();

    // Start is called before the first frame update
    void Awake()
    {
        data_path = Application.persistentDataPath + "/" + "save_user_data" + ".json";
        Load();
        Save();
    }
    public static save_sc find_save_sc()// �ڱ� �ڽ� ã�� 
    {
        save_sc gm_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<save_sc>();
        return gm_temp;
    }
    // Update is called once per frame
    // �ҷ�����
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
    //����
    public void Save()
    {
        save_temp = save_data;

        string json = JsonUtility.ToJson(save_temp);
        Byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        string j_data = System.Convert.ToBase64String(data);

        File.WriteAllText(data_path, j_data);
    }
    public float find_upgrade_value(int value_temp)//���׷��̵� ��� �������°�// ���׷��̵� ���� 0: ���ݷ� 1: ���� 2: ������ 3: ü�� 4: ��� ȹ�淮
    {
        float answer = 0f;//��ȯ��
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
    public int find_item(int id)//������ ã�� �뵵 ��ø�� �����ش� 
    {
        for (int i = 0; i < save_data.user_have_item.Length; i++)
        {
            Debug.Log(id+"������ ã����");
            Debug.Log(save_data.user_have_item[i]+"����");
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
    //����
    public float user_job = 0;//0�⺻ 1 ���� 2 ������ 1.1~ �̷��� �þ ����
    //�⺻ ���� 
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
    public float user_gold_more = 100;//�⺻ 100��
    public int user_now_gold = 0;

    //����
    public int[] user_have_item = { 0, 0, 0, 0, 0 }; //�⺻ 5ĭ 
    public int[] user_have_item_mounting = { 0, 0, 0, 0, 0 };// ������ ��������� 1 �ö󰥼��� ��ø 

    //�������� ���� 
    public int[] fight_monster_id = { 0, 0, 0 };//�ο�� �ִ� ���� 
    public bool chuse_heal_stage = false;
    public int chuse_stage_save = 0;//������ ���� â ����
    public int stage_monster_fight = 0;//�������� �߿� ���� ���� Ƚ�� 
    public int clear_stage = 0;// 0 Ŭ����� �߰� Ŭ���� ������ ���� �������� ���� 
    public int user_now_floor = 0;// 1�� ���� ���� 3������ ���� ū���� 1�� 1�������� 
    public int user_now_stage = 0;//���� stage; // 10�ܰ� ���� ���� ����  
    public int heal_room_stack = 0;//���� ������ �������� ���� 
    public int[] bouse_stage_item = {0, 0, 0};//���ʽ� �������� ���� 3�� ���� ���� ������ ���ö� 000���� �ʱ�ȭ
    public bool change_job_stage = false;//������� ����� 

    //���׷��̵� ��
    public int upgrade_atk = 1;//�������� �ܰ躰��
    public int upgrade_matk = 1;
    public int upgrade_hp = 1;
    public int upgrade_defend = 1;
    public int upgrade_more_gold = 1;//��� �߰� ȹ�� ���� //�������� ���׷��̵�
    public int upgrade_mdefend = 1;

    public bool first_game_start = true; // ó������ Ȯ�ο� 
}

