using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expander : MonoBehaviour {

	public float speed;
	private float truespeed;
	Text goText;
	// Use this for initialization
	void Start () {
		truespeed = speed * .1F;
		goText = GameObject.Find("GOText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3 (truespeed, 0, truespeed);

		if (transform.localScale.x > 21) {
			goText.text = "GAME OVER";
			Destroy (gameObject);
			waveBlaster.gameover = true;
		}
	}


}
