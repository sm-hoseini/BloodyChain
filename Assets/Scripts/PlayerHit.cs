using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        if (collision.contacts.Length > 0)
        {
            if (collision.gameObject.tag == "enemy")
            {
                if (PlayerController.isPlayerAlive && PlayerController.canRecieveDamage)
                    ActionManager.OnPlayerHit();

                collision.transform.gameObject.SendMessage("hit", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
