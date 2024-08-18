using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class item_slot : MonoBehaviour , IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler
{
    public int this_slot_int;//슬롯 번호
    public item_data item_data_temp;//장착한 슬롯 아이템 

    item_slot_seting item_slot_seting_temp;//슬롯 셋팅 해주는 스크립트 
    public GameObject baground_img_obj;// 뒷배경

    public find_item find_item_temp;//아이템 템프 

    public Sprite defolt_img;//기본 스프라이트 빈 화면 
    //이미지 용도 
    public Image item_img;//스프라이트 이미지 사용할 이미지 오브젝트
    public Image mounting_img;//중첩 이미지 예로 별 모양 
    [SerializeField]
    Sprite[] mounting_sprite;//중첩 이미지. 아직

    public int mounting_int;//중첩 갯수 

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
    public void OnPointerClick(PointerEventData eventData)//클릭시 발동 
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
    public void check_img()//이미지 등 확인용도 
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
