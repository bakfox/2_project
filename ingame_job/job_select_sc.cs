using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class job_select_sc : MonoBehaviour , IPointerClickHandler  , IPointerEnterHandler, IPointerExitHandler
{
    public int this_obj_job = 0;
    public GameObject imgae_obj;
    public Sprite[] sprite_img;
    public find_job find_job_temp;
    public sound_manager sound_temp;
    save_sc save_temp;

    float delta = 0.5f;//����
    [SerializeField]
    float spead = 3f;//���ǵ� 
    bool select_check = false;
    public void OnPointerClick(PointerEventData eventData)//Ŭ���� 
    {
        save_temp.save_data.user_job = this_obj_job;
        save_temp.save_data.user_now_stage = 1;
        save_temp.save_data.change_job_stage = false;
        save_temp.Save();
        sound_temp.change_mab_main();
        load_manager.LoadScene_fast("in_game_1");
    }

    void Start()
    {
        save_temp = save_sc.find_save_sc();
        Invoke("image_set",0.2f);
    }

    void image_set()
    {
        Debug.Log(imgae_obj);
        imgae_obj.GetComponent<SpriteRenderer>().sprite = sprite_img[this_obj_job];
        StartCoroutine("idle");
    }
    IEnumerator idle() //�⺻ 
    {
        Vector3 defolt_pos = imgae_obj.transform.position;
        while (!select_check)
        {
            Vector3 v_pos = defolt_pos;
            v_pos.y = defolt_pos.y - delta * Mathf.Sin(Time.time * spead);
            imgae_obj.transform.position = v_pos;
            yield return new WaitForFixedUpdate();
        }
        StopCoroutine("idle");
    }
    public void OnPointerEnter(PointerEventData eventData)// ����� �ߵ� 
    {
        find_job_temp.set_job_data(job_explanation(), job_name());
        
    }

    public void OnPointerExit(PointerEventData eventData)// �˷��ִ°� 
    {
        find_job_temp.off_data_ui();
    }
    string job_name()// �̸��� 
    {
        string s_temp = "";
        switch (this_obj_job)
        {
            case 0:
                s_temp = "���谡";
                break;
        }
        switch (this_obj_job)
        {
            case 1:
                s_temp = "ȭ�� ����";
                break;
        }

        switch (this_obj_job)
        {
            case 2:
                s_temp = "ȭ�� ������";
                break;
        }


        return s_temp;
    }
    string job_explanation()// ��ų ���� 
    {
        string s_temp = "";
        switch (this_obj_job)
        {
            case 0:
                s_temp = "��ų : 2�� ���� , Ư�� ��ų : 5�� ����";
                break;
        }

        switch (this_obj_job)
        {
            case 1:
                s_temp = "��ų : 5�� ���� , Ư�� ��ų : 6�� ���� ���� ";
                break;
        }

        switch (this_obj_job)
        {
            case 2:
                s_temp = "��ų : 5�� ���� , Ư�� ��ų : 12�� ����";
                break;
        }

        return s_temp;
    }

}
