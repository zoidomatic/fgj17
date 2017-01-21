using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expander : MonoBehaviour {

	public float speed;
	private float truespeed;
	Text goText; //KORVATTAVA
    SpriteRenderer life1;
    SpriteRenderer life2;
    SpriteRenderer life3;
    // Use this for initialization
    void Start () {
		truespeed = speed * .1F;
		goText = GameObject.Find("GOText").GetComponent<Text>(); //KORVATTAVA
        life1 = GameObject.Find("life_1").GetComponent<SpriteRenderer>();
        life2 = GameObject.Find("life_2").GetComponent<SpriteRenderer>();
        life3 = GameObject.Find("life_3").GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3 (truespeed, 0, truespeed);

		if (transform.localScale.x > 21) {
			Destroy (gameObject);
            waveBlaster.lives--;
            if (waveBlaster.lives == 2) {
                //life1.sprite 
            }
            else if (waveBlaster.lives == 1) { }
            else if (waveBlaster.lives == 0) { }
            if (waveBlaster.lives == 0) {
                goText.text = "GAME OVER"; //KORVATTAVA
                waveBlaster.gameover = true;
            }
			// Heittää päävalikkoon?
		}
	}


}
