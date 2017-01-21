using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveBlaster : MonoBehaviour {

	private int cooldown;
	private int aicool;
	private int direction;
	public GameObject wave;
	public GameObject waveneg;
	public Transform wavespawn;
	// Use this for initialization
	void Start () {
		cooldown = 0;
		aicool = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if (cooldown > 0)
			cooldown -= 1;
		if (aicool > 0)
			aicool -= 1;
		
		if (Input.GetKey ("up") && cooldown == 0) {
			wavespawn.position = new Vector3 (5F,0,0);
			Instantiate (wave, wavespawn.position, wavespawn.rotation);
			cooldown = 20;
		}
		if (Input.GetKey ("down") && cooldown == 0) {
			wavespawn.position = new Vector3 (-5F,0,0);
			Instantiate (waveneg, wavespawn.position, wavespawn.rotation);
			cooldown = 20;
		}
		if (Input.GetKey ("left") && cooldown == 0) {
			wavespawn.position = new Vector3 (0,0,5F);
			Instantiate (waveneg, wavespawn.position, wavespawn.rotation);
			cooldown = 20;
		}
		if (Input.GetKey ("right") && cooldown == 0) {
			wavespawn.position = new Vector3 (0,0,-5F);
			Instantiate (wave, wavespawn.position, wavespawn.rotation);
			cooldown = 20;
		}

		if (aicool == 0) {
			aicool = 40;
			direction = Random.Range (1, 5);
			switch (direction) {
			case 1:
				wavespawn.position = new Vector3 (5F,0,0);
				Instantiate (wave, wavespawn.position, wavespawn.rotation);
				break;
			case 2:
				wavespawn.position = new Vector3 (-5F,0,0);
				Instantiate (waveneg, wavespawn.position, wavespawn.rotation);
				break;
			case 3:
				wavespawn.position = new Vector3 (0,0,5F);
				Instantiate (waveneg, wavespawn.position, wavespawn.rotation);
				break;
			default:
				wavespawn.position = new Vector3 (0, 0, -5F);
				Instantiate (wave, wavespawn.position, wavespawn.rotation);
				break;
			}
		}
	}
}
