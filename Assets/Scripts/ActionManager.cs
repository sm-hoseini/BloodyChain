using UnityEngine;
using System.Collections;
using System;

public class ActionManager : MonoBehaviour
{
    public static Action OnPlayerStartRun = delegate { };
    public static Action OnPlayerEndRun = delegate { };
    public static Action OnPlayerJump = delegate { };

    public static Action OnPlayerHitGround = delegate { };
    public static Action<bool> OnPlayerIsInGround = delegate { };
    public static Action<int> PlayerAirCondition = delegate { };

    public static Action OnPlayerHookStart = delegate { };
    public static Action OnPlayerHooked = delegate { };
    public static Action OnPlayerHookEnd = delegate { };

    public static Action OnPlayerShoot = delegate { };
    public static Action OnPlayerHit = delegate { };
    public static Action OnPlayerDie = delegate { };
    public static Action OnPlayerSwitchWorld = delegate { };

    public static Action OnEnemyKilled = delegate { };
    public static Action OnEnvironmentChange = delegate { };

    public static Action OnScoreUpdated = delegate { };

    public static Action OnGameRestart = delegate { };

    public static Action OnGamePaused = delegate { };
    public static Action OnGameResumed = delegate { };

    public static Action OnBulletHitWall = delegate { };
}
