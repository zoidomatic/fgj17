using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour {
	public float dur = 1.0F;
	public Light lg;
	// Use this for initialization
	void Start () {
		lg = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		float phi = Time.time / dur * 2 * Mathf.PI;
		float amplitude = Mathf.Cos (phi) * 2.0F + 3.0F;
		lg.intensity = amplitude;
	}
}
