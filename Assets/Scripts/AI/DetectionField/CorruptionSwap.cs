using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CorruptionSwap : MonoBehaviour
{
    // Normal (black and white) Corrupted (Colourful)
    public GameObject Normal, Corrupted;

    private HashSet<Collider> m_Colliders = new HashSet<Collider>();

    public void OnCollide(Collider collider)
    {
        if (!m_Colliders.Add(collider))
        {
            return;
        }

        Normal.SetActive(false);
        Corrupted.SetActive(true);
    }

    public void OnCollideExit(Collider collider)
    {
        if (m_Colliders.Remove(collider))
        {
            if (!m_Colliders.Any())
            {
                Normal.SetActive(true);
                Corrupted.SetActive(false);
            }
        }
    }
}
