using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class red_boss_anim : MonoBehaviour
{
    public SpriteRenderer[] sprite_render;//여러 파츠들 랜더
    public Transform[] transform_temp;//여러 위치 

    public monster_data monster_data_temp;
    public Sprite[] sprite_temp;//바꿔 버릴 스프라이트 머리
    public Sprite[] body_sprite_temp;
    public Sprite[] left_sprite_temp;//손
    public Sprite[] right_sprite_temp;//손
    public GameObject atck_obj;
    public GameObject sp_atck_obj;// 특별 공격 오브젝트

    [SerializeField]
    int check_steck = 0;//5번 쓰면 궁국기 
    Vector3[] pos = new Vector3[5];//저장용 처음위치 전체 머리 몸 왼손 오른손 순

    float delta = 0.5f;//넓이
    float head_delta = 0.55f;
    float left_hend_delta = 0.2f;
    [SerializeField]
    float spead = 3f;//스피드 
    [SerializeField]
    bool in_check = false;//체크용 
    // Start is called before the first frame update
    void Start()
    {
        pos[0] = transform.position;
        pos[1] = transform_temp[0].position;
        pos[2] = transform_temp[1].position;
        pos[3] = transform_temp[2].position;
        pos[4] = transform_temp[3].position;
        check();
    }

    // Update is called once per frame

    public void check()//상태 체크용 
    {
        switch (monster_data_temp.monster_statuse)
        {
            case 0:
                sprite_render[0].sprite = sprite_temp[0];
                sprite_render[1].sprite = body_sprite_temp[0];
                sprite_render[2].sprite = left_sprite_temp[0];
                sprite_render[3].sprite = right_sprite_temp[0];
                StopAllCoroutines();
                StartCoroutine("idle");//기본
                break;
            case 1:
                sprite_render[0].sprite = sprite_temp[0];
                sprite_render[1].sprite = body_sprite_temp[0];
                sprite_render[2].sprite = left_sprite_temp[0];
                sprite_render[3].sprite = right_sprite_temp[0];
                StopAllCoroutines();
                if (check_steck != 5)
                {
                    StartCoroutine("atck");//공격
                }
                else
                    StartCoroutine("atck_sp");
                
                break;
            case 2:
                sprite_render[0].sprite = sprite_temp[1];
                sprite_render[1].sprite = body_sprite_temp[1];
                sprite_render[2].sprite = left_sprite_temp[1];
                sprite_render[3].sprite = right_sprite_temp[1];
                StopAllCoroutines();
                StartCoroutine("dmg");//맞음
                break;
        }
    }
    IEnumerator idle() //기본 
    {
        Vector3 v_pos = pos[0];
        Vector3 v_l_pos = pos[3];
        Vector3 v_r_pos = pos[4];
        Vector3 v_h_pos = pos[1];

        while (monster_data_temp.monster_statuse == 0)
        {
            v_pos.y = pos[0].y - delta * Mathf.Sin(Time.time * spead);
            v_h_pos.y = pos[1].y - head_delta * Mathf.Sin(Time.time * spead);
            v_l_pos.x = pos[3].x - left_hend_delta * Mathf.Sin (Time.time * spead);
            v_l_pos.y = pos[3].y - delta * Mathf.Sin(Time.time * spead);
            v_r_pos.x = pos[4].x + left_hend_delta * Mathf.Sin(Time.time * spead);
            v_r_pos.y = pos[4].y - delta * Mathf.Sin(Time.time * spead); 
            transform.position = v_pos;
            transform_temp[0].position = v_h_pos;
            transform_temp[2].position = v_l_pos;
            transform_temp[3].position = v_r_pos;
            yield return new WaitForFixedUpdate();
        }
        check();
    }
    IEnumerator atck() //공격
    {
        check_steck++;
        sprite_render[0].sprite = sprite_temp[2];
        
        float time = 0;
        while (time < 1f)
        {
            time += 10 * Time.deltaTime;
            transform_temp[2].position = Vector3.Lerp(transform_temp[2].position, new Vector3(pos[3].x -1f, pos[3].y, pos[3].z), time);
            yield return new WaitForFixedUpdate();
        }
        sprite_render[2].sprite = left_sprite_temp[2];
        GameObject efect_temp = Instantiate(atck_obj);
        efect_temp.GetComponent<red_boss_atck_modul>().trun_manager_temp = monster_data_temp.trun_manager_temp;
        efect_temp.GetComponent<red_boss_atck_modul>().defolt_postion = transform_temp[2].position;
        Debug.Log(monster_data_temp.monster_skill_dmg);
        yield return new WaitForSeconds(0.5f);
        monster_data_temp.monster_statuse = 0;
        check();
    }
    IEnumerator atck_sp() //공격
    {
        check_steck = 0;
        sprite_render[0].sprite = sprite_temp[2];
        float time = 0;

        while (time < 1f)
        {
            time +=  Time.deltaTime;
            transform_temp[2].position = Vector3.Lerp(new Vector3(pos[3].x - 1f, pos[3].y + 1.3f, pos[3].z), new Vector3(pos[3].x - 1f, pos[3].y, pos[3].z), time);
            yield return new WaitForFixedUpdate();
        }
        monster_data_temp.trun_manager_temp.atkc_Certain_monster_temp = 1;
        GameObject efect_temp = Instantiate(sp_atck_obj);
        efect_temp.GetComponent<defolt_atck_modul>().trun_Manager = monster_data_temp.trun_manager_temp;
        Debug.Log(monster_data_temp.monster_skill_dmg);
        yield return new WaitForSeconds(0.5f);
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
