﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Initialize button texts
        GameObject.Find("StartButton").GetComponentInChildren<Text>().text = "Start";
        GameObject.Find("HighScoreButton").GetComponentInChildren<Text>().text = "Highscores";
        GameObject.Find("CreditsButton").GetComponentInChildren<Text>().text = "Credits";
        GameObject.Find("QuitButton").GetComponentInChildren<Text>().text = "Quit";


    }

    // Update is called once per frame
    void Update () {
		
	}
    public void handleStartButtonClick()
    {
        SceneManager.LoadScene("wavebreaker");
    }

    public void handleQuitButtonClick()
    {
        Application.Quit();
    }
}