using UnityEngine;
using System.Collections;

public class EnemyHit : MonoBehaviour
{
    public Animator anim;
    bool isHited = false;
    public void hit()
    {
        if (!isHited)
        {
            anim.SetTrigger("dead");
            Statics.Score += Statics.monsterKillingScore;
            ActionManager.OnEnemyKilled();
            ActionManager.OnScoreUpdated();
            isHited = true;
        }
    }
}
