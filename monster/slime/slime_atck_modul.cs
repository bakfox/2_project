using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_atck_modul : MonoBehaviour
{
    public trun_manager trun_Manager;
    // Start is called before the first frame update
    void Start()
    {
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
