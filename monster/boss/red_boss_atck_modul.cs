using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red_boss_atck_modul : MonoBehaviour
{
    public GameObject player_actk;
    public Vector3 defolt_postion;// 손 위치

    public float atck_shootspead;
    public float time_max;
    public float time_now;
    Vector3[] trun_point = new Vector3[4];

    public trun_manager trun_manager_temp;// 템프 
    // Start is called before the first frame update
    void Start()
    {
        Invoke("in_setting",0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Animator anim_temp = gameObject.GetComponent<Animator>();
            anim_temp.SetTrigger("boom");
            StartCoroutine("hit");
        }
    }
    IEnumerator atck_modul()
    {
        while (time_now < time_max)
        {
            // 경과 시간 계산.
            time_now += Time.deltaTime * atck_shootspead;

            // X,Y,Z 좌표 얻기.
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
        float t = time_now / time_max; //현재 경과 시간 최대 시간

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
    void in_setting()// 기본 설정 
    {
        player_actk = trun_manager_temp.player_obj;

        time_max = Random.Range(0.8f, 1f);
        //particle_temp.Stop();
        trun_point[0] = defolt_postion;
        trun_point[1] = defolt_postion +
            (Random.Range(-1.0f, 1.0f) * transform.right) + // X 좌 우
            (Random.Range(-0.15f, 1.0f) * transform.up) + // Y 아래쪽 위쪽 
            (Random.Range(-1.0f, -0.8f) * transform.forward); // Z 뒤

        // 랜덤 포인트 지정.
        trun_point[2] = player_actk.transform.position +
            (Random.Range(-1.0f, 1.0f) * transform.right) + // X 좌 우 
            (Random.Range(-1.0f, 1.0f) * transform.up) + // Y 위 아래
            (Random.Range(0.8f, 1.0f) * transform.forward); // Z 앞 
        trun_point[3] = player_actk.transform.position;

        StartCoroutine("atck_modul");
    }
    IEnumerator hit()
    {
        Debug.Log("공격 성공");
        trun_manager_temp.re_coltime_monster();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
