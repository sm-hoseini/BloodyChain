using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public Transform enemy;
    public float timeSpawn = 10;
    public float numberSpawn = 3;
    public float rate = 1f;

    public GameObject topPanel;
    public GameObject botPanel;
    public GameObject rightPanel;
    public GameObject leftPanel;

    private int createdEnemy = 0;

    private int waveCounter = 0;

    private bool waveFinished = false;
    private bool canSpawn = false;

    private int totalEnemy = 0;

    public float margin = 0.5f;

    void OnEnable()
    {
        ActionManager.OnEnemyKilled += OnEnemyKilled;
    }

    void OnDisable()
    {
        ActionManager.OnEnemyKilled -= OnEnemyKilled;
    }

    void Start () {
        StopCoroutine("NewWave");
        StartCoroutine("NewWave");
	}

    void Update()
    {
        if (waveFinished)
        {
            StopCoroutine("NewWave");
            StartCoroutine("NewWave");
        }
    }

    private void OnEnemyKilled()
    {
        totalEnemy--;
        if (totalEnemy == 0 && canSpawn)
        {
            StopCoroutine("NewWave");
            StartCoroutine("NewWave");
        }
    }

    IEnumerator NewWave()
    {
        waveFinished = false;
        canSpawn = false;

        waveCounter++;

        createdEnemy = 0;
        float randomTime = 0;
        float timeSpent = 0;
        while (createdEnemy < numberSpawn)
        {
            randomTime = Random.Range(0, timeSpawn / numberSpawn);
            yield return new WaitForSeconds(randomTime);
            timeSpent += randomTime;
            Spawn(RandomPosition());
        }

        numberSpawn += rate;
        canSpawn = true;

        yield return new WaitForSeconds(timeSpawn - timeSpent);

        waveFinished = true;
    }

    private void Spawn(Vector3 position)
    {
        Instantiate(enemy, position, enemy.transform.rotation);
        createdEnemy++;
        totalEnemy++;
    }

    private Vector3 RandomPosition()
    {
        float x =0, y, z =0;
        y = -2.77f;
        int side = Random.Range(0,4);
        switch (side)
        {
            case 0:
                z = topPanel.transform.position.z - margin;
                x = Random.Range(leftPanel.transform.position.x + margin , rightPanel.transform.position.x - margin);
                break;
            case 1:
                z = botPanel.transform.position.z + margin;
                x = Random.Range(leftPanel.transform.position.x + margin, rightPanel.transform.position.x - margin);
                break;
            case 2:
                x = leftPanel.transform.position.x + margin;
                z = Random.Range(topPanel.transform.position.z + margin, botPanel.transform.position.z - margin);
                break;
            case 3:
                x = rightPanel.transform.position.x - margin;
                z = Random.Range(topPanel.transform.position.z + margin, botPanel.transform.position.z - margin);
                break;
        }

        return new Vector3(x,y,z);
    }
}
