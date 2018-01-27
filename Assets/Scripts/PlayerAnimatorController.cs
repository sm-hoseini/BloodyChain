using UnityEngine;
using System.Collections;

public class PlayerAnimatorController : MonoBehaviour
{

    Animator anim;


    void OnEnable()
    {
        ActionManager.OnPlayerStartRun += OnPlayerStartRun;
        ActionManager.OnPlayerEndRun += OnPlayerEndRun;
        ActionManager.OnPlayerJump += OnPlayerJump;

        ActionManager.OnPlayerHitGround += OnPlayerHitGround;
        ActionManager.OnPlayerIsInGround += OnPlayerIsInGround;

        ActionManager.OnPlayerHookStart += OnPlayerHookStart;
        ActionManager.OnPlayerHookEnd += OnPlayerHookEnd;
        ActionManager.OnPlayerHooked += OnPlayerHooked;
        ActionManager.OnPlayerShoot += OnPlayerShoot;
        ActionManager.OnPlayerHit += OnPlayerHit;
        ActionManager.OnPlayerDie += OnPlayerDie;
        ActionManager.OnPlayerSwitchWorld += OnPlayerSwitchWorld;

        ActionManager.PlayerAirCondition += SetPlayerAirCondition;
    }

    void OnDisable()
    {
        ActionManager.OnPlayerStartRun -= OnPlayerStartRun;
        ActionManager.OnPlayerEndRun -= OnPlayerEndRun;
        ActionManager.OnPlayerJump -= OnPlayerJump;

        ActionManager.OnPlayerHitGround -= OnPlayerHitGround;
        ActionManager.OnPlayerIsInGround -= OnPlayerIsInGround;

        ActionManager.OnPlayerHookStart -= OnPlayerHookStart;
        ActionManager.OnPlayerHookEnd -= OnPlayerHookEnd;
        ActionManager.OnPlayerHooked -= OnPlayerHooked;
        ActionManager.OnPlayerShoot -= OnPlayerShoot;
        ActionManager.OnPlayerHit -= OnPlayerHit;
        ActionManager.OnPlayerDie -= OnPlayerDie;

        ActionManager.PlayerAirCondition -= SetPlayerAirCondition;
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnPlayerStartRun()
    {
        anim.SetBool("isRunning", true);
        //print("isRunningStart");
    }

    void OnPlayerEndRun()
    {
        anim.SetBool("isRunning", false);
        //print("isRunningEnd");
    }

    void OnPlayerJump()
    {
        anim.SetTrigger("jump");
        //print("OnPlayerJump");
    }

    void OnPlayerHitGround()
    {
        anim.SetTrigger("hitGround");
    }
    void OnPlayerIsInGround(bool isInGround)
    {
        if (isInGround)
        {
            anim.SetBool("grounded", true);
            //print("OnPlayerGrounded");
        }
        else
        {
            anim.SetBool("grounded", false);
            //print("OnPlayerFlying");
        }
    }

    void OnPlayerHookStart()
    {
        anim.SetTrigger("hookStart");
        //print("OnPlayerHookStart");
    }
    void OnPlayerHooked()
    {
        anim.SetBool("isHooking", true);
        //print("isHooking True");
    }
    void OnPlayerHookEnd()
    {
        anim.SetTrigger("hookEnd");
        anim.SetBool("isHooking", false);
        //print("isHooking False");
        //print("OnPlayerHookEnd");
    }

    void OnPlayerShoot()
    {
        anim.SetTrigger("shoot");
        //print("OnPlayerShoot");
    }
    void OnPlayerHit()
    {
        anim.SetTrigger("hit");
        //print("OnPlayerHit");

    }
    void OnPlayerDie()
    {
        anim.SetTrigger("die");
        anim.SetBool("isPlayerDead", true);
        //print("OnPlayerDie");

    }
    void OnPlayerSwitchWorld()
    {
        anim.SetTrigger("switch");
        //print("OnPlayerSwitchWorld");

    }

    void SetPlayerAirCondition(int airCondition)
    {
        anim.SetInteger("airCondition", airCondition);

        switch (airCondition)
        {
            case 1:
                anim.SetTrigger("goUp");
                break;

            case -1:
                anim.SetTrigger("goDown");
                break;
        }
    }
}
