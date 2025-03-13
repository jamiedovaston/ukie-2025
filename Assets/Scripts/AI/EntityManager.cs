using System.Collections;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public GameObject m_EntityPrefab;

    public Transform m_SpawnPoint;

    private bool hasEnded = false;

    [ContextMenu("Initialise")]
    public void Initialise()
    {
        hasEnded = false;
        StartCoroutine(C_SpawnEntities());
    }

    public IEnumerator C_SpawnEntities()
    {
        while (!hasEnded)
        {
            yield return new WaitForSeconds(2); // replace

            Entity entity = Instantiate(m_EntityPrefab, m_SpawnPoint.position, Quaternion.identity).GetComponent<Entity>();

            entity.Initialise(5.0f, TeamData.GetRandom());
        }
    }
}
