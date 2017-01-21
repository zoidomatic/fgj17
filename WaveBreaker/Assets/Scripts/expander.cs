﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expander : MonoBehaviour {

	public float speed;
	private float truespeed;

 	private float BPM;

	Text goText; //KORVATTAVA
    SpriteRenderer life1;
    SpriteRenderer life2;
    SpriteRenderer life3;
	// Use this for initialization
	void Start () {

        BPM = GameObject.Find("wavespawn").GetComponent<waveBlaster>().getBPM();
        float BPMCoeff = GameObject.Find("wavespawn").GetComponent<waveBlaster>().WaveSpeedBPMCoeff;
        // scaling factor 20 reaches end of screen, fit it to BPM
        truespeed = (BPM*BPMCoeff/60)*20;

		goText = GameObject.Find("GOText").GetComponent<Text>(); //KORVATTAVA
        life1 = GameObject.Find("life_1").GetComponent<SpriteRenderer>();
        life2 = GameObject.Find("life_2").GetComponent<SpriteRenderer>();
        life3 = GameObject.Find("life_3").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        float timeDelta = Time.deltaTime;
		transform.localScale += new Vector3 (truespeed*timeDelta, 0, truespeed*timeDelta);

        if (transform.localScale.x > 21)
        {
            Destroy(gameObject);
            waveBlaster.lives--;
            if (waveBlaster.lives == 2)
            {
                life1.enabled = false;
            }
            else if (waveBlaster.lives == 1) { life2.enabled = false; }
            else if (waveBlaster.lives == 0) { life3.enabled = false; }
            if (waveBlaster.lives == 0)
            {
                goText.text = "GAME OVER"; //KORVATTAVA
                waveBlaster.gameover = true;
            }
            // Heittää päävalikkoon?

        }
    }
 
}
