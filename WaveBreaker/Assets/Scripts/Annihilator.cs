using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annihilator : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.transform.position.x + transform.position.x == 0) {
            // Calculate score based on the collision
            float multiplier = 10;
            int score = 0;
            if(transform.localScale.x > 20 || transform.localScale.x < 0)
            {
                score = 0;
            }
            else if(transform.localScale.x < 10)
            {
                score = (int)(multiplier * (transform.localScale.x));
            }
            else
            {
                score = (int)(multiplier * (20-(transform.localScale.x)));
            }
            
            Debug.Log("scale x " + transform.localScale.x + "Score: " + score);
            GameObject.Find("wavespawn").GetComponent<waveBlaster>().addToScore(score);
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
