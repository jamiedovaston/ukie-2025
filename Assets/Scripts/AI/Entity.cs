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
        pickedUp = true;
    }

    public void Drop()
    {
        StartCoroutine(C_RagdollWait());
        pickedUp = false;
        m_NavMeshAgent.isStopped = true;
        rb.isKinematic = false;
    }

    public IEnumerator C_RagdollWait()
    {
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(C_NavigateToChair());
        rb.isKinematic = true;
        m_NavMeshAgent.isStopped = false;
    }

    public IEnumerator C_NavigateToChair()
    {
        IChairable chair = Chair.GetRandomChair();
        m_NavMeshAgent.SetDestination(chair.position); // chair

        while (Vector3.Distance(transform.position, chair.position) > 2 && !pickedUp)
        {
            yield return new WaitForFixedUpdate();
        }

        if(!chair.occupied) StartCoroutine(C_SitInChair(chair));
        else StartCoroutine(C_NavigateToChair());
    }

    public IEnumerator C_SitInChair(IChairable chair)
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