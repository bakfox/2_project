using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_manager : MonoBehaviour
{
    monster_data_base monster_data_temp;//생성때 넣어줄 몬스터 내용들 템프// 내용 불러올때도 사용
    public save_sc save_sc_temp;
    trun_manager trun_Manage_temp;
    // Start is called before the first frame update
    void Start()
    {
        save_sc_temp = save_sc.find_save_sc();
        monster_data_temp = GetComponent<monster_data_base>();
        trun_Manage_temp = GetComponent<trun_manager>();
        spawn_monster();
    }

    void spawn_monster()//몬스터 스폰 
    {
        GameObject obj_temp;

            if (save_sc_temp.save_data.user_now_stage == 10)
            {
                obj_temp = monster_data_temp.return_boss_obj(save_sc_temp.save_data.user_now_floor);
                GameObject monster_obj = Instantiate(obj_temp) as GameObject;

                Transform monster_obj_tsf = monster_obj.GetComponent<Transform>();
                monster_obj_tsf.position = new Vector3(4 , 1.05f, -2.95f);
                trun_Manage_temp.monster_obj[0] = monster_obj;
                trun_Manage_temp.live_monster++;
            }
            else
            {
                if (save_sc_temp.save_data.fight_monster_id[0] != 0)
                {
                    int i = 0;
                for (int i_temp = 0; i_temp < 3; i_temp++)
                {
                    if (save_sc_temp.save_data.fight_monster_id[i_temp] != 0)
                    {
                        i++;
                        Debug.Log("현재 몬스터 " + i);
                    }
                 }
                    for (int i_temp_2 = 0; i_temp_2 < i; i_temp_2++)
                    {
                        obj_temp = monster_data_temp.retun_monster_obj(save_sc_temp.save_data.fight_monster_id[i_temp_2]);

                        GameObject monster_obj = Instantiate(obj_temp);

                        Transform monster_obj_tsf = monster_obj.GetComponent<Transform>();
                        monster_obj_tsf.position = new Vector3(0 + (4 * i_temp_2), -1.05f, -2.95f);
                        trun_Manage_temp.live_monster++;
                        trun_Manage_temp.monster_obj[i_temp_2] = monster_obj;
                    }
                }
                else
                {
                    int i = random_sc.random_gacha(3);
                    for (int i_temp = 0; i_temp < i + 1; i_temp++)
                    {

                        obj_temp = monster_data_temp.retun_monster_obj(random_chuse_monster());

                        GameObject monster_obj = Instantiate(obj_temp);

                        Transform monster_obj_tsf = monster_obj.GetComponent<Transform>();
                        monster_obj_tsf.position = new Vector3(0 + (4 * i_temp), -1.05f, -2.95f);
                        trun_Manage_temp.monster_obj[i_temp] = monster_obj;
                        trun_Manage_temp.live_monster++;
                        save_sc_temp.save_data.fight_monster_id[i_temp] = monster_obj.GetComponent<monster_data>().monster_id;

                        save_sc_temp.Save();
                 }
            }
        }
    }
    int random_chuse_monster() 
    {
        int i_r = 0;
        switch (save_sc_temp.save_data.user_now_floor)
        {
            case 0:
                i_r = Random.Range(1,5);
                break;
            case 1:
                i_r = Random.Range(1,5);
                break;
            case 2:
                i_r = Random.Range(5, monster_data_temp.monster_obj.Length+1);
                break;
        }

        return i_r;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
