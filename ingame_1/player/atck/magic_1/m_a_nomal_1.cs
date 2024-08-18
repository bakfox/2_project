using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_a_nomal_1 : MonoBehaviour
{
    public float skill_dmg = 5f;//스킬 데미지
    public int atck_vaalue;// 공격 종류

    public Vector3 now_position;// 기본 위치 
    public GameObject hit_obj;//터지는 이펙트 
    atck_target atck_target_temp;//타겟 받아오기 
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hit");
        in_setting();

        StartCoroutine("hit");
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator hit()
    {
        transform.position = atck_target_temp.target_obj.transform.position;
        GameObject hit_temp = Instantiate(hit_obj);
        yield return new WaitForSeconds(0.5f);
        hit_temp.GetComponent<hit_sc>().now_transfrom = atck_target_temp.target_obj.transform.position;
        atck_target_temp.target_obj.GetComponent<monster_data>().hit_value = atck_vaalue;
        atck_target_temp.target_obj.GetComponent<monster_data>().skill_dmg_temp = skill_dmg;
        atck_target_temp.target_obj.GetComponent<monster_data>().StartCoroutine("hit_anim");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        StopCoroutine("hit");
    }
    void in_setting()// 기본 설정 
    {
        atck_target_temp = gameObject.GetComponent<atck_target>();
    }
}
