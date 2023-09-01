using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver2 : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(8);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(4);
    }

    public void Exit()
    {
        Application.Quit();
    }
}