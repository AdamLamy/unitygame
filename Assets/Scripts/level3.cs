using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class level3 : MonoBehaviour
{
    public void back() 
    {
        SceneManager.LoadScene(5);
    }

    public void Exit() 
    {
       Application.Quit(); 
    }
}
