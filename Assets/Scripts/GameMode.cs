using System;

public enum GameMode
{
    TIME,
    COUNT
}

public abstract class ModeManager
{
    public static ModeManager CreateModelManager(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.TIME:
                return new TimeGameModeManager();
            case GameMode.COUNT:
                return new CountGameModeManager();
            default:
                throw new NotImplementedException();
        }
    }

    public abstract bool IsGameOver(GameProgress progress, int condition);

    public abstract string GetTimerText(int current, int target);
    public abstract string GetScoreText(int current, int target);
}

public class CountGameModeManager : ModeManager
{
    public override bool IsGameOver(GameProgress progress, int targetWord)
    {
        return progress.wordTyped >= targetWord;
    }

    public override string GetTimerText(int current, int target)
    {
        return current.ToString();
    }

    public override string GetScoreText(int current, int target)
    {
        return current + "/" + target + "s";
    }
}

public class TimeGameModeManager : ModeManager
{
    public override bool IsGameOver(GameProgress progress, int tagretTime)
    {
        return progress.usedSeconds >= tagretTime;
    }

    public override string GetTimerText(int current, int target)
    {
        return current + "/" + target + "s";
    }

    public override string GetScoreText(int current, int target)
    {
        return current.ToString();
    }
}