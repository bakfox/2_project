using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defolt_monster_anim : MonoBehaviour
{
    monster_data monster_data_temp;

    // Start is called before the first frame update
    void Start()
    {
        monster_data_temp = gameObject.AddComponent<monster_data>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void check()//상태 체크용 
    {
        switch (monster_data_temp.monster_statuse)
        {
            case 0:
                StopAllCoroutines();
                StartCoroutine("idle");//기본
                break;
            case 1:
                StopAllCoroutines();
                StartCoroutine("atck");//공격
                break;
            case 2:
                StopAllCoroutines();
                StartCoroutine("dmg");//맞음
                break;
        }
    }
    IEnumerator idle()
    {
        yield return new WaitForFixedUpdate();
    }
    IEnumerator atck()
    {
        yield return new WaitForFixedUpdate();

    }
    IEnumerator dmg()
    {
        yield return new WaitForFixedUpdate();
    }
}
