using UnityEngine;

public class ExpandingCorruptionField : MonoBehaviour
{
    private SphereCollider m_Corruption;
    public float expansionRate = 1.05f; // Expansion rate per frame
    public float expansionInterval = 1.0f; // Time interval between expansions in seconds

    private void Awake()
    {
        m_Corruption = GetComponent<SphereCollider>();
        m_Corruption.radius = 1.0f;
        StartCoroutine(ExpandRadius());
    }

    private System.Collections.IEnumerator ExpandRadius()
    {
        while (true)
        {
            m_Corruption.radius *= expansionRate;
            yield return new WaitForSeconds(expansionInterval);
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
