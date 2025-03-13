using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{
    private float m_MoveSpeed;
    private TeamData m_Team;

    private NavMeshAgent m_NavMeshAgent;
    private bool pickedUp = false;

    public void Initialise(float _moveSpeed, TeamData _data)
    {
        m_MoveSpeed = _moveSpeed;
        m_Team = _data;

        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(C_NavigateToChair());
    }

    public void Pickup(bool _pickup)
    {
        if(pickedUp) //if already picked up (aka dropping)
            StartCoroutine(C_RagdollWait());

        pickedUp = _pickup;
        m_NavMeshAgent.enabled = !_pickup;
    }

    public IEnumerator C_RagdollWait()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(C_NavigateToChair());
    }

    public IEnumerator C_NavigateToChair()
    {
        m_NavMeshAgent.SetDestination(Vector3.zero); // chair

        while (Vector3.Distance(transform.position, m_NavMeshAgent.destination) > 2 || !pickedUp)
        {
            yield return null;
        }
    }
}