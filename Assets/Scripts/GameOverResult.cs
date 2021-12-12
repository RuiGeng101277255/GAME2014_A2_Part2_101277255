using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverResult : MonoBehaviour
{
    private static GameOverResult _Instance;
    private static int PlayerScore;
    private static bool PlayerWin;

    public static GameOverResult Instance()
    {
        if (_Instance == null)
        {
            _Instance = new GameOverResult();
        }

        return _Instance;
    }

    public void setResultStats(int score, bool playerwon)
    {
        PlayerScore = score;
        PlayerWin = playerwon;
    }

    public int getPlayerScore()
    {
        return PlayerScore;
    }

    public bool getDidPlayerWin()
    {
        return PlayerWin;
    }
}
