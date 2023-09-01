using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    public enum CurrentWindow { Login, Register, Leaderboard }
    public CurrentWindow currentWindow;
    Vector2 leaderboardScroll = Vector2.zero;
    bool showLeaderboard = true;
    int currentScore = 0; 
    int previousScore = 0;
    int highestScore = 0;
    int playerRank = -1;
    [System.Serializable]
    public class LeaderboardUser
    {
        public string username;
        public int score;
    }
    LeaderboardUser[] leaderboardUsers;

    bool isWorking = true;
    private void Start() {
        StartCoroutine(GetLeaderboard());
    }

    //Logged-in user data
    IEnumerator GetLeaderboard()
    {
        isWorking = true;

        WWWForm form = new WWWForm();

        form.AddField("username", DBManager.username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/leaderboard.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log(responseText);
                if (responseText.StartsWith("User"))
                {
                    string[] dataChunks = responseText.Split('|');
                    //Retrieve player score and rank
                    if (dataChunks[0].Contains(","))
                    {
                        string[] tmp = dataChunks[0].Split(',');
                        highestScore = int.Parse(tmp[1]);
                        playerRank = int.Parse(tmp[2]);
                    }
                    else
                    {
                        highestScore = 0;
                        playerRank = -1;
                    }

                    //Retrieve player leaderboard
                    leaderboardUsers = new LeaderboardUser[dataChunks.Length - 1];
                    for (int i = 1; i < dataChunks.Length; i++)
                    {
                        string[] tmp = dataChunks[i].Split(',');
                        LeaderboardUser user = new LeaderboardUser();
                        user.username = tmp[0];
                        user.score = int.Parse(tmp[1]);
                        leaderboardUsers[i - 1] = user;
                    }

                    for (int i = 0; i < leaderboardUsers.Length; i++)
                    {
                        var item = leaderboardUsers[i];
                        var currentIndex = i;

                        while (currentIndex > 0 && leaderboardUsers[currentIndex - 1].score < item.score)
                        {
                            leaderboardUsers[currentIndex] = leaderboardUsers[currentIndex - 1];
                            currentIndex--;
                        }

                        leaderboardUsers[currentIndex] = item;
                    }
                }
                else
                {
                    print(responseText);
                }
            }
        }

        isWorking = false;
    }

    void OnGUI()
    {
        if (showLeaderboard)
        {
            GUI.Window(1, new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 450), LeaderboardWindow, "Leaderboard");
        }
        if (!DBManager.LoggedIn)
        {
            showLeaderboard = false;
            currentScore = 0;
        }
        else
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, 5, 120, 25), currentScore.ToString());

            if (GUI.Button(new Rect(5, 60, 100, 25), "Leaderboard"))
            {
                showLeaderboard = !showLeaderboard;
                if (!isWorking)
                {
                    StartCoroutine(GetLeaderboard());
                }
            }
        }
    }
    void LeaderboardWindow(int index)
    {
        if (isWorking)
        {
            GUILayout.Label("Loading...");
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUI.color = Color.green;
            GUILayout.Label("Your Rank: " + (playerRank > 0 ? playerRank.ToString() : "Not ranked yet"));
            GUILayout.Label("Highest Score: " + highestScore.ToString());
            GUI.color = Color.white;
            GUILayout.EndHorizontal();

            leaderboardScroll = GUILayout.BeginScrollView(leaderboardScroll, false, true);

            for (int i = 0; i < leaderboardUsers.Length; i++)
            {
                GUILayout.BeginHorizontal("box");
                if (leaderboardUsers[i].username == DBManager.username)
                {
                    GUI.color = Color.green;
                }
                GUILayout.Label((i + 1).ToString(), GUILayout.Width(30));
                GUILayout.Label(leaderboardUsers[i].username, GUILayout.Width(230));
                GUILayout.Label(leaderboardUsers[i].score.ToString());
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
    }
}