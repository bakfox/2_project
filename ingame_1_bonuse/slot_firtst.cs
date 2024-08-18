using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slot_firtst : MonoBehaviour
{
    [SerializeField]
    GameObject[] card_obj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("card_open");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator card_open()
    {
        yield return new WaitForSeconds(0.5f);
        card_obj[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        card_obj[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        card_obj[2].SetActive(true);
        StopCoroutine("card_open");
    }
    public void end_card()
    {
        StopCoroutine("card_open");
        card_obj[0].SetActive(false);
        card_obj[1].SetActive(false);
        card_obj[2].SetActive(false);
    }
}
