using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class making_mganger : MonoBehaviour
{
    public GameObject help_main_ai;
    public Vector3[] help_postion;//����� ��ġ 

    public int tutorial_steck = 0;//�̰� �ö󰥼��� �ߵ� 

    public bool atck_tuto = false;

    trun_manager trun_manager_temp;
    save_sc save_sc_temp;

    // Start is called before the first frame update
    void Start()
    {
        trun_manager_temp = gameObject.GetComponent<trun_manager>();
        check_tutorial_steck();
        save_sc_temp = save_sc.find_save_sc();
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
                make_1();
                break;
            case 1:
                break;
        }
    }
    public void make_1()
    {
        help_main_ai.GetComponent<helper_main_ai>().set_text_and_anim("�ȳ� ������ �ݰ��� ���� ���� ..", 3, 1);
    }
}
