using JD.Utility.General;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform sitSocket { get => m_SitTransform; }
    public Vector3 position { get => transform.position; }
    public bool occupied { get; set; }

    public Transform m_SitTransform;

    public static List<Chair> allChairs = null;

    void Awake()
    {
        if (allChairs == null)
        {
            allChairs = FindObjectsByType<Chair>(FindObjectsSortMode.None).ToList();
        }
    }

    public static Chair GetRandomChair()
    {
        if (allChairs == null || allChairs.Count == 0)
            return null;

        int random = UnityEngine.Random.Range(0, allChairs.Count);
        Chair chair = allChairs[random];

        return chair;
    }
}
