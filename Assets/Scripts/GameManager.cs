using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using JD.Utility.General;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public void Start()
    {
        StartCoroutine(C_StartGame());
    }

    IEnumerator C_StartGame()
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
            yield return new WaitForSeconds(0.5f); 
        }

        Debug.Log("All objects are corrupted!");
    }

}