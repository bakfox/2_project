using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_string : MonoBehaviour
{
    //1 ���� �븻 
    public string[] n_text_1 = {};
    // ���ʿ��� ���� ���ͼ� hp �Ҹ�  // �տ� ���� Ȯ��  // ���� ü�� �Ҹ� �Ǵ� ���� 1 2 3 ����
    //2 ���� �븻
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
