using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private int MaxScore = 100;
    public int TimeScore = 0;
    public double totalSeconds;

    private string sceneName;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "Time: 00:00.00";
        timerGoing = false;
    }

    public void BeginTimer()
    {
        Debug.Log("begun");
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        Debug.Log("Timer Stopped");
        Debug.Log("End of " + sceneName);
        
        if (sceneName == "Level2")
        {
            Scoring2();
        }

        else
        {
            Scoring1();
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }

    //Scoring for level 1
    private void Scoring1()
    {
        Debug.Log(timePlaying);
        totalSeconds = timePlaying.TotalSeconds;
        int value = (int)Math.Ceiling(totalSeconds);
        TimeScore = MaxScore - value;
        if (TimeScore > DBManager.score)
        {
            StartCoroutine(ScoreInsert(TimeScore));
            //int y = TimeScore + DBManager.score;
            Debug.Log(TimeScore);
        }
    }

    //Scoring for level 2
    private void Scoring2()
    {
        Debug.Log(timePlaying);
        totalSeconds = timePlaying.TotalSeconds;
        int value = (int)Math.Ceiling(totalSeconds);
        TimeScore = MaxScore - value;
        int TimeScore2 = (TimeScore * 2);
        if (TimeScore2 > DBManager.score)
        {
            StartCoroutine(ScoreInsert(TimeScore2));
            //Debug.Log("return");
            //int y = (TimeScore + DBManager.score) * 2;
            Debug.Log(TimeScore2);
        }
    }

    private IEnumerator ScoreInsert(int _score)
    {
        Debug.Log("Ienumerator" + _score);
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("score", _score);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form))
        {
            yield return www.SendWebRequest();
            if (www.downloadHandler.text == "0")
            {
                Debug.Log("Score updated successfully!");
                //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[1]);
            }
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
        Debug.Log("Score:" + DBManager.score);
    }
}
