using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingame_3_help : MonoBehaviour
{
    public helper_main_ai help_temp;//help ���� ai
    // Start is called before the first frame update
    void Start()
    {
        change_text();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void change_text()
    {
        string text_index = " ���⼭�� ü���� ȸ���Ҽ� �վ�!";
        help_temp.set_text_and_anim(text_index, 3,4);
        help_temp.trun_off_helper();
    }
}
