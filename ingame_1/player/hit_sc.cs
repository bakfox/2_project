using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_sc : MonoBehaviour
{
    public Vector3 now_transfrom;
    // Start is called before the first frame update
    void Start()
    {
        random_chanag();
        Invoke("destroy_obj",0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void random_chanag()
    {
        transform.position = new Vector3(now_transfrom.x, now_transfrom.y, now_transfrom.z);
        
    }
    void destroy_obj()
    {
        DestroyObject(gameObject);
    }
}
