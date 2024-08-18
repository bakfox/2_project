using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_anim : MonoBehaviour
{
    public monster_data monster_data_temp;
    public Sprite[] sprite_temp;//�ٲ� ���� ������Ʈ
    public GameObject atck_obj;
    public GameObject effect_obj;//����Ʈ��

    Vector3 pos;//����� ó����ġ 

    float delta = 0.5f;//����
    [SerializeField]
    float spead = 3f;//���ǵ� 
    [SerializeField]
    bool in_check = false;//üũ�� 

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        check();
    }

    // Update is called once per frame

    public void check()//���� üũ�� 
    {
        SpriteRenderer sprite_check = gameObject.GetComponent<SpriteRenderer>();
        switch (monster_data_temp.monster_statuse)
        {
            case 0:
                sprite_check.sprite = sprite_temp[0];
                StopAllCoroutines();
                StartCoroutine("idle");//�⺻
                break;
            case 1:
                sprite_check.sprite = sprite_temp[1];
                StopAllCoroutines();
                StartCoroutine("atck");//����     
                break;
            case 2:
                sprite_check.sprite = sprite_temp[2];
                StopAllCoroutines();
                StartCoroutine("dmg");//����
                break;
        }
    }
    IEnumerator idle() //�⺻ 
    {
        SpriteRenderer sprite_idle = gameObject.GetComponent<SpriteRenderer>();
        while (monster_data_temp.monster_statuse == 0)
        {
            
            Vector3 v_pos = pos;
            v_pos.y = pos.y - delta * Mathf.Sin(Time.time * spead);
            if (transform.position.y <= -1.5f)
            {
                Debug.Log("������ 2��° ���");
                sprite_idle.sprite = sprite_temp[0];
            }
            else if (transform.position.y >= -0.6f)
            {
                sprite_idle.sprite = sprite_temp[2];
            }
            transform.position = v_pos;
            yield return new WaitForFixedUpdate();
        }
        check();
    }
    IEnumerator atck() //����
    {
        effect_obj.SetActive(true);
        yield return new WaitForSeconds(1f);
        GameObject efect_temp = Instantiate(atck_obj);
        efect_temp.GetComponent<slime_atck_modul>().trun_Manager = monster_data_temp.trun_manager_temp;
        Debug.Log(monster_data_temp.monster_skill_dmg);
        yield return new WaitForSeconds(0.5f);
        effect_obj.SetActive(false);
        monster_data_temp.monster_statuse = 0;
        check();
    }
    IEnumerator dmg() // ������ ����
    {
        float dmg_x_postion = transform.position.x + 1.3f;
        while (!in_check)//�ڷ� �з����� ��ũ��Ʈ
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
        monster_data_temp.monster_cooltiem = 0;
        check();
    }
}
