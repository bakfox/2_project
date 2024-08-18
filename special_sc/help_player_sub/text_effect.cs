using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class text_effect : MonoBehaviour
{
    public helper_main_ai help_main_temp;//도우미 
    public TextMeshProUGUI text_mesh;// 텍스트 매쉬 
    public AudioSource audio_temp;// 오디오 소스

    string help_text_temp;//저장용

    public bool check_text_set = false;

    public GameObject text_panel_obj;
    int text_index;// 인덱스 용 
    [SerializeField]
    float charspead = 10f;// 나오는 속도 

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
        text_mesh.text = "";//초기화
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
