using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_string : MonoBehaviour
{
    //1 모음 노말 
    public string[] n_text_1 = {};
    // 뒤쪽에서 몬스터 나와서 hp 소모  // 앞에 몬스터 확정  // 랜덤 체력 소모 또는 보상 1 2 3 순서
    //2 모음 노말
    public string[] n_text_2 = { };
    public int now_floor;


    private void Start()
    {
        save_sc save_temp = save_sc.find_save_sc();
        now_floor = save_temp.save_data.user_now_floor;
    }
   
    public string[] find_stage_string()
    {
        string[] string_temp = new string[3];
        
        switch (now_floor)
        {
            case 1:
                string_temp = n_text_1;
                break;
            case 2:
                string_temp = n_text_2;
                break;
            default:
                return string_temp = n_text_1;
        }
        Debug.Log(string_temp);
        return string_temp;
    }
  
}
