using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_manager : MonoBehaviour
{
    sound_main sound_main_temp;//메인 사운드 
    public AudioClip main_change_audio;
    public AudioClip[] mab_change_audio;
    public AudioClip[] chuse_job_change_audio;

    save_sc save_sc_temp;//세이브 데이터 

    public void Start()
    {
        save_sc_temp = save_sc.find_save_sc();
        sound_main_temp = GameObject.FindGameObjectWithTag("sound").GetComponent<sound_main>();
    }

    public void change_main()
    {
        sound_main_temp.audio_stop();
        sound_main_temp.change_audio(main_change_audio);
        sound_main_temp.audio_play();
    }
    public void change_mab_main()
    {
        if (save_sc_temp.save_data.user_now_floor == 1)
        {
            if (save_sc_temp.save_data.user_now_stage == 10)
            {
                sound_main_temp.audio_stop();
                sound_main_temp.change_audio(mab_change_audio[1]);
                sound_main_temp.audio_play();
            }
            else
            {
                sound_main_temp.audio_stop();
                sound_main_temp.change_audio(mab_change_audio[0]);
                sound_main_temp.audio_play();
            }
        }else if (save_sc_temp.save_data.user_now_floor == 2)
        {
            if (save_sc_temp.save_data.user_now_stage == 10)
            {
                sound_main_temp.audio_stop();
                sound_main_temp.change_audio(mab_change_audio[3]);
                sound_main_temp.audio_play();
            }
            else
            {
                sound_main_temp.audio_stop();
                sound_main_temp.change_audio(mab_change_audio[2]);
                sound_main_temp.audio_play();
            }
        }
        else
        {
            sound_main_temp.audio_stop();
            sound_main_temp.change_audio(main_change_audio);
            sound_main_temp.audio_play();
        }
    }
    public void change_change_job_main()
    {
        if (save_sc_temp.save_data.user_now_floor == 1)
        {
            sound_main_temp.audio_stop();
            sound_main_temp.change_audio(chuse_job_change_audio[0]);
            sound_main_temp.audio_play();
        }
        else if (save_sc_temp.save_data.user_now_floor == 2)
        {
            sound_main_temp.audio_stop();
            sound_main_temp.change_audio(chuse_job_change_audio[1]);
            sound_main_temp.audio_play();
        }
        else
        {
            sound_main_temp.audio_stop();
            sound_main_temp.change_audio(main_change_audio);
            sound_main_temp.audio_play();
        }
    }
}
