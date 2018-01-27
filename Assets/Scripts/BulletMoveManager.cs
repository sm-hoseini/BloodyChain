using UnityEngine;
using System.Collections;

public class BulletMoveManager : MonoBehaviour
{
    //the speed, in units per second, we want to move towards the target
    public float speed = 6;
    public float timeToDestroy = 5f;

    private bool moving = false;

    private Vector3 directionOfTravel;
    private float fixedY;

    private float moveSpeed = 6;

    void OnEnable()
    {
        ActionManager.OnGamePaused += OnGamePaused;
        ActionManager.OnGameResumed += OnGameResumed;
    }

    void OnDisable()
    {
        ActionManager.OnGamePaused -= OnGamePaused;
        ActionManager.OnGameResumed -= OnGameResumed;
    }

    void Awake()
    {
        fixedY = transform.position.y;
    }

    void Start()
    {
        moveSpeed = speed;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.y = fixedY;
        directionOfTravel = targetPosition - transform.position;
        directionOfTravel.Normalize();

        moving = true;

        StartCoroutine("DestroyBullet");
    }

    void Update()
    {
        if (moving)
        {
            MoveTowardsTarget();
        }
    }

    //move towards a target at a set speed.
    private void MoveTowardsTarget()
    {
        //scale the movement on each axis by the directionOfTravel vector components
        transform.Translate(
            (directionOfTravel.x * moveSpeed * Time.deltaTime), 0,
            (directionOfTravel.z * moveSpeed * Time.deltaTime),
            Space.World);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hookable")
        {
            Ray ray = new Ray(transform.position, directionOfTravel);
            RaycastHit hit;
            if (other.Raycast(ray, out hit, 10))
            {
                float angle = AngleBetweenVector2(directionOfTravel, hit.normal);
                if (angle < 0)
                {
                    angle = 2 * (Mathf.Abs(angle) - 90);
                    angle *= -1;
                }
                else
                {
                    angle = 2 * (Mathf.Abs(angle) - 90);
                    angle *= 1;
                }

                angle += UnityEngine.Random.Range(-2, 2);

                directionOfTravel = Quaternion.Euler(0, angle, 0) * directionOfTravel;
                directionOfTravel.Normalize();
            }
        }
    }*/

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        if (collision.gameObject.tag == "hookable")
        {
            float angle = AngleBetweenVector2(directionOfTravel, contact.normal);
            if (angle < 0)
            {
                angle = 2 * (Mathf.Abs(angle) - 90);
                angle *= -1;
            }
            else
            {
                angle = 2 * (Mathf.Abs(angle) - 90);
                angle *= 1;
            }

            angle += UnityEngine.Random.Range(-2, 2);

            directionOfTravel = Quaternion.Euler(0, angle, 0) * directionOfTravel;
            directionOfTravel.Normalize();

            ActionManager.OnBulletHitWall();
        }
        else if (collision.gameObject.tag == "enemy")
        {
            collision.transform.gameObject.SendMessage("hit");
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (PlayerController.isPlayerAlive && PlayerController.canRecieveDamage)
                ActionManager.OnPlayerHit();
        }
    }

    private float AngleBetweenVector2(Vector3 a, Vector3 b)
    {
        var angle = Vector3.Angle(a, b); // calculate angle
                                         // assume the sign of the cross product's Y component:
        return angle * Mathf.Sign(Vector3.Cross(a, b).y);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }


    private void OnGamePaused()
    {
        moveSpeed = 0;
    }

    private void OnGameResumed()
    {
        moveSpeed = speed;
    }
}
