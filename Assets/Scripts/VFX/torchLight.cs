using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchLight : MonoBehaviour {
    Light light;
   public  float minIntensity = 30, rangeIntensity = 20,minrange, rangeRange;
	// Use this for initialization
	void Start () {
        light=this.GetComponent<Light>();
	}

	// Update is called once per frame
	void Update () {
        light.intensity =minIntensity+rangeIntensity* Mathf.PerlinNoise(Time.time,0);
        light.spotAngle=minrange + Mathf.PerlinNoise(Time.time, 0) *rangeRange;
    }
}
