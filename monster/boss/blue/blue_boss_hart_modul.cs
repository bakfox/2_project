using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class blue_boss_hart_modul : MonoBehaviour
{
    public bool hart_gard_broken = false;//하트 막는거 부서짐

    public blue_boss_anim blue_boss_temp;

    float monster_cooltiem;
    [SerializeField]
    float hart_spead;

    public GameObject blue_hart_effect;
    public GameObject hart_gard_obj;
    public GameObject cooltime_obj;
    public TextMeshProUGUI text_cooltime;
    public void start_cooltime()
    {
        StartCoroutine("hart_coltiem");
    } 
    IEnumerator hart_coltiem()
    {
        hart_gard_broken = true;
        blue_hart_effect.SetActive(true);
        cooltime_obj.SetActive(true);
        hart_gard_obj.SetActive(false);

        yield return new WaitForSeconds(0.8f);
        blue_hart_effect.SetActive(false);
        monster_cooltiem = hart_spead;
        while (monster_cooltiem > 0.0f)
        {
            if (!trun_manager.game_stop)
            {
                monster_cooltiem -= Time.deltaTime;
                int cooltiem_int = Mathf.FloorToInt(monster_cooltiem);
                cooltime_obj.GetComponent<Image>().fillAmount = monster_cooltiem / hart_spead;
                if (cooltiem_int >= 0)
                {
                    text_cooltime.SetText("" + cooltiem_int + "");
                }
            }

            yield return new WaitForFixedUpdate();
        }

        hart_gard_broken = false;
        cooltime_obj.SetActive (false);
        hart_gard_obj.SetActive(true);
        StopCoroutine("hart_coltiem");
    }
}
