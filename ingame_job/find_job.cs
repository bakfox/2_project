using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class find_job : MonoBehaviour
{

    public TextMeshProUGUI job_text_s;//설명
    public TextMeshProUGUI job_text_j;//직업
    public GameObject actvit_obj;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void set_job_data(string s_temp_s, string s_temp_j)
    {
        on_data_ui();

        job_text_s.SetText("" + s_temp_s + "");
        job_text_j.SetText("직업 : " + s_temp_j);

    }
    public void on_data_ui()
    {
        if (!trun_manager.game_stop)
        {
            actvit_obj.SetActive(true);
        }

    }
    public void off_data_ui()
    {
        actvit_obj.SetActive(false);
    }

}
