using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ingame_1_manager : MonoBehaviour
{
    trun_manager trun_manager_temp;
    // Start is called before the first frame update
    void Start()
    {
        trun_manager_temp = gameObject.GetComponent<trun_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
