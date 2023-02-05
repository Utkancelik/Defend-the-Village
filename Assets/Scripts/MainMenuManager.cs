using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        int lastSceneIndex = totalSceneCount - 1;
        int loadedSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int willBeLoadedScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (willBeLoadedScene <= lastSceneIndex)
        {
            SceneManager.LoadScene(willBeLoadedScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}
