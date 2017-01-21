﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreText
{
    public float curTime_;
    public TextMesh text_;

    public void setText(TextMesh text)
    {
        text_ = text;
    }
}


public class waveBlaster : MonoBehaviour {

	private float cooldown;
	private float aicool;
    private float BPM;
	private int direction;
    private int score;

	private float gameovertime;
	public static int scoremultiplier;
	public static int multigrow;
	public static bool updategrow;
	private int shutflash;
	public static bool flash;
	public static bool gameover;
    public GameObject wave;
	public GameObject waveneg;
	public GameObject wave1;
	public GameObject waveneg1;
	public Transform wavespawn;
	public GameObject flashscreen;
	public static Vector3 pokspos;
    private TextMesh scoreText;
    public static TextMesh endtext;
    public float BPMStartValue = 65.0F;
    public float BPMIncreasePerWave = 1.0F;
    public float WaveSpeedBPMCoeff = 0.5F;
	public AudioClip[] clips;
    private AudioSource[] audioSources;

 	private List<ScoreText> scoreTextList;
	SpriteRenderer grow2;
	SpriteRenderer grow3;
	SpriteRenderer grow4;
	SpriteRenderer grow5;    public static int lives;
    // Use this for initialization
    void Start () {		
        scoreTextList = new List<ScoreText>();
        lives = 3;
		updategrow = false;
		shutflash = 0;
		flash = false;
		cooldown = 0;
		aicool = 1;
        score = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMesh>();
        endtext = GameObject.Find("endtext").GetComponent<TextMesh>();
        addToScore(0);
        BPM = BPMStartValue;
		gameovertime = 8;
		scoremultiplier = 1;
		multigrow = 1;

        audioSources = new AudioSource[clips.Length];

        for(int i = 0;i<clips.Length;i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].clip = clips[i];

        }

		grow2 = GameObject.Find("grow_2").GetComponent<SpriteRenderer>();
		grow3 = GameObject.Find("grow_3").GetComponent<SpriteRenderer>();
		grow4 = GameObject.Find("grow_4").GetComponent<SpriteRenderer>();
		grow5 = GameObject.Find("grow_5").GetComponent<SpriteRenderer>();

 		multiGrowing (1);
    }
	
	// Update is called once per frame
	void Update () {
        float timeDelta = Time.deltaTime;
        if (gameover)
        {
            gameovertime -= timeDelta;
            endtext.text = "press 'R' to restart ("+gameovertime.ToString("F1")+")";

            if (Input.GetKey("r"))
            {
                if (score > MenuButtonScript.hiscore)
                    MenuButtonScript.hiscore = score;
                gameover = false;
                SceneManager.LoadScene("wavebreaker");
            }

        }
		if (gameovertime < 0) {
			if (score > MenuButtonScript.hiscore)
				MenuButtonScript.hiscore = score;
			SceneManager.LoadScene ("wavebreaker_menu");
		}
		if (cooldown > -0.02)
			cooldown -= timeDelta;
		if (aicool > -0.02)
			aicool -= timeDelta;

		if (shutflash > 0) {
			shutflash--;
			flashscreen.transform.localScale += new Vector3 (1, 1, 1);
		}
		
		if (shutflash == 1) {
			shutflash = 0;
			flashscreen.SetActive (false);
		}

		if (flash) {
			flashscreen.transform.position = pokspos;
			flashscreen.transform.localScale = new Vector3 (3,3,1);
			flashscreen.SetActive (true);
			shutflash = 6;
			flash = false;
		}
		if (gameover == false) {
            int clipNum = Random.Range(0, 2);
			if (Input.GetKey ("up") && cooldown < 0) {
				wavespawn.position = new Vector3 (5F, 0, 0);
				Instantiate (wave, wavespawn.position, wavespawn.rotation);
				cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
			}
			if (Input.GetKey ("down") && cooldown < 0) {
				wavespawn.position = new Vector3 (-5F, 0, 0);
				Instantiate (waveneg, wavespawn.position, wavespawn.rotation);
				cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
            }
			if (Input.GetKey ("left") && cooldown < 0) {
				wavespawn.position = new Vector3 (0, 0, 5F);
				Instantiate (waveneg, wavespawn.position, wavespawn.rotation);
				cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
            }
			if (Input.GetKey ("right") && cooldown < 0) {
				wavespawn.position = new Vector3 (0, 0, -5F);
				Instantiate (wave, wavespawn.position, wavespawn.rotation);
				cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
            }
		}

		if (aicool < 0 && gameover == false) {
			aicool = 60/BPM; // seconds per beat
			BPM += BPMIncreasePerWave;
			direction = Random.Range (1, 5);
			switch (direction) {
			case 1:
				wavespawn.position = new Vector3 (5F,0,0);
				Instantiate (wave1, wavespawn.position, wavespawn.rotation);
				break;
			case 2:
				wavespawn.position = new Vector3 (-5F,0,0);
				Instantiate (waveneg1, wavespawn.position, wavespawn.rotation);
				break;
			case 3:
				wavespawn.position = new Vector3 (0,0,5F);
				Instantiate (waveneg1, wavespawn.position, wavespawn.rotation);
				break;
			default:
				wavespawn.position = new Vector3 (0, 0, -5F);
				Instantiate (wave1, wavespawn.position, wavespawn.rotation);
				break;
			}
		}

		if (updategrow) {
			multiGrowing (multigrow);
			updategrow = false;
		}
        updateScoreTexts(timeDelta);
    }

    public void addToScore(int addedScore) {
        score += addedScore;
		scoreText.text = score.ToString();
    }

    public void playSound(string component)
    {
        if(component == "annihilator")
        {
            int clipNum = Random.Range(2, clips.Length);
            audioSources[clipNum].Stop();
            audioSources[clipNum].Play();
        }
    }

    public float getBPM()
    {
        return BPM;
    }

    public void addScoreText(TextMesh scoreTextE)
    {
        ScoreText sc = new ScoreText();
        sc.curTime_ = 0;
        sc.setText(scoreTextE);
        Debug.Log("SC:" + scoreTextList);
         scoreTextList.Add(sc);

    }

    void updateScoreTexts(float timeDelta)
    {
        Debug.Log("Count:" + scoreTextList.Count);
        for(int i=scoreTextList.Count-1; i>=0;--i)
        {
           Transform trans = scoreTextList[i].text_.transform;
           scoreTextList[i].curTime_ += timeDelta;
            scoreTextList[i].text_.transform.position = (new Vector3(trans.position.x, trans.position.y, trans.position.z-scoreTextList[i].curTime_*0.8F));
            
            if (scoreTextList[i].text_.transform.position.z < -10)
            {
                Destroy(scoreTextList[i].text_.transform.gameObject);
                scoreTextList.RemoveAt(i);

            }
        }
    }
    
 	public void multiGrowing(int i) {
		switch (i) {
		case 1:
			grow2.enabled = false;
			grow3.enabled = false;
			grow4.enabled = false;
			grow5.enabled = false;
			break;
		case 2:
			grow2.enabled = true;
			break;
		case 3:
			grow3.enabled = true;
			break;
		case 4:
			grow4.enabled = true;
			break;
		case 5:
			grow5.enabled = true;
			break;
		}
	}
}
