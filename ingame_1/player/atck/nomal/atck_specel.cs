using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atck_specel : MonoBehaviour
{
    public float skill_dmg = 5f;//��ų ������
    public int atck_vaalue;// ���� ����

    public Vector3 now_position;// �⺻ ��ġ 
    public GameObject hit_obj;//������ ����Ʈ 
    atck_target atck_target_temp;//Ÿ�� �޾ƿ��� 
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
        transform.position = new Vector3(atck_target_temp.target_obj.transform.position.x, atck_target_temp.target_obj.transform.position.y+1f, atck_target_temp.target_obj.transform.position.z);
        GameObject hit_temp = Instantiate(hit_obj);
        hit_temp.GetComponent<hit_sc>().now_transfrom = atck_target_temp.target_obj.transform.position;
        yield return new WaitForSeconds(0.7f);
        atck_target_temp.target_obj.GetComponent<monster_data>().hit_value = atck_vaalue;
        atck_target_temp.target_obj.GetComponent<monster_data>().skill_dmg_temp = skill_dmg;
        atck_target_temp.target_obj.GetComponent<monster_data>().StartCoroutine("hit_anim");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        StopCoroutine("hit");
    }
    void in_setting()// �⺻ ���� 
    {
        atck_target_temp = gameObject.GetComponent<atck_target>();
    }
}
