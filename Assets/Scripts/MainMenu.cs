using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void LevelSelect() 
    {
        SceneManager.LoadScene(5);
    }

    public void Leaderboard() 
    {
        SceneManager.LoadScene(11);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
