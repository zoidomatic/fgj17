using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveBlaster : MonoBehaviour {

	private int cooldown;
	public GameObject wave;
	public Transform wavespawn;
	// Use this for initialization
	void Start () {
		cooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown > 0)
			cooldown -= 1;
		
		if (Input.GetKey ("up") && cooldown == 0) {
			wavespawn.position = new Vector3 (5F,0,0);
			Instantiate (wave, wavespawn.position, wavespawn.rotation);
			cooldown = 40;
		}
		if (Input.GetKey ("down") && cooldown == 0) {
			wavespawn.position = new Vector3 (-5F,0,0);
			Instantiate (wave, wavespawn.position, wavespawn.rotation);
			cooldown = 40;
		}
		if (Input.GetKey ("left") && cooldown == 0) {
			wavespawn.position = new Vector3 (0,0,5F);
			Instantiate (wave, wavespawn.position, wavespawn.rotation);
			cooldown = 40;
		}
		if (Input.GetKey ("right") && cooldown == 0) {
			wavespawn.position = new Vector3 (0,0,-5F);
			Instantiate (wave, wavespawn.position, wavespawn.rotation);
			cooldown = 40;
		}
	}
}
