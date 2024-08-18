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

    float delta = 0.5f;//넓이
    [SerializeField]
    float spead = 3f;//스피드 
    bool select_check = false;
    public void OnPointerClick(PointerEventData eventData)//클릭시 
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
    IEnumerator idle() //기본 
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
    public void OnPointerEnter(PointerEventData eventData)// 벗어나면 발동 
    {
        find_job_temp.set_job_data(job_explanation(), job_name());
        
    }

    public void OnPointerExit(PointerEventData eventData)// 알려주는거 
    {
        find_job_temp.off_data_ui();
    }
    string job_name()// 이름쪽 
    {
        string s_temp = "";
        switch (this_obj_job)
        {
            case 0:
                s_temp = "모험가";
                break;
        }
        switch (this_obj_job)
        {
            case 1:
                s_temp = "화염 전사";
                break;
        }

        switch (this_obj_job)
        {
            case 2:
                s_temp = "화염 마법사";
                break;
        }


        return s_temp;
    }
    string job_explanation()// 스킬 관련 
    {
        string s_temp = "";
        switch (this_obj_job)
        {
            case 0:
                s_temp = "스킬 : 2배 물리 , 특수 스킬 : 5배 마법";
                break;
        }

        switch (this_obj_job)
        {
            case 1:
                s_temp = "스킬 : 5배 물리 , 특수 스킬 : 6배 물리 관통 ";
                break;
        }

        switch (this_obj_job)
        {
            case 2:
                s_temp = "스킬 : 5배 마법 , 특수 스킬 : 12배 마법";
                break;
        }

        return s_temp;
    }

}
