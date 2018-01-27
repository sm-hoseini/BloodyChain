using UnityEngine;
using System.Collections;

public class UiAudioManager : MonoBehaviour
{

    private static UiAudioManager instance;
    public static UiAudioManager GetInstance()
    {
        return instance;
    }

    AudioSource source;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        source = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void PlayClickSound()
    {
        source.Play();
    }
}
