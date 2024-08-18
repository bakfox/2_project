using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candle_atck_modul : MonoBehaviour
{
    //몬스터 용도
    public trun_manager trun_Manager;
    public new Vector3 position_temp;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(position_temp.x, position_temp.y + 1.4f, position_temp.z - 0.29f);
        StartCoroutine("hit");
    }
    IEnumerator hit()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("공격 성공");
        trun_Manager.re_coltime_monster();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
