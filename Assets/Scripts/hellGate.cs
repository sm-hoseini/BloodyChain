using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hellGate : MonoBehaviour {

    private void Awake()
    {
        ActionManager.OnGetKey += OnGetKey;
    }
    private void OnDestroy()
    {
        ActionManager.OnGetKey -= OnGetKey;
    }
    void OnGetKey()
    {
        keys++;
    }
    int keys;
    private void OnCollisionEnter2D(Collision2D collision)
    {
if( collision.transform.tag=="Player"&& keys >= 3)
        {
            ActionManager.OnEndGame();
            print("gameEnd");
        }

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
