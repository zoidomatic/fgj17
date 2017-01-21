using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expander : MonoBehaviour {

	public float speed;
	private float truespeed;
	Text scoreText;
	// Use this for initialization
	void Start () {
		truespeed = speed * .1F;
		scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3 (truespeed, 0, truespeed);

		if (transform.localScale.x > 21) {
			scoreText.text = "GAME OVER";
			Destroy (gameObject);
			waveBlaster.gameover = true;
			// Heittää päävalikkoon?
		}
	}


}
