using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class item_data_base : MonoBehaviour
{
    public GameObject[] item_data_bs;//아이템 모음집 인덱스가 아이디임 ex: 1번 포션 
    // Start is called before the first frame update
    
    public item_data return_find_item_data(int id_temp)
    {
        item_data item_Data_temp = new item_data();
        if (item_data_bs[id_temp] != null)
        {
            item_Data_temp = item_data_bs[id_temp].GetComponent<item_data>();
        }
        return item_Data_temp;
    }
}
