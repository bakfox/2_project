using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_manager : MonoBehaviour
{
    public static string nextScene;

    public static void LoadScene_fast(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
