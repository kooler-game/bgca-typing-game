using UnityEngine;

public static class ScoreManager
{
    public static float[] rankingWPM = { 0, 0, 0, 0, 0 };

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

        for (int i = 0; i < rankingWPM.Length; i++)
        {
            Debug.Log(i + ": " + rankingWPM[i]);
        }
    }
}
