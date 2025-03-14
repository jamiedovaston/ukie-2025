using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CorruptionSwap : MonoBehaviour
{
    // Normal (black and white) Corrupted (Colourful)
    public GameObject Normal, Corrupted;

    private HashSet<Collider> m_Colliders = new HashSet<Collider>();

    public bool isCorrupted { get; private set; }

    private void Update()
    {
        // Find all null colliders and call OnCollideExit before removing them
        foreach (Collider collider in m_Colliders.Where(c => c == null).ToList())
        {
            OnCollideExit(collider); // Handle exit before removal
        }
    }

    public void OnCollide(Collider collider)
    {
        if (!m_Colliders.Add(collider))
        {
            return;
        }

        isCorrupted = true;
        
        Normal.SetActive(false);
        Corrupted.SetActive(true);
    }

    public void OnCollideExit(Collider collider)
    {
        if (m_Colliders.Remove(collider))
        {
            if (!m_Colliders.Any())
            {
                isCorrupted = false;
                
                Normal.SetActive(true);
                Corrupted.SetActive(false);
            }
        }
    }
}
