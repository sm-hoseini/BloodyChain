using UnityEngine;
using System.Collections;

public class EnvironmentChanger : MonoBehaviour {

    public GameObject[] environmentObjects0;
    public GameObject[] environmentObjects1;

    public GameObject lightPlayer;
    public GameObject darkPlayer;

    void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeEnvironMent();
        }
	}

    private void ChangeEnvironMent()
    {
        if (Statics.EnvironmentNumber == 0)
        {
            Statics.EnvironmentNumber = 1;
            EnableDisableObjects();
        }
        else if (Statics.EnvironmentNumber == 1)
        {
            Statics.EnvironmentNumber = 0;
            EnableDisableObjects();
        }

        ActionManager.OnEnvironmentChange();
    }

    private void EnableDisableObjects()
    {
        if (Statics.EnvironmentNumber == 0)
        {
            foreach (GameObject obj in environmentObjects0)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in environmentObjects1)
            {
                obj.SetActive(false);
            }

            lightPlayer.SetActive(true);
            darkPlayer.SetActive(false);
        }
        else if (Statics.EnvironmentNumber == 1)
        {
            foreach (GameObject obj in environmentObjects1)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in environmentObjects0)
            {
                obj.SetActive(false);
            }

            lightPlayer.SetActive(false);
            darkPlayer.SetActive(true);
        }
    }
}
