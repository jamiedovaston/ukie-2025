using UnityEngine;
using System.Collections;

public class ExpandingCorruptionField : MonoBehaviour
{
    private SphereCollider m_Corruption;
    public float expansionInterval = 1.0f; // Time interval between expansions in seconds

    private Collider[] allColliders;

    private void Awake()
    {
        m_Corruption = GetComponent<SphereCollider>();
        allColliders = GetComponents<Collider>();
        m_Corruption.radius = 1.0f;
        StartCoroutine(ExpandRadius());
    }

    private IEnumerator ExpandRadius()
    {
        while (m_Corruption != null)
        {
            m_Corruption.radius *= GameManager.EXPANSION_RATE;
            yield return new WaitForSeconds(expansionInterval);
        }
    }

    public void SetActive(bool active)
    { 
        m_Corruption.enabled = active;
        foreach (Collider c in allColliders)
        {
            c.enabled = active;
        }
    }
        

    private void OnTriggerEnter(Collider collision)
    {
        CorruptionSwap swap = collision.gameObject.GetComponent<CorruptionSwap>();
        if (swap)
            swap.OnCollide(m_Corruption);
    }

    private void OnTriggerExit(Collider collision)
    {
        CorruptionSwap swap = collision.gameObject.GetComponent<CorruptionSwap>();
        if (swap)
            swap.OnCollideExit(m_Corruption);
    }
}
