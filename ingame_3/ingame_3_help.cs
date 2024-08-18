using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingame_3_help : MonoBehaviour
{
    public helper_main_ai help_temp;//help 메인 ai
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
        string text_index = " 여기서는 체력을 회복할수 잇어!";
        help_temp.set_text_and_anim(text_index, 3,4);
        help_temp.trun_off_helper();
    }
}
