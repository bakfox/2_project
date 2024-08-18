using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class item_slot : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    public int this_slot_int;//���� ��ȣ
    public item_data item_data_temp;//������ ���� ������ 

    item_slot_seting item_slot_seting_temp;//���� ���� ���ִ� ��ũ��Ʈ 
    public GameObject baground_img_obj;// �޹��

    public find_item find_item_temp;//������ ���� 

    public Sprite defolt_img;//�⺻ ��������Ʈ �� ȭ�� 
    //�̹��� �뵵 
    public Image item_img;//��������Ʈ �̹��� ����� �̹��� ������Ʈ
    public Image mounting_img;//��ø �̹��� ���� �� ��� 
    [SerializeField]
    Sprite[] mounting_sprite;//��ø �̹���. ����

    public int mounting_int;//��ø ���� 

    float i_temp = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        item_slot_seting_temp = GameObject.FindGameObjectWithTag("gamemanager").GetComponent<item_slot_seting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (i_temp < 360)
        {
            i_temp = i_temp + Time.deltaTime*20;
            baground_img_obj.transform.rotation = Quaternion.Euler(0,0, i_temp);
        } else
            i_temp = 0;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        find_item_temp.off_data_ui();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item_data_temp != null)
        {
            find_item_temp.set_monster_data(item_data_temp);
        }        
    }
    public void OnPointerClick(PointerEventData eventData)//Ŭ���� �ߵ� 
    {
        if (item_slot_seting_temp.change_item)
        {
            item_slot_seting_temp.set_item_explanation(this_slot_int);
        }
        else if(item_slot_seting_temp.end_item_once)
        {
            if (item_data_temp != null)
            {
                item_slot_seting_temp.end_item_get(this_slot_int);
            }
        }
    }
    public void check_img()//�̹��� �� Ȯ�ο뵵 
    {
        if (item_data_temp == null)
        {
            item_img.sprite = defolt_img;
        }
        else
        {
            item_img.sprite = item_data_temp.sprite_img;
            mounting_img.sprite = mounting_sprite[mounting_int-1];
        }
            

    }
    
}
