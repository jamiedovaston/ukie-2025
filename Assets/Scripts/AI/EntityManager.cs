using JD.Utility.General;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TeamOppositionChartSO;

public class EntityManager : MonoSingleton<EntityManager>
{
    public GameObject m_EntityPrefab;

    public List<Transform> m_SpawnPoints;

    private bool hasEnded = false;

    public void Initialise()
    {
        hasEnded = false;
        StartCoroutine(C_SpawnEntities());
    }

    public IEnumerator C_SpawnEntities()
    {
        while (!hasEnded)
        {
            TeamOpposition_Data data = TeamOppositionChartSO.instance.GetRandomTeamData();

            foreach(TeamData t in data.Teams)
            {
                Entity entity = Instantiate(m_EntityPrefab, GetRandomDoor(), Quaternion.identity).GetComponent<Entity>();
                entity.Initialise(t);
                yield return new WaitForSeconds(Random.Range(.6f, 1.5f));
            }
            
            yield return new WaitForSeconds(2); // BUFFER THAT SHOULD BE REPLACED BY GAME MANAGER SPEED UP
        }
    }

    public Vector3 GetRandomDoor() => m_SpawnPoints[Random.Range(0, m_SpawnPoints.Count)].position;
}
