using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class make_manager : MonoBehaviour
{

    public GameObject help_main_ai;

    public int tutorial_steck = 0;//�̰� �ö󰥼��� �ߵ� 

    public bool atck_tuto = false;

    trun_manager trun_manager_temp;
    save_sc save_sc_temp;

    // Start is called before the first frame update
    void Start()
    {
        trun_manager_temp = gameObject.GetComponent<trun_manager>();
        save_sc_temp = save_sc.find_save_sc();
        check_tutorial_steck();
        
    }

    // Update is called once per frame
    void Update()
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
                make_1();
                break;
            case 1:
                make_2();
                break;
            case 2:
                make_3();
                break;
            case 3:
                make_4();
                break;
            case 4:
                make_5();
                break;
            case 5:
                make_6();
                break;


        }
    }
    public void make_1()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("Ŭ���� ������ !", 3, 1);
    }

    public void make_2()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("���� �״��� ���� ����� ���̾� !", 0, 0);
    }
    public void make_3()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("�������� ��带 �� ��� �ٰ� !", 2, 1);
        save_sc_temp.save_data.user_now_gold = save_sc_temp.save_data.user_now_gold * 2;
    }
    public void make_4()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("�÷������༭ ���� !", 3, 0);
    }
    public void make_5()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("������ ����� ���� !", 0, 1);
    }
    public void make_6()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("...", 0, 0);
        trun_manager_temp.end_game();
    }
}
