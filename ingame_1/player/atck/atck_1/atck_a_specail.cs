using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atck_a_specail : MonoBehaviour
{
    public float skill_dmg = 5f;//��ų ������
    public int atck_vaalue;// ���� ����

    public GameObject hit_obj;//������ ����Ʈ 
    GameObject monster_obj;//���� obj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator hit()
    {
        GameObject hit_temp = Instantiate(hit_obj);
        hit_temp.GetComponent<hit_sc>().now_transfrom = monster_obj.transform.position;
        monster_obj.GetComponent<monster_data>().hit_value = atck_vaalue;
        monster_obj.GetComponent<monster_data>().skill_dmg_temp = skill_dmg;
        monster_obj.GetComponent<monster_data>().StartCoroutine("hit_anim");
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        StopCoroutine("hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���� ����");
        if (other.CompareTag("monster"))
        {
            monster_obj = other.gameObject;
            StartCoroutine("hit");
        }
    }
}
