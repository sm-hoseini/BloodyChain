using UnityEngine;
using System.Collections;

public class Statics : MonoBehaviour
{
    public static int monsterKillingScore = 100;
    public static Transform Emmet;
    public static int EnvironmentNumber = 0;
    public static int Score = 0;

    public static void OnGameRestart()
    {
        Score = 0;
        EnvironmentNumber = 0;
    }

}
