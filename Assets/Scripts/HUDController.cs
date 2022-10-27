using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private string startScene = "StartScene";
    public Text scareTimer;
    public Text scareTime;
    private void Start()
    {
        scareTimer.enabled = false;
        scareTime.enabled = false;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(startScene, LoadSceneMode.Single);
    }
}
