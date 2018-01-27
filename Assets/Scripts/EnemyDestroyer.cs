using UnityEngine;
using System.Collections;

public class EnemyDestroyer : MonoBehaviour {

    void destroy()
    {
        Destroy(transform.root.gameObject);
    }
}
