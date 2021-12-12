/*
 Full Name: Rui Chen Geng Li (101277255)
 File Name: ScenePanelChange.cs
 Last Modified: December 12th, 2021
 Description: Changes from start to instruction, gameplay, and end panels
 Version History: v1.03 - Made into a singleton
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePanelChange : MonoBehaviour
{
    private static ScenePanelChange _Instance;

    public static ScenePanelChange Instance()
    {
        if (_Instance == null)
        {
            _Instance = new ScenePanelChange();
        }

        return _Instance;
    }
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
