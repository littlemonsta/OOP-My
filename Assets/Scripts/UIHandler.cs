using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
   
    public void StartGame()
    {

        SceneManager.LoadScene("Main");
   
    }

    public void TitleScreen()
    {

        SceneManager.LoadScene("Title");

    }

    public void QuitApplication()
    {
        // MainManager.Instance.SaveColor();
#if UNITY_EDITOR

        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
