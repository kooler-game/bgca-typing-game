using UnityEngine;

public static class ScoreManager
{
    public readonly static string wpmSaveDataKey = "wpm";
    public static float[] rankingWPM = { 0, 0, 0, 0, 0 };

    static ScoreManager()
    {
        rankingWPM = LoadScore();
        Debug.Log("Constructor");
        PrintScore(rankingWPM);
    }

    public static void EndGame(float wpm)
    {
        for (int i = 0; i < rankingWPM.Length; i++)
        {
            if (rankingWPM[i] < wpm)
            {
                // shift array right by 1 and record current
                if (i < rankingWPM.Length - 1)
                {
                    for (int j = i + 1; j < rankingWPM.Length; j++)
                    {
                        rankingWPM[j] = rankingWPM[j - 1];
                    }
                }
                rankingWPM[i] = wpm;
                break;
            }
        }

        PrintScore(rankingWPM);
        SaveScore(rankingWPM);
    }

    private static void SaveScore(float[] wpms)
    {
        SaveData saveData = new SaveData
        {
            wpms = wpms
        };

        PlayerPrefs.SetString(wpmSaveDataKey, saveData.ToJson());
    }

    private static float[] LoadScore()
    {
        string saveDataJsonString = PlayerPrefs.GetString(wpmSaveDataKey);
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveDataJsonString);
        Debug.Log("Load Score");
        PrintScore(saveData.wpms);
        return saveData.wpms;
    }


    // For Debug
    private static void PrintScore(float[] wpms)
    {
        string message = "";
        message += "WPM\n";
        for (int i = 0; i < wpms.Length; i++)
        {
            message += i + ": " + wpms[i] + "\n";
        }
        Debug.Log(message);
    }
}

class SaveData
{
    public float[] wpms = { 0, 0, 0, 0, 0 };

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static SaveData FromJson(string json)
    {
        return JsonUtility.FromJson<SaveData>(json);
    }
}