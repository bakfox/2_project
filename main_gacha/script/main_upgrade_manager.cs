using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class main_upgrade_manager : MonoBehaviour
{
    public int now_upgrade_what = 0;//���� //���׷��̵� ���� 0: ���ݷ� 1: ���� 2: ������ 3: ü�� 4: ��� ȹ�淮 

    public TextMeshProUGUI gold_text; 
    public GameObject buy_one_more;
    public GameObject nead_more_gold;
    public int nead_bye_gold;//������� ���

    [SerializeField]    
    save_sc save_sc_temp;
    public bool trade_end = false;//�ŷ� ������

    public sound_manager sound_temp;
    // Start is called before the first frame update
    void Start()
    {
        save_sc_temp = save_sc.find_save_sc();
        set_gold_ui();
    }

    public void do_not_bye()//�Ȼ��
    {
        buy_one_more.SetActive(false);
    }
    public void buy_upgrade()//�춧 �θ��°� ���� ������ �ߵ�//���� //���׷��̵� ���� 0: ���ݷ� 1: ���� 2: ������ 3: ü�� 4: ��� ȹ�淮 
    {
        if (nead_bye_gold <= save_sc_temp.save_data.user_now_gold)
        {
            save_sc_temp.save_data.user_now_gold -= nead_bye_gold;
            switch (now_upgrade_what)
            {
                case 0:
                    save_sc_temp.save_data.upgrade_atk++;
                    break;
                case 1:
                    save_sc_temp.save_data.upgrade_defend++;
                    break;
                case 2:
                    save_sc_temp.save_data.upgrade_matk++;
                    break;
                case 3:
                    save_sc_temp.save_data.upgrade_hp++;
                    break;
                case 4:
                    save_sc_temp.save_data.upgrade_more_gold++;
                    break;
                case 5:
                    save_sc_temp.save_data.upgrade_mdefend++;
                    break;
            }
            now_upgrade_what = 0;
            buy_one_more.SetActive(false);
            save_sc_temp.Save();
            trade_end = true;
            set_gold_ui();
        }
        else
        {
            nead_more_gold.SetActive(true);
            StartCoroutine("nead_more_gold_false");
        }
        
    }
    IEnumerator nead_more_gold_false()// �ڷ�ƾ 
    {
        yield return new WaitForSeconds(0.3f);
        nead_more_gold.SetActive (false);
        StopCoroutine("nead_more_gold_false");
    }
    void set_gold_ui()
    {
        gold_text.SetText(save_sc_temp.save_data.user_now_gold + "");
    }
    public void return_menu()
    {
        sound_temp.change_main();
        load_manager.LoadScene_fast("main_ui");
    }
}
