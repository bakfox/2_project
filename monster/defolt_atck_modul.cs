using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defolt_atck_modul : MonoBehaviour
{
    //���� �뵵
    public trun_manager trun_Manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine("hit");
        }
    }
    IEnumerator hit()
    {
        Debug.Log("���� ����");
        trun_Manager.re_coltime_monster();
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
