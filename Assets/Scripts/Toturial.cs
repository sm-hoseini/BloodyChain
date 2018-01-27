using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Toturial : MonoBehaviour {

    private bool loading = false;

	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.E))
        {
            if (!loading)
            {
                loading = true;
                SceneManager.LoadScene("GameplayScene");
            }
        }
	}
}
