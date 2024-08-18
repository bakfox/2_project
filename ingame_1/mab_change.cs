using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mab_change : MonoBehaviour
{
    [SerializeField]
    GameObject[] mab_objs;
    save_sc save_sc_temp;
    // Start is called before the first frame update
    void Start()
    {
        save_sc_temp =  gameObject.GetComponent<save_sc>();
        one_change_map();
    }

    void one_change_map()
    {
        switch (save_sc_temp.save_data.user_now_floor)
        {
            case 1:
                mab_objs[0].SetActive(true);
                mab_objs[1].SetActive(false);
                break;
            case 2:
                mab_objs[1].SetActive(true);
                mab_objs[0].SetActive(false);
                break;
        }

    }
}
