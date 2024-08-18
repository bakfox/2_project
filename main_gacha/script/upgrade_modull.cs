using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class upgrade_modull : MonoBehaviour , IPointerClickHandler
{
    public TextMeshProUGUI now_upgrade_value;//���׷��̵� �� �ö󰡴� ��ġ�׷��� ���� �ؽ�Ʈ
    public TextMeshProUGUI nead_upgrade_gold;//���׷��̵� �� �ʿ��� ��� 

    public GameObject buy_one_more;//�ѹ��� ����� ���� ������Ʈ 
    public GameObject nead_more_gold;//��� ������ 

    public int upgrade_value = 0; //���׷��̵� ���� 0: ���ݷ� 1: ���� 2: ������ 3: ü�� 4: ��� ȹ�淮  5: ���� ����

    public int nead_gold_defold = 0;
    [SerializeField]
    main_upgrade_manager main_up_temp;
    [SerializeField]
    save_sc save_temp_sc;
    private void Awake()
    {
        save_temp_sc = save_sc.find_save_sc();
        main_up_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<main_upgrade_manager>();
        StartCoroutine("reseet_ui_update");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        int nead_gold = find_nead_gold();
        if (nead_gold <= save_temp_sc.save_data.user_now_gold)
        {
            buy_one_more.SetActive(true);
            main_up_temp.now_upgrade_what = upgrade_value;
            main_up_temp.nead_bye_gold = nead_gold;
        }
        else 
        {
            nead_more_gold.SetActive(true);
            StartCoroutine("nead_more_gold_false");
        }
             

    }

    int find_nead_gold()// ���׷��̵� ���� 0: ���ݷ� 1: ���� 2: ������ 3: ü�� 4: ��� ȹ�淮
    {
        float nead_gold = 0;//�⺻��

        switch (upgrade_value)
        {
            case 0:
                nead_gold = nead_gold_defold;
                break;
            case 1:
                nead_gold = nead_gold_defold ;
                break;
            case 2:
                nead_gold = nead_gold_defold;
                break;
            case 3:
                nead_gold = nead_gold_defold;
                break;
            case 4:
                nead_gold = nead_gold_defold * (save_temp_sc.save_data.upgrade_more_gold * 0.1f);
                break;
            case 5:
                nead_gold = nead_gold_defold;
                break;
        }
        return (int)nead_gold;
    }
    IEnumerator reseet_ui_update()
    {
        yield return new WaitForSeconds(0.1f);
        int nead_gold_index = find_nead_gold();
        float now_upgrade_index = save_temp_sc.find_upgrade_value(upgrade_value);
        now_upgrade_value.SetText("upgrade : "+ now_upgrade_index);
        nead_upgrade_gold.SetText("�ʿ� ��� : "+ nead_gold_index);
        main_up_temp.trade_end = false;
        StopCoroutine("reseet_ui_update");
    }
    public void FixedUpdate()
    {
        if (main_up_temp.trade_end == true)
        {
            StartCoroutine("reseet_ui_update");
        }
        
    }
    IEnumerator nead_more_gold_false()// Ȯ���� �������  
    {
        yield return new WaitForSeconds(0.4f);
        nead_more_gold.SetActive(false);
        StopCoroutine("nead_more_gold_false");
    }
}
