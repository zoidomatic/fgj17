using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annihilator : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.transform.position.x + transform.position.x == 0) {
			if ((other.gameObject.tag == "Enemy" && gameObject.tag == "Player") || (other.gameObject.tag == "Player" && gameObject.tag == "Enemy")) {
				Destroy (other.gameObject);
				Destroy (gameObject);
			}
		}
	}
}
