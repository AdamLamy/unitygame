using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public string previousScene;
    public void Level1() 
    {
        SceneManager.LoadScene(1);
    }

    public void Level2() 
    {
        sceneCheck.previousscene = true;

        SceneManager.LoadScene(8);
    }

    public void Level3() 
    {
        SceneManager.LoadScene(13);
    }

    public void back() 
    {
        SceneManager.LoadScene(4);
    }

    public void Exit() 
    {
       Application.Quit(); 
    }
}
