using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{

    public void Login() 
    {
        SceneManager.LoadScene(3);
    }

    public void Register() 
    {
        SceneManager.LoadScene(2);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
