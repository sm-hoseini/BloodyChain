using UnityEngine;
using System.Collections;

public class EnemyScripts : MonoBehaviour {

	// Use this for initialization
    private UnityEngine.AI.NavMeshAgent enemyAgent;
    public Transform target;
    public Transform enemySprite;

	void Start () {
        enemyAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = Statics.Emmet;
        StartCoroutine("MoveEnemyToDitanation");
	}

  
    IEnumerator MoveEnemyToDitanation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            enemyAgent.SetDestination(target.position);

        }
    }

	// Update is called once per frame
	void Update () {
        
	}

    void LateUpdate()
    {
        enemySprite.transform.position = transform.position;
        if (enemyAgent.velocity.x < 0)
        {
            enemySprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            enemySprite.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
