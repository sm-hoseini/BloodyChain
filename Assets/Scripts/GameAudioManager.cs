using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class GameAudioManager : MonoBehaviour
{
    public int numberOfPlayerAudioSources;
    public AudioMixer mixer;

    public AudioClip runClip;
    public AudioClip jumpClip;
    public AudioClip deadClip;
    public AudioClip hurtClip;
    public AudioClip hookShootClip;
    public AudioClip hookedClip;
    public AudioClip shootClip;
    public AudioClip portalClip;

    public AudioClip enemyHittedClip;

    public AudioClip bulletHit;

    AudioSource[] playerSources;
    AudioSource runSource;

    public AudioMixerSnapshot baseSnapshot;
    public AudioMixerSnapshot muteSfxSpanshot;
    public AudioMixerSnapshot darkBgSnapshot;

    bool isDarkScene = false;
    void Start()
    {
        baseSnapshot.TransitionTo(1);
        playerSources = new AudioSource[numberOfPlayerAudioSources];
        for (int i = 0; i < numberOfPlayerAudioSources; i++)
        {
            playerSources[i] = AddAudio(jumpClip, false, false, 1, "PlayerAudios");
        }

        runSource = AddAudio(runClip, false, false, 1, "PlayerAudios");
    }

    void OnEnable()
    {
        ActionManager.OnPlayerStartRun += OnPlayerStartRun;
        ActionManager.OnPlayerEndRun += OnPlayerEndRun;
        ActionManager.OnPlayerJump += OnPlayerJump;
        ActionManager.OnPlayerHookStart += OnPlayerHookStart;
        ActionManager.OnPlayerHookEnd += OnPlayerHookEnd;
        ActionManager.OnPlayerHooked += OnPlayerHooked;
        ActionManager.OnPlayerShoot += OnPlayerShoot;
        ActionManager.OnPlayerHit += OnPlayerHit;
        ActionManager.OnPlayerDie += OnPlayerDie;
        ActionManager.OnEnvironmentChange += OnPlayerSwitchWorld;
        ActionManager.OnEnemyKilled += OnEnemyKilled;
        ActionManager.OnBulletHitWall += OnBulletHitWall;
    }

    void OnDisable()
    {
        ActionManager.OnPlayerStartRun -= OnPlayerStartRun;
        ActionManager.OnPlayerEndRun -= OnPlayerEndRun;
        ActionManager.OnPlayerJump -= OnPlayerJump;
        ActionManager.OnPlayerHookStart -= OnPlayerHookStart;
        ActionManager.OnPlayerHookEnd -= OnPlayerHookEnd;
        ActionManager.OnPlayerHooked -= OnPlayerHooked;
        ActionManager.OnPlayerShoot -= OnPlayerShoot;
        ActionManager.OnPlayerHit -= OnPlayerHit;
        ActionManager.OnPlayerDie -= OnPlayerDie;
        ActionManager.OnEnvironmentChange -= OnPlayerSwitchWorld;
        ActionManager.OnEnemyKilled -= OnEnemyKilled;
        ActionManager.OnBulletHitWall -= OnBulletHitWall;
    }

    void OnDestroy()
    {
        baseSnapshot.TransitionTo(1);
    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol, string outputAudioMixerGroup)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;

        newAudio.spatialBlend = 0;
        newAudio.rolloffMode = AudioRolloffMode.Linear;
        newAudio.outputAudioMixerGroup = mixer.FindMatchingGroups(outputAudioMixerGroup)[0];
        return newAudio;
    }

    AudioSource SelectAFreePlayerAudioSource()
    {
        foreach (AudioSource aus in playerSources)
        {
            if (!aus.isPlaying)
            {
                return aus;
            }
        }
        return playerSources[0];
    }

    void OnPlayerStartRun()
    {
        runSource.clip = runClip;
        StartCoroutine("PlayRunClip");
    }

    IEnumerator PlayRunClip()
    {
        while (true)
        {
            runSource.Play();
            yield return new WaitForSeconds(0.3f);
            runSource.Play();
            yield return null;
        }
    }


    void OnPlayerEndRun()
    {
        StopCoroutine("PlayRunClip");
    }

    void OnPlayerJump()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = jumpClip;
        player.Play();
    }
    void OnPlayerHookStart()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = hookShootClip;
        player.Play();
    }
    void OnPlayerHooked()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = hookedClip;
        player.Play();
    }
    void OnPlayerHookEnd()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = hookShootClip;
        player.Play();
    }
    void OnPlayerShoot()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = shootClip;
        player.Play();
    }
    void OnPlayerHit()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = hurtClip;
        player.Play();
    }
    void OnPlayerDie()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = deadClip;
        player.Play();

        muteSfxSpanshot.TransitionTo(2);
    }
    void OnPlayerSwitchWorld()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = portalClip;
        player.Play();

        if (!isDarkScene)
        {
            darkBgSnapshot.TransitionTo(1);
            isDarkScene = true;
        }
        else
        {
            baseSnapshot.TransitionTo(1);
            isDarkScene = false;
        }
    }
    void OnEnemyKilled()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = enemyHittedClip;
        player.Play();
    }

    void OnBulletHitWall()
    {
        AudioSource player = SelectAFreePlayerAudioSource();
        player.clip = bulletHit;
        player.Play();
    }
}
