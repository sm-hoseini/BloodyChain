using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieOffLight : MonoBehaviour {
    public float lifeTime, Intensity, Range;
    float initTime;
    Light light; 
	// Use this for initialization
	void Start () {
        initTime = Time.time;
        light = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - initTime >= 0.8f * lifeTime)
        {
            light.intensity = Mathf.Lerp(light.intensity, 0, (Time.time - 0.8f * lifeTime-initTime) / 0.2f*lifeTime);
        }
        if (Time.time > initTime + lifeTime)
        {
            Destroy(this.gameObject);
        }
	}
}
