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
