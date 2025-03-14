using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using JD.Utility.General;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public static float GAME_TIME = 0.0f;
    public static float EXPANSION_RATE = 1.05f;
    public static float MAX_EXPANSION_RATE = 2.0f; // Maximum limit for expansion

    public void Start()
    {
        StartCoroutine(C_Gameplay());
        
        // defaults
        GAME_TIME = 0.0f;
        EXPANSION_RATE = 1.05f;
    }

    IEnumerator C_Gameplay()
    {
        yield return StartCoroutine(UIManager.instance.C_Countdown());

        EntityManager.instance.Initialise();

        List<CorruptionSwap> allCorruptedObjects = FindObjectsByType<CorruptionSwap>(FindObjectsSortMode.None).ToList();
        int totalObjects = allCorruptedObjects.Count;

        bool isAllCorrupted = false;

        while (!isAllCorrupted)
        {
            int corruptedCount = allCorruptedObjects.Count(obj => obj.isCorrupted);
            float percentage = (corruptedCount / (float)totalObjects) * 100f;
        
            UIManager.instance.ChangeCorruptionAmount(percentage);

            isAllCorrupted = corruptedCount == totalObjects;

            // Exponentially increase EXPANSION_RATE but keep it under the limit
            EXPANSION_RATE = Mathf.Min(1f + (0.005f * GAME_TIME), MAX_EXPANSION_RATE);  // Increased multiplier

            yield return new WaitForFixedUpdate();
            GAME_TIME += Time.fixedDeltaTime;
        }

        Debug.Log("All objects are corrupted!");
        
        UIManager.instance.GameOver();
    }
}

