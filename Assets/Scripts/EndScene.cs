/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: EndScene.cs
 Last Modified: December 12th, 2021
 Description: EndScene's UI Behaviour
 Version History: v1.03 Loads data from the static class and display them on the screen. Also added internal documentation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public Text ResultText;
    public Text FinalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateResult();
    }

    void UpdateResult()
    {
        if (GameOverResult.Instance().getDidPlayerWin())
        {
            ResultText.text = "YOU WIN!";
        }
        else
        {
            ResultText.text = "You lost...";
        }

        FinalScoreText.text = "Final Score: " + GameOverResult.Instance().getPlayerScore();
    }
}
