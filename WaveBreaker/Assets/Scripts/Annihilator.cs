using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Annihilator : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
		if (other.transform.position.x + transform.position.x == 0 && waveBlaster.gameover == false) {
			if (other.gameObject.tag == "Player" || gameObject.tag == "Player") {
				// Calculate score based on the collision
				int scoremulti = waveBlaster.scoremultiplier;
				int multiplier = 10;
				int score = 0;
				if (transform.localScale.x > 20 || transform.localScale.x < 0) {
					score = 0;
				} else if (transform.localScale.x < 10) {
					score = (int)(scoremulti * multiplier * (transform.localScale.x));
					//waveBlaster.scoremultiplier += 0.1F;
				} else {
					score = (int)(scoremulti * multiplier * (20 - (transform.localScale.x)));
					//waveBlaster.scoremultiplier += 0.1F;
				}
				waveBlaster.multigrow++;
				if (waveBlaster.multigrow == 6) {
					waveBlaster.multigrow = 1;
					waveBlaster.scoremultiplier++;
					scoremulti = waveBlaster.scoremultiplier;
					GameObject.Find ("MultiText").GetComponent<TextMesh> ().text = "x " + (scoremulti).ToString();
				}
				waveBlaster.updategrow = true;

				Debug.Log ("scale x " + transform.localScale.x + "Score: " + score);
				GameObject.Find ("wavespawn").GetComponent<waveBlaster> ().addToScore (score);

				if (gameObject.transform.position.x == 0) { // Räjähdyksen sijainti
					waveBlaster.pokspos = new Vector3 (0, 1, (gameObject.transform.position.z - (gameObject.transform.localScale.z / 2)));
				} else {
					waveBlaster.pokspos = new Vector3 ((gameObject.transform.position.x + (gameObject.transform.localScale.x / 2)), 1, 0);
				}

				waveBlaster.flash = true;
				GameObject.Find("wavespawn").GetComponent<waveBlaster>().playSound("annihilator");


                GameObject ngo = new GameObject();
                TextMesh text = ngo.AddComponent<TextMesh>();
                text.text = "+" + score;
                text.transform.position = new Vector3(2, 5.5F, 0);
                text.transform.rotation = Quaternion.Euler(90, 90, 0);
                text.transform.localScale = new Vector3(0.2F, 0.2F, 1);


                Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                text.font = ArialFont;
                text.fontSize = 70;
                text.color = new Color(0.0F, 171.0F / 255.0F, 1.0F);
                GameObject.Find("wavespawn").GetComponent<waveBlaster>().addScoreText(text);
                
                Destroy (other.gameObject);
				Destroy (gameObject);
			}
		}
	}
}