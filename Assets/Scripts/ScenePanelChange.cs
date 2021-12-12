/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: ScenePanelChange.cs
 Last Modified: November 21st, 2021
 Description: Changes from start to instruction, gameplay, and end panels
 Version History: v1.02 - Loads scene
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePanelChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openScene(string sceneName)
    {
        if (sceneName != "quit")
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            Application.Quit();
        }
    }
}
