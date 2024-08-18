using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class start_manager : MonoBehaviour
{
    public TextMeshProUGUI start_text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("change");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StopCoroutine("change");
            load_manager.LoadScene_fast("main_ui");
        }
    }
    IEnumerator change()
    {
        bool test = false;
        while (!test)
        {
            start_text.SetText("화면을 클릭해주세요");
            yield return new WaitForSeconds(0.5f);
            start_text.SetText(".화면을 클릭해주세요.");
            yield return new WaitForSeconds(0.5f);
            start_text.SetText("..화면을 클릭해주세요..");
            yield return new WaitForSeconds(0.5f);
            start_text.SetText("...화면을 클릭해주세요...");
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
}
