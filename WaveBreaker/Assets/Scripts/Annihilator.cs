using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annihilator : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.transform.position.x + transform.position.x == 0) {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
