using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_ui_sc : MonoBehaviour
{
    public GameObject stop_uI_obj;
    bool game_stop = false;
    public sound_manager sound_temp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (game_stop)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()//계속하기
    {
        stop_uI_obj.SetActive(false);
        game_stop = false;
    }

    public void Pause()//esc누르면
    {
        stop_uI_obj.SetActive(true);
        game_stop = true;
    }
    public void main_menu()
    {
        sound_temp.change_main();
        load_manager.LoadScene_fast("main_ui");
    }
}
