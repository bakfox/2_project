using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper_main_ai : MonoBehaviour
{
    public int eye_statuse;// 0~3 순서 기본 , 째려보기 , 찡긋 , 웃음
    public int body_statuse;// 0~4 순서 기본 , 설명 , 웃기 , 꺼내기 , 응원
    public GameObject text_obj;

    public string set_text;

    public text_effect text_effect_temp;
    [SerializeField]
    helper_chang_anim help_temp;

    public bool text_end = true;// 채팅 끝남
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_text_and_anim(string text,int eye_index,int body_index)//텍스트 생성 
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
