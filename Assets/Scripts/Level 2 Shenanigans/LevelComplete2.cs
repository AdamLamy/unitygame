using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete2 : MonoBehaviour
{
    public void NextLevel()
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