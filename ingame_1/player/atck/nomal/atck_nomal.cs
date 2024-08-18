using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class atck_nomal : MonoBehaviour
{ 
    public Vector3 defolt_positon;//�⺻ ��ġ 
    public ParticleSystem particle_temp; // �ڱ� ��ƼŬ ������ 
    public GameObject hit_obj; //������ ȿ�� ������Ʈ 
    atck_target atck_target_temp;

    public float skill_dmg = 2f;
    public int atck_value = 0;// 0 ���� 1 ���� ������ ���� 
    public float atck_shootspead;
    public float time_max;
    public float time_now;
    Vector3[] trun_point = new Vector3[4];// � ǥ���ϱ� ���� ��ġ  

    int check_atck_bat = 1;//Ÿ�� 
    int trigger_check = 0;//Ÿ�� üũ
    // Start is called before the first frame update
    void Start()
    {
        in_setting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            GameObject obj_temp = other.gameObject;
            Debug.Log(obj_temp.name);
            if (obj_temp.name == gameObject.GetComponent<atck_target>().target_obj.name)
            {
                if (trigger_check < check_atck_bat)
                {
                    StartCoroutine("hit");
                    trigger_check++;
                }
            }
           
        }
    }
    IEnumerator hit()
    {
        GameObject hit_temp = Instantiate(hit_obj);
        hit_temp.GetComponent<hit_sc>().now_transfrom = atck_target_temp.target_obj.transform.position;
        atck_target_temp.target_obj.GetComponent<monster_data>().hit_value = atck_value;
        atck_target_temp.target_obj.GetComponent<monster_data>().skill_dmg_temp = skill_dmg;
        atck_target_temp.target_obj.GetComponent<monster_data>().StartCoroutine("hit_anim");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        StopCoroutine("hit");
    }
    IEnumerator atck_modul()
    {
        while (time_now < time_max)
        {
            // ��� �ð� ���.
            time_now += Time.deltaTime * atck_shootspead;

            // X,Y,Z ��ǥ ���.
            transform.position = new Vector3(
                curve(trun_point[0].x, trun_point[1].x, trun_point[2].x, trun_point[3].x),
                curve(trun_point[0].y, trun_point[1].y, trun_point[2].y, trun_point[3].y),
                curve(trun_point[0].z, trun_point[1].z, trun_point[2].z, trun_point[3].z)
            );
            yield return new WaitForFixedUpdate();
        }
    }
    float curve(float a, float b, float c, float d)
    {
        float t = time_now / time_max; //���� ��� �ð� �ִ� �ð�

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
    void in_setting()// �⺻ ���� 
    {
        atck_target_temp = gameObject.GetComponent<atck_target>();

        time_max = Random.Range(0.8f, 1f);
        //particle_temp.Stop();
        trun_point[0] = defolt_positon;
        trun_point[1] = defolt_positon +
            (Random.Range(-1.0f, 1.0f) * transform.right) + // X �� ��
            (Random.Range(-0.15f, 1.0f) * transform.up) + // Y �Ʒ��� ���� 
            (Random.Range(-1.0f, -0.8f) * transform.forward); // Z ��

        // ���� ����Ʈ ����.
        trun_point[2] = atck_target_temp.target_obj.transform.position +
            (Random.Range(-1.0f, 1.0f) * transform.right) + // X �� �� 
            (Random.Range(-1.0f, 1.0f) * transform.up) + // Y �� �Ʒ�
            (Random.Range(0.8f, 1.0f) * transform.forward); // Z �� 
        trun_point[3] = atck_target_temp.target_obj.transform.position;

       StartCoroutine("atck_modul");
    }
}
