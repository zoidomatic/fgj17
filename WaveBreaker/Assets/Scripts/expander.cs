using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expander : MonoBehaviour {

	public float speed;
	private float truespeed;
    private float BPM;
	Text goText;
	// Use this for initialization
	void Start () {
        BPM = GameObject.Find("wavespawn").GetComponent<waveBlaster>().getBPM();
        float BPMCoeff = GameObject.Find("wavespawn").GetComponent<waveBlaster>().WaveSpeedBPMCoeff;
        // scaling factor 20 reaches end of screen, fit it to BPM
        truespeed = (BPM*BPMCoeff/60)*20;

		goText = GameObject.Find("GOText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float timeDelta = Time.deltaTime;
		transform.localScale += new Vector3 (truespeed*timeDelta, 0, truespeed*timeDelta);

		if (transform.localScale.x > 21) {
			goText.text = "GAME OVER";
			Destroy (gameObject);
			waveBlaster.gameover = true;
			// Heittää päävalikkoon?
		}
	}


}
