using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_data_base : MonoBehaviour
{
    public GameObject[] monster_obj;//±âº»¸÷
    public GameObject[] boss_monster;//º¸½º¸÷ 

    public GameObject retun_monster_obj(int monster_id)
    {
        return monster_obj[monster_id];

    }
    public GameObject return_boss_obj(int monster_id)
    {
        return boss_monster[monster_id];
    }
}
