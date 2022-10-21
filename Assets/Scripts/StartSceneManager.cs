using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private string level1Scene = "PacStudent";
    public void StartGame()
    {
        SceneManager.LoadScene(level1Scene, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
