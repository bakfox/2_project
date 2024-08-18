using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class job_spawn : MonoBehaviour
{
    public GameObject[] job_obj;// 직업 오브젝트
    [SerializeField]
    List<int> job_id = new List<int>() {0,1,2};
    Vector3 vector_temp;
    save_sc save_temp;
    // Start is called before the first frame update
    void Start()
    {
        save_temp =gameObject.GetComponent<save_sc>();
        spwan_job();
    }

    void spwan_job()
    {
        switch(save_temp.save_data.clear_stage)
        {
            case 0:
                job_obj[0].SetActive(true);
                break;
            case 1:
                job_obj[1].SetActive(true);
                job_obj[2].SetActive(true);

                for (int i = 1; i < 3; i++)
                {
                    int rand = Random.Range(0, job_id.Count);
                    job_obj[i].GetComponent<job_select_sc>().this_obj_job = job_id[rand];
                    job_obj[i].GetComponent<job_select_sc>().sound_temp = this.GetComponent<sound_manager>();
                    job_id.RemoveAt(rand);
                    
                }
                break;
            case 2:
                job_obj[1].SetActive(true);
                job_obj[2].SetActive(true);

                for (int i = 1; i < 3; i++)
                {
                    int rand = Random.Range(0, job_id.Count);
                    job_obj[i].GetComponent<job_select_sc>().this_obj_job = job_id[rand];
                    job_id.RemoveAt(rand);

                }
                break;
            default:
                job_obj[0].SetActive(true);
                break;

        }
    }

}
