using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoginMenu : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public Button playButton;

    void Update()
    {
        playButton.interactable = nameField.text.Length >= 8 && passwordField.text.Length >= 8;
        
    }

    public void Lback()
    {
        SceneManager.LoadScene(0);
    }

    public void Lexit()
    {
        Application.Quit();
    }

    public void Lnext()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form))
        {
            yield return www.SendWebRequest();
            if (www.downloadHandler.text.Split('\t')[0] == "0")
            {
                DBManager.username = nameField.text;
                DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[1]);
                UnityEngine.SceneManagement.SceneManager.LoadScene(4);
            }
            else
            {
                Debug.Log("User Login failed, error code " + www.downloadHandler.text);
                UnityEngine.SceneManagement.SceneManager.LoadScene(3);
            }

        }
    }
}
