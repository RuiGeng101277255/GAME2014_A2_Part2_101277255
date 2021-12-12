using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [Header("Text Components")]
    public Text timerText;
    public float TotalTimeLeft;

    public Text scoreText;
    public Text ammoText;

    [Header("Player Lives")]
    public GameObject[] Lives;

    public PlayerScript Player;

    // Update is called once per frame
    void Update()
    {
        updateText();
        updatePlayerLives();
    }

    void updateText()
    {
        TotalTimeLeft -= Time.deltaTime;
        timerText.text = "Time Left: " + (int)TotalTimeLeft + "s";
        scoreText.text = "Score: " + Player.PlayerScore;
        ammoText.text = "x " + Player.AmmmoCount;
    }

    void updatePlayerLives()
    {
        if (Player.PlayerLive <= 4)
        {
            foreach (GameObject g in Lives)
            {
                g.SetActive(true);
            }

            switch (Player.PlayerLive)
            {
                case 3:
                    Lives[3].SetActive(false);
                    break;
                case 2:
                    Lives[3].SetActive(false);
                    Lives[2].SetActive(false);
                    break;
                case 1:
                    Lives[3].SetActive(false);
                    Lives[2].SetActive(false);
                    Lives[1].SetActive(false);
                    break;
                case 0:
                    foreach (GameObject g in Lives)
                    {
                        g.SetActive(false);
                    }
                    break;
            }
        }
    }
}
