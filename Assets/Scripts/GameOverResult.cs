/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: GameOverResult.cs
 Last Modified: December 12th, 2021
 Description: Singleton used for transfering the player's game state and score from play scene to end scene
 Version History: v1.02 Full functionality and internal documentation
 */

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
