using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class waveBlaster : MonoBehaviour {

	private float cooldown;
	private float aicool;
    private float BPM;
	private int direction;
    private int score;
	private float firerate;
	private float gameovertime;
	private bool shutflash;
	public static bool flash;
	public static bool gameover;
    public GameObject wave;
	public GameObject waveneg;
	public GameObject wave1;
	public GameObject waveneg1;
	public Transform wavespawn;
	public GameObject flashscreen;
	public static Vector3 pokspos;
    public float BPMStartValue;
    public float BPMIncreasePerWave;
    public float WaveSpeedBPMCoeff;


    Text scoreText;
    public AudioClip[] clips;
    private AudioSource[] audioSources;
    // Use this for initialization
    void Start () {
		

		shutflash = false;
		flash = false;
		cooldown = 0;
		aicool = 1;
        score = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        addToScore(0);
		firerate = 1;
        BPM = BPMStartValue;
		gameovertime = 3;

        audioSources = new AudioSource[clips.Length];

        for(int i = 0;i<clips.Length;i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].clip = clips[i];

        }
    }
	
	// Update is called once per frame
	void Update () {
        float timeDelta = Time.deltaTime;
		if (gameover)
			gameovertime -= timeDelta;
		if (gameovertime < 0) {
			if (score > MenuButtonScript.hiscore)
				MenuButtonScript.hiscore = score;
			SceneManager.LoadScene ("wavebreaker_menu");
		}
		if (cooldown > -0.02)
			cooldown -= timeDelta;
		if (aicool > -0.02)
			aicool -= timeDelta;
		if (shutflash) {
			shutflash = false;
			flashscreen.SetActive (false);
		}

		if (flash) {
			flashscreen.transform.position = pokspos;
			flashscreen.SetActive (true);
			shutflash = true;
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
    
}
