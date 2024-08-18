using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class find_item : MonoBehaviour
{
    //������ ���� ����
    string item_s;
    string item_n;
    Sprite item_img;

    //������ ���� ������ �ؽ�Ʈ �̹�����
    public TextMeshProUGUI item_string;
    public TextMeshProUGUI item_name;
    public Image img;

    public GameObject actvit_obj;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void set_monster_data(item_data item_date_temp)
    {
        on_data_ui();

        item_s = item_date_temp.item_explanation;
        item_n = item_date_temp.item_name;
        item_img = item_date_temp.sprite_img;

        item_string.SetText(""+item_s+"");
        item_name.SetText("�̸� :" + item_n);
        img.sprite = item_img;
    }
    public void on_data_ui()
    {
        if (!trun_manager.game_stop)
        {
            actvit_obj.SetActive(true);
        }

    }
    public void off_data_ui()
    {
        actvit_obj.SetActive(false);
    }

}
