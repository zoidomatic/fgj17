using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Annihilator : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
		if (other.transform.position.x + transform.position.x == 0 && waveBlaster.gameover == false) {

            if (this.gameObject.tag == "Player_V_1" && other.gameObject.tag == "Player_V_2") { destroy(0,other); }
            else if (this.gameObject.tag == "Player_H_1" && other.gameObject.tag == "Player_H_2") { destroy(0, other); }

            else if (this.gameObject.tag == "Player_V_1" && other.gameObject.tag == "Enemy_V_2") { destroy(1, other); }
            else if (this.gameObject.tag == "Player_V_2" && other.gameObject.tag == "Enemy_V_1") { destroy(1, other); }
            else if (this.gameObject.tag == "Player_H_1" && other.gameObject.tag == "Enemy_H_2") { destroy(1, other); }
            else if (this.gameObject.tag == "Player_H_2" && other.gameObject.tag == "Enemy_H_1") { destroy(1, other); }

        }
	}
    
    void destroy(int input, Collider other) {
            //Debug.Log("ENEMY AND PLAYER " + other.gameObject.tag + "___" + this.gameObject.tag);

            // Calculate score based on the collision
            
            int multiplier = 10;
            int score = 0;
            int scoremulti = waveBlaster.scoremultiplier;
        if (input == 1) {
            if (transform.localScale.x > 20 || transform.localScale.x < 0)
            {
                score = 0;
            }
            else if (transform.localScale.x < 10)
            {
                score = (int)(scoremulti * multiplier * (transform.localScale.x));
                //waveBlaster.scoremultiplier += 0.1F;
            }
            else
            {
                score = (int)(scoremulti * multiplier * (20 - (transform.localScale.x)));
                //waveBlaster.scoremultiplier += 0.1F;
            }
            waveBlaster.multigrow++;
        }

            if (waveBlaster.multigrow == 6)
            {
                waveBlaster.multigrow = 1;
                waveBlaster.scoremultiplier++;
                scoremulti = waveBlaster.scoremultiplier;
                GameObject.Find("MultiText").GetComponent<TextMesh>().text = "x " + (scoremulti).ToString();


            }
            waveBlaster.updategrow = true;

            //Debug.Log("scale x " + transform.localScale.x + "Score: " + score);
            GameObject.Find("wavespawn").GetComponent<waveBlaster>().addToScore(score);

		if (gameObject.transform.position.x == 0 && gameObject.transform.position.z == -5) { // Räjähdyksen sijainti
			waveBlaster.pokspos = new Vector3 (0, 1, (gameObject.transform.position.z + (gameObject.transform.localScale.z / 2)));
		} else if (gameObject.transform.position.x == 0 && gameObject.transform.position.z == 5) {
			waveBlaster.pokspos = new Vector3 (0, 1, (gameObject.transform.position.z - (gameObject.transform.localScale.z / 2)));
		} else if (gameObject.transform.position.x == -5 && gameObject.transform.position.z == 0) {
			waveBlaster.pokspos = new Vector3 ((gameObject.transform.position.x + (gameObject.transform.localScale.z / 2)), 1, 0);
		} else {
			waveBlaster.pokspos = new Vector3((gameObject.transform.position.x - (gameObject.transform.localScale.z / 2)), 1, 0);
		}
            waveBlaster.flash = true;
            GameObject.Find("wavespawn").GetComponent<waveBlaster>().playSound("annihilator");


            GameObject ngo = new GameObject();
            TextMesh text = ngo.AddComponent<TextMesh>();
        if (input == 1) {
            text.text = "+" + score;
            text.color = new Color(0.0F, 171.0F / 255.0F, 1.0F);
        }
        else if (input == 0) {
            text.text = "0";
            text.color = Color.red;
        }
            text.transform.position = new Vector3(2, 5.5F, 0);
            text.transform.rotation = Quaternion.Euler(90, 90, 0);
            text.transform.localScale = new Vector3(0.2F, 0.2F, 1);


            Font Techno = (Font)Resources.Load("Techno.ttf");
            text.font = Techno;
            text.fontSize = 70;
            
            GameObject.Find("wavespawn").GetComponent<waveBlaster>().addScoreText(text);

            Destroy(other.gameObject);
            Destroy(gameObject);
        
    }
}