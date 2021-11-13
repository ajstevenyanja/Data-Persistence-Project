using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string currUserName;
    public int currUserScore;
    public string highscoreUsername;
    public int highscoreUserScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Length retuning 1 when this string is null
        // A null string in C# is not the same as an empty string.
        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null
        if (currUserName.Length == 1)
        {
            currUserName = "Name";
            currUserScore = 0;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // data persistence over sessions
    [System.Serializable]
    class SaveData
    {
        public string Username;
        public int UserScore;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.Username = currUserName;
        data.UserScore = currUserScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "break_savefile.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "break_savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscoreUsername = data.Username;
            highscoreUserScore = data.UserScore;
        }
    }    
}
