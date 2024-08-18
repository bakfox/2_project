using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class text_effect : MonoBehaviour
{
    public helper_main_ai help_main_temp;//����� 
    public TextMeshProUGUI text_mesh;// �ؽ�Ʈ �Ž� 
    public AudioSource audio_temp;// ����� �ҽ�

    string help_text_temp;//�����

    public bool check_text_set = false;

    public GameObject text_panel_obj;
    int text_index;// �ε��� �� 
    [SerializeField]
    float charspead = 10f;// ������ �ӵ� 

    private void Start()
    {
        audio_temp.Stop();
    }
    public void set_msg(string msg_temp)
    {
        StopCoroutine("effect_end");
        help_text_temp = msg_temp;
        effect_start();
    }
    void effect_start()
    {   
        text_mesh.text = "";//�ʱ�ȭ
        text_index = 0;

        Invoke("effecting", charspead);
    }
    void effecting()
    {
        if (text_mesh.text == help_text_temp)
        {

            StartCoroutine("effect_end");
            return;
        }

        text_mesh.text += help_text_temp[text_index];

        if (help_text_temp[text_index] != ' ' || help_text_temp[text_index] != '.')
        {
            audio_temp.Play();
        }

        text_index++;

        Invoke("effecting",charspead);
    }
    IEnumerator effect_end()
    {
        help_main_temp.text_end = true;
        yield return new WaitForSeconds(2f);
        text_panel_obj.SetActive(false);
    }



}
