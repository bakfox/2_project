using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper_main_ai : MonoBehaviour
{
    public int eye_statuse;// 0~3 ���� �⺻ , °������ , ���� , ����
    public int body_statuse;// 0~4 ���� �⺻ , ���� , ���� , ������ , ����
    public GameObject text_obj;

    public string set_text;

    public text_effect text_effect_temp;
    [SerializeField]
    helper_chang_anim help_temp;

    public bool text_end = true;// ä�� ����
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_text_and_anim(string text,int eye_index,int body_index)//�ؽ�Ʈ ���� 
    {
        text_end = false;
        text_obj.SetActive(true);
        gameObject.SetActive(true);
        body_statuse = body_index;
        eye_statuse = eye_index;
        set_text = text;

        text_effect_temp.set_msg(set_text);
        help_temp.changer_anim();
    }
    public void trun_off_helper()
    {
        StartCoroutine("trun_off_corutin");
    }
    IEnumerator trun_off_corutin()
    {
        yield return new WaitForSeconds(3.5f);
        gameObject.SetActive(false);
    }
}
