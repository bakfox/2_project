using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rock_anim : MonoBehaviour
{
    public monster_data monster_data_temp;
    public Sprite[] sprite_temp;//바꿔 버릴 스프리트
    public GameObject efect_obj;//
    public GameObject atck_obj;
    

    Vector3 pos;//저장용 처음위치 

    float delta = 0.5f;//넓이
    [SerializeField]
    float spead = 3f;//스피드 
    [SerializeField]
    bool in_check = false;//체크용 
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        check();
    }

    // Update is called once per frame

    public void check()//상태 체크용 
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        switch (monster_data_temp.monster_statuse)
        {
            case 0:
                sprite.sprite = sprite_temp[0];
                StopAllCoroutines();
                StartCoroutine("idle");//기본
                break;
            case 1:
                sprite.sprite = sprite_temp[1];
                StopAllCoroutines();
                StartCoroutine("atck");//공격
                break;
            case 2:
                sprite.sprite = sprite_temp[2];
                StopAllCoroutines();
                StartCoroutine("dmg");//맞음
                break;
        }
    }
    IEnumerator idle() //기본 
    {
        while (monster_data_temp.monster_statuse == 0)
        {
            Vector3 v_pos = pos;
            v_pos.y = pos.y - delta * Mathf.Sin(Time.time * spead);
            transform.position = v_pos;
            yield return new WaitForFixedUpdate();
        }
        check();
    }
    IEnumerator atck() //공격
    {
        
        while (!in_check)//위로 올라가는 스크립트
        {
            if (transform.position.y >=4f)
            {
                in_check = true;
            }
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,4.05f,-2.95f),0.5f) ;
            yield return new WaitForFixedUpdate();
        }
        in_check = false;// 초기화
        while (!in_check)//위로 올라가는 스크립트
        {
            if (transform.position.y <= -1.0f)
            {
                in_check = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
            yield return new WaitForFixedUpdate();
        }
        in_check = false; 
        efect_obj.SetActive(true);
        GameObject efect_temp = Instantiate(atck_obj);
        efect_temp.GetComponent<defolt_atck_modul>().trun_Manager = monster_data_temp.trun_manager_temp;
        Debug.Log(monster_data_temp.monster_skill_dmg);
        yield return new WaitForSeconds(0.5f);
        efect_obj.SetActive(false);
        monster_data_temp.monster_statuse = 0;
        check();
    }
    IEnumerator dmg() // 데미지 받음
    {
        float dmg_x_postion = transform.position.x + 1.3f;
        while (!in_check)//뒤로 밀려나는 스크립트
        {
            if (transform.position.x >= dmg_x_postion)
            {
                in_check = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(dmg_x_postion, -0.45f, -2.95f), 0.2f);
            yield return new WaitForFixedUpdate();
        }
        in_check = false;
        monster_data_temp.monster_statuse = 0;
        check();
    }
}
