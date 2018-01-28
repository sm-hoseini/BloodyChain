using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu1 : MonoBehaviour {
    public GameObject gameObjectLoading;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void BtnStart()
    {
        Debug.Log("start");
        gameObjectLoading.SetActive(true);
        Application.LoadLevel("GamePlayPrototype");
    }
}
