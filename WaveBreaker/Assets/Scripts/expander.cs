using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expander : MonoBehaviour {

	public float speed;
	private float truespeed;
	// Use this for initialization
	void Start () {
		truespeed = speed * .1F;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3 (truespeed, 0, truespeed);
	}


}
