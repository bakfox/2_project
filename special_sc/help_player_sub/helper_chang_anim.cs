using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helper_chang_anim : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sp_render;// ���� ����
    [SerializeField]
    GameObject eye_obj;//�� ������Ʈ 

    public Sprite[] sprite_body;//���� �⺻ , ���� , ���� , ������ , ����
    public Sprite[] sprite_eye;// ���� �⺻ , °������ , ���� , ����
    [SerializeField]
    helper_main_ai helper_main_temp;

    Vector3 eye_pos = new Vector3(0.2f,0.75f,-0.043f);
    Vector3 eye_defolt_pos = new Vector3(0f, 0.6f, -0.043f);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changer_anim()
    {
        sp_render.sprite = sprite_body[helper_main_temp.body_statuse];
        SpriteRenderer sp_eye = eye_obj.GetComponent<SpriteRenderer>();
        sp_eye.sprite = sprite_eye[helper_main_temp.eye_statuse];
        if (helper_main_temp.eye_statuse == 0 || helper_main_temp.eye_statuse == 1)
        {
            eye_obj.transform.localPosition = eye_defolt_pos;
        }
        else
        {
            eye_obj.transform.localPosition = eye_pos;
        }
        
    }
}
