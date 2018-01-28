using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keys : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && gameObject.activeSelf)
        {
            
            ActionManager.OnGetKey();
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
