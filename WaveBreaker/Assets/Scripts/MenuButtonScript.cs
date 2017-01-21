﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour {
	
	public static int hiscore;
	// Use this for initialization
	void Start () {
        // Initialize button texts
        GameObject.Find("StartButton").GetComponentInChildren<Text>().text = "Start";
        GameObject.Find("QuitButton").GetComponentInChildren<Text>().text = "Quit";
		GameObject.Find("HiscoreText").GetComponentInChildren<Text>().text = "Latest Highscore: " + hiscore;
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void handleStartButtonClick()
    {
        SceneManager.LoadScene("wavebreaker");
		waveBlaster.gameover = false;
    }

    public void handleQuitButtonClick()
    {
        Application.Quit();
    }
}
