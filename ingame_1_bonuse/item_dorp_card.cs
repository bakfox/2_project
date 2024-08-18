using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class item_dorp_card : MonoBehaviour , IPointerClickHandler
{
    // Start is called before the first frame update
    [SerializeField]
    int item_id = 0;
    public int card_id ;//���� ���� ī�� ���̵� 0~2 ī�� ����� 

    save_sc save_temp;
    item_slot_seting item_slot_seting_temp;
    item_data_base item_data_Base_temp;
    item_data item_temp;

    [SerializeField]
    GameObject chuse_item_ui;
    [SerializeField]
    GameObject re_chuse_item_ui;
    [SerializeField]
    Image card_img;
    [SerializeField]
    TextMeshProUGUI text_text;
    [SerializeField]
    slot_firtst slot_fst_temp;
    void Start()
    {
        save_temp = save_sc.find_save_sc();
        item_slot_seting_temp = GameObject.FindWithTag("gamemanager").GetComponent<item_slot_seting>();
        item_data_Base_temp = GameObject.FindWithTag("gamemanager").GetComponent<item_data_base>();
        slot_fst_temp = GameObject.FindWithTag("gamemanager").GetComponent<slot_firtst>();
        StartCoroutine("trun_first");
        set_item_card();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void set_item_card()//ī�� ó�� �������� �̱�
    {
        if (save_temp.save_data.bouse_stage_item[card_id] != 0)
        {
            item_id = save_temp.save_data.bouse_stage_item[card_id];
        }else
            switch (save_temp.save_data.clear_stage)
            {
                case 0:
                    item_id = Random.Range(1, 4);
                    break;
                case 1:
                    item_id = Random.Range(1, 9);
                    break;
                case 2:
                    item_id = Random.Range(1,10);
                    break;
            }
        save_temp.save_data.bouse_stage_item[card_id] = item_id;
        save_temp.Save();
        set_item();
    }
    public void chuse_item()
    {

    }
    void set_item()
    {
        item_temp = item_data_Base_temp.return_find_item_data(item_id);
        card_img.sprite = item_temp.sprite_img;
        text_text.SetText(item_temp.item_explanation);
    }

    public void OnPointerClick(PointerEventData eventData)// Ŭ���� �ߵ� 
    {
        Debug.Log("Ŭ����");
        item_slot_seting_temp.rooting_item(item_id);
        slot_fst_temp.end_card();
        if (item_slot_seting_temp.pull_item_mounting)
        {
            re_chuse_item_ui.SetActive(true);
            item_slot_seting_temp.pull_item_mounting = false;
        }
    }
    IEnumerator trun_first()//ó�� Ȱ��ȭ�� �ڵ� ȸ�� 
    {
        float run_Time = 90f;
        RectTransform rect_1_temp = gameObject.GetComponent<RectTransform>();
        while (run_Time < 360f)
        {
            run_Time += Time.deltaTime * 150f;
            rect_1_temp.rotation = Quaternion.Euler(0, run_Time, 0);
            yield return new WaitForFixedUpdate();
        }
        rect_1_temp.rotation = Quaternion.Euler(0,360f,0); 
        Debug.Log(card_id);
        StopCoroutine("trun_first");
        yield return null;
    }
}
