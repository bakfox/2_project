using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hp_up_effect : MonoBehaviour
{
    public TextMeshProUGUI hp_textmesh;//�Ž� 
    save_sc save_sc_temp;//���� ������ 
    float hp_float_temp;//hp
    //
    // Start is called before the first frame update
    void Start()
    {
       
    }

    IEnumerator hp_first()//hp �۵� ��ƾ 
    {
        hp_textmesh.SetText("+ " + hp_float_temp);
        Debug.Log(""+ hp_float_temp);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        StopCoroutine("hp_first");
    }
    public void heal_to_start(float f_index)//Ȱ��ȭ 
    {
        save_sc_temp = save_sc.find_save_sc();
        hp_float_temp = f_index;
        gameObject.SetActive(true);
        StartCoroutine("hp_first");
        
    }
}
