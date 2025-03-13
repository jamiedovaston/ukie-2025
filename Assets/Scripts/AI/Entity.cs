using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{
    public MeshRenderer m_Mesh;

    private float m_MoveSpeed;
    private TeamData m_Team;

    private NavMeshAgent m_NavMeshAgent;
    private Rigidbody rb;

    private bool pickedUp = false;

    public void Initialise(float _moveSpeed, TeamData _data)
    {
        m_MoveSpeed = _moveSpeed;
        m_Team = _data;

        m_Mesh.material.color = _data.color;

        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        StartCoroutine(C_NavigateToChair());
    }

    public void Pickup()
    {
        m_NavMeshAgent.isStopped = true;
        pickedUp = true;
    }
    
    public void Drop()
    {
        if (C_Ragdoll != null) StopCoroutine(C_Ragdoll);
        C_Ragdoll = StartCoroutine(C_RagdollWait());
        pickedUp = false;
    }

    Coroutine C_Ragdoll;
    public IEnumerator C_RagdollWait()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(C_NavigateToChair());
        rb.isKinematic = true;
        m_NavMeshAgent.isStopped = false;
    }

    public IEnumerator C_NavigateToChair()
    {
        while (!pickedUp)
        {
            Chair chair = Chair.GetRandomChair();
            if (chair == null) yield break; // No chair found, exit coroutine

            m_NavMeshAgent.SetDestination(chair.position);

            while (Vector3.Distance(transform.position, chair.position) > 2 && !pickedUp)
            {
                yield return new WaitForFixedUpdate();
            }

            if (!chair.occupied)
            {
                StartCoroutine(C_SitInChair(chair));
                yield break; // Exit the while loop and coroutine once a chair is found and occupied
            }
            else
            {
                yield return new WaitForSeconds(0.5f); // Delay before trying to find another chair
            }
        }
    }

    public IEnumerator C_SitInChair(Chair chair)
    {
        chair.occupied = true;

        while (!pickedUp)
        {
            yield return new WaitForFixedUpdate();
            transform.position = chair.sitSocket.position;
        }

        chair.occupied = false;
    }
}
