using System.Collections;
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

    public GameObject playerO;
    public GameObject enemyO;

    private float freeze_cd;
    private bool freeze_active;
    private float shield_cd;
    public static bool shield_active;
	private bool shield_safe;
    private float x2_cd;
    private bool x2_active;
    private int x2_increased;

    private List<ScoreText> scoreTextList;
	SpriteRenderer grow2;
	SpriteRenderer grow3;
	SpriteRenderer grow4;
	SpriteRenderer grow5;    public static int lives;
    // Use this for initialization
    void Start () {
        gameover = false;
        scoreTextList = new List<ScoreText>();
        lives = 3;
		updategrow = false;
		shutflash = 0;
		flash = false;

		cooldown = 0;
		aicool = 1;

        //Power-up stuff
        freeze_cd = 0;
        freeze_active = false;
        shield_cd = 0;
        shield_active = false;
		shield_safe = false;
        x2_cd = 0;
        x2_active = false;
        x2_increased = 0;

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
    void Update() {
        float timeDelta = Time.deltaTime;
        if (gameover)
        {
            gameovertime -= timeDelta;
            endtext.text = "Press [space] to restart (" + gameovertime.ToString("F1") + ")";

            if (Input.GetKey("space"))
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
            SceneManager.LoadScene("wavebreaker_menu");
        }
        if (cooldown > -0.02)
            cooldown -= timeDelta;
        if (aicool > -0.02)
            aicool -= timeDelta;

        if (shutflash > 0) {
            shutflash--;
            flashscreen.transform.localScale += new Vector3(1, 1, 1);
        }

        if (shutflash == 1) {
            shutflash = 0;
            flashscreen.SetActive(false);
        }

        if (flash) {
            flashscreen.transform.position = pokspos;
            flashscreen.transform.localScale = new Vector3(3, 3, 1);
            flashscreen.SetActive(true);
            shutflash = 6;
            flash = false;
        }

        //POWERUPS>>>>>>>>>>

        //////(freeze)
        if (scoremultiplier == 9 && !freeze_active)//9
        {
            GameObject.Find("freeze_on").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetKey("j") && GameObject.Find("freeze_on").GetComponent<SpriteRenderer>().enabled) {
            GameObject.Find("freeze_on").GetComponent<SpriteRenderer>().enabled = false;
            freeze_cd = 6;
            freeze_active = true;
            this.BPM = this.BPM / (float)1.35;
            this.WaveSpeedBPMCoeff = this.WaveSpeedBPMCoeff / (float)1.5;
            GameObject.Find("freeze_text").GetComponent<MeshRenderer>().enabled = true;

        }

        if (freeze_cd > -0.02)
        {
            Debug.Log("freeze ongoing");
            GameObject.Find("freeze_text").GetComponent<TextMesh>().text = "SLOWED (" + freeze_cd.ToString("F1") + ")";
            freeze_cd -= timeDelta;
        }
        else if (freeze_active)
        {
            Debug.Log("freeze disabled");
            freeze_active = false;
            this.BPM = this.BPM * (float)1.35;
            this.WaveSpeedBPMCoeff = this.WaveSpeedBPMCoeff * (float)1.5;
            this.BPM = this.BPM - 20;
            Debug.Log("BPM: " + this.BPM);
            GameObject.Find("freeze_text").GetComponent<MeshRenderer>().enabled = false;
        }
        //////////////
        ////(shield)
        if (scoremultiplier == 13 && !shield_active)//13
        {
            GameObject.Find("shield_on").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetKey("k") && GameObject.Find("shield_on").GetComponent<SpriteRenderer>().enabled)
        {
            GameObject.Find("shield_on").GetComponent<SpriteRenderer>().enabled = false;
            shield_cd = 5;
            shield_active = true;
            GameObject.Find("inv_text").GetComponent<MeshRenderer>().enabled = true;
			shield_safe = true;
        }

        if (shield_cd > -0.02)
        {
            Debug.Log("shield ongoing");
            GameObject.Find("inv_text").GetComponent<TextMesh>().text = "INVULNERABLE ("+shield_cd.ToString("F1") + ")";
            shield_cd -= timeDelta;
        }

        else if (shield_active)
        {
            GameObject.Find("inv_text").GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("shield disabled");
            shield_active = false;
        }
		if((shield_cd < 2)&&shield_safe){
			shield_safe = false;
			aicool = 2;
		}
        /////////////
        ////(X2)
        if (scoremultiplier == 5 && !x2_active)//5
        {
            GameObject.Find("x2_on").GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetKey("h") && GameObject.Find("x2_on").GetComponent<SpriteRenderer>().enabled)
        {
            GameObject.Find("x2_on").GetComponent<SpriteRenderer>().enabled = false;
            x2_cd = 7;
            x2_active = true;
            x2_increased = scoremultiplier;
            scoremultiplier = scoremultiplier * 2;
            GameObject.Find("MultiText").GetComponent<TextMesh>().text = "x " + scoremultiplier;
            GameObject.Find("MultiText").GetComponent<TextMesh>().color = Color.red;
        }

        if (x2_cd > -0.02)
        {
            Debug.Log("x2 ongoing");
            x2_cd -= timeDelta;
        }
        else if (x2_active)
        {
            scoremultiplier = scoremultiplier - x2_increased;
            if(scoremultiplier < 1) { scoremultiplier = 1; }
            GameObject.Find("MultiText").GetComponent<TextMesh>().text = "x " + scoremultiplier;
            GameObject.Find("MultiText").GetComponent<TextMesh>().color = new Color(0,195,255,255);
            Debug.Log("x2 disabled");
            x2_active = false;
        }
        ////////////
        ////(heart)
        if (scoremultiplier == 20)//20
        {
            GameObject.Find("hp_on").GetComponent<SpriteRenderer>().enabled = true;
        }
        if ((Input.GetKey("m") && GameObject.Find("hp_on").GetComponent<SpriteRenderer>().enabled) && (lives < 3))
        {
            GameObject.Find("hp_on").GetComponent<SpriteRenderer>().enabled = false;
            if (lives == 1) {
                lives = 2;
                GameObject.Find("life_2").GetComponent<SpriteRenderer>().enabled = true;
            }
            else if (lives == 2) {
                lives = 3;
                GameObject.Find("life_1").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        ///////////

        if (gameover == false) {
            int clipNum = Random.Range(0, 2);
			if ((Input.GetKey ("up")|| Input.GetKey("w")) && cooldown < 0) {
                Debug.Log("wave_A spawned");
                wavespawn.position = new Vector3 (5F, 0, 0);
				playerO = (GameObject)Instantiate (wave, wavespawn.position, wavespawn.rotation);
                playerO.tag = "Player_V_1";
				cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
			}
			if ((Input.GetKey ("down")|| Input.GetKey("s")) && cooldown < 0) {
                Debug.Log("waveneg_A spawned");
                wavespawn.position = new Vector3 (-5F, 0, 0);
				playerO = (GameObject)Instantiate (wave, wavespawn.position, wavespawn.rotation);//neg
                playerO.tag = "Player_V_2";
                cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
            }
			if ((Input.GetKey ("left")|| Input.GetKey("a")) && cooldown < 0) {
                Debug.Log("waveneg_B spawned");
                wavespawn.position = new Vector3 (0, 0, 5F);
				playerO = (GameObject)Instantiate (wave, wavespawn.position, wavespawn.rotation);//neg
                playerO.tag = "Player_H_1";
                cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
            }
			if ((Input.GetKey ("right")|| Input.GetKey("d")) && cooldown < 0) {
                Debug.Log("wave_B spawned");
                wavespawn.position = new Vector3 (0, 0, -5F);
				playerO = (GameObject)Instantiate (wave, wavespawn.position, wavespawn.rotation);
                playerO.tag = "Player_H_2";
                cooldown = 20 / BPM;
                audioSources[clipNum].Stop();
                audioSources[clipNum].Play();
            }
		}

		if (aicool < 0 && gameover == false) {
            int clipNum = Random.Range(0, 2);
            aicool = 60/BPM; // seconds per beat
			BPM += BPMIncreasePerWave;
			direction = Random.Range (1, 5);
			switch (direction) {
			case 1:
                    Debug.Log("wave1A spawned");
                    wavespawn.position = new Vector3 (5F,0,0);
                    wave1.tag = "Enemy_V_1";
                    enemyO = (GameObject)Instantiate(wave1, wavespawn.position, wavespawn.rotation);
                    //enemyO.tag = "Enemy_V_1";
                    audioSources[clipNum].Stop();
                    audioSources[clipNum].Play();
                    break;
			case 2:
                    Debug.Log("waveneg1A spawned");
                    wavespawn.position = new Vector3 (-5F,0,0);
                    wave1.tag = "Enemy_V_2";
                    enemyO = (GameObject)Instantiate(wave1, wavespawn.position, wavespawn.rotation);//neg
                    //enemyO.tag = "Enemy_V_2";
                    audioSources[clipNum].Stop();
                    audioSources[clipNum].Play();
                    break;
			case 3:
                    Debug.Log("waveneg1B spawned");
				wavespawn.position = new Vector3 (0,0,5F);
                    wave1.tag = "Enemy_H_1";
                    enemyO = (GameObject)Instantiate(wave1, wavespawn.position, wavespawn.rotation);//neg
                    //enemyO.tag = "Enemy_H_1";
                    audioSources[clipNum].Stop();
                    audioSources[clipNum].Play();
                    break;
			default:
                    Debug.Log("wave1B spawned");
                    wavespawn.position = new Vector3 (0, 0, -5F);
                    wave1.tag = "Enemy_H_2";
                    enemyO = (GameObject)Instantiate(wave1, wavespawn.position, wavespawn.rotation);
                    //enemyO.tag = "Enemy_H_2";
                    audioSources[clipNum].Stop();
                    audioSources[clipNum].Play();
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
        //Debug.Log("SC:" + scoreTextList);
         scoreTextList.Add(sc);

    }

    void updateScoreTexts(float timeDelta)
    {
        //Debug.Log("Count:" + scoreTextList.Count);
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
