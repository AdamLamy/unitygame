using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class RegisterMenu : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;
    public InputField ConfirmPWField;
    public Button registerButton;


    public void Rback()
    {
        SceneManager.LoadScene(0);
    }

    public void Rexit()
    {
        Application.Quit();
    }

    public void Rnext()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form))
        {
            yield return www.SendWebRequest();
            // isNetworkError always comes true, else is to check for logs
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(response);
                Debug.Log("If regiestration successful, please log in!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }
    }

    void Update()
    {
        if (nameField.text.Length >= 8 && passwordField.text.Length >= 8 && passwordField.text == ConfirmPWField.text)
        {
            registerButton.interactable = nameField.text.Length >= 8 && passwordField.text.Length >= 8 && passwordField.text == ConfirmPWField.text;
        }
        else
        {
            registerButton.interactable = false;
        }
    }
}
