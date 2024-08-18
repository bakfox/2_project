using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_boss_anim : MonoBehaviour
{
    public monster_data monster_data_temp;
    public Sprite[] sprite_temp;//�ٲ� ���� ������Ʈ
    public GameObject atck_obj;

    Vector3 pos;//����� ó����ġ 

    float delta = 0.5f;//����
    [SerializeField]
    float spead = 3f;//���ǵ� 
    [SerializeField]
    bool in_check = false;//üũ�� 
    public blue_boss_hart_modul hart_modul_temp;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        check();
    }

    // Update is called once per frame

    public void check()//���� üũ�� 
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        switch (monster_data_temp.monster_statuse)
        {
            case 0:
                sprite.sprite = sprite_temp[0];
                StopAllCoroutines();
                StartCoroutine("idle");//�⺻
                break;
            case 1:
                sprite.sprite = sprite_temp[0];
                StopAllCoroutines();
                StartCoroutine("atck");//����
                break;
            case 2:
                sprite.sprite = sprite_temp[1];
                StopAllCoroutines();
                StartCoroutine("dmg");//����
                break;
        }
    }
    IEnumerator idle() //�⺻ 
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
    IEnumerator atck() //����
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_temp[2];
        GameObject efect_temp = Instantiate(atck_obj);
        efect_temp.GetComponent<defolt_atck_modul>().trun_Manager = monster_data_temp.trun_manager_temp;
        Debug.Log(monster_data_temp.monster_skill_dmg);
        yield return new WaitForSeconds(0.5f);
        monster_data_temp.monster_statuse = 0;
        check();
    }
    IEnumerator dmg() // ������ ����
    {
        if (hart_modul_temp.hart_gard_broken == false)
        {
            blue_heal();
        }
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
        check();
    }
    public void blue_heal()// ������ ������ ������ �� 
    {
        monster_data_temp.monster_hp += monster_data_temp.monster_max_hp * 0.2f;
        if (monster_data_temp.monster_hp > monster_data_temp.monster_max_hp)
        {
            monster_data_temp.monster_hp = monster_data_temp.monster_max_hp;
        }
        monster_data_temp.set_hp();
        hart_modul_temp.start_cooltime();
    }
}
