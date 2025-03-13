using JD.Utility.General;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chair : MonoBehaviour, IChairable
{
    public Transform m_SitTransform;
    public Transform sitSocket { get => m_SitTransform; }

    public static IChairable GetRandomChair()
    {
        List<IChairable> chairable = InterfaceUtility.FindObjectsWithInterface<IChairable>();

        Debug.Assert(chairable.Any(), "No chairs in chair list!");

        int random = Random.Range(0, chairable.Count);
        IChairable chair = chairable[random];

        return chair;
    }
}
