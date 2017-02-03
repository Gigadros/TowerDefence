using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{

    public GameObject loadingImage;

    public void LoadScene(string sceneName)
    {
        loadingImage.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
