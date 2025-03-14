using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{
    public GameObject Normal, Corrupted;

    public MeshRenderer m_Mesh;

    public float m_StartMoveSpeed, m_MaxMoveSpeed;
    public TeamData m_Team { get; set; }

    private NavMeshAgent m_NavMeshAgent;
    private Rigidbody rb;
    private ExpandingCorruptionField m_Corruption;
    
    public LookToCamera lookToCamera;

    private bool pickedUp = false;
    private bool ragdoll = false;

    public bool isDead { get; private set; } = false;

    public void Initialise(TeamData _data)
    {
        m_StartMoveSpeed = Mathf.Min(m_StartMoveSpeed * (1 + (.00005f * GameManager.GAME_TIME)), m_MaxMoveSpeed);
        m_Team = _data;

        m_Mesh.material.color = _data.color;

        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        m_Corruption = GetComponent<ExpandingCorruptionField>();

        StartCoroutine(C_NavigateToChair());
    }

    public void Pickup()
    {
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.updatePosition = false;
        m_NavMeshAgent.updateRotation = false;
        pickedUp = true;
    }
    
    public void Drop()
    {
        if (C_Ragdoll != null)
        {
            StopCoroutine(C_Ragdoll);
            C_Ragdoll = null;
        }
        C_Ragdoll = StartCoroutine(C_RagdollWait());
        pickedUp = false;
    }

    Coroutine C_Ragdoll;
    public IEnumerator C_RagdollWait()
    {
        ragdoll = true;
        yield return new WaitForSeconds(3.0f);
        if(pickedUp) yield break;
        ragdoll = false;
        StartCoroutine(C_NavigateToChair());
        rb.isKinematic = true;
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.nextPosition = transform.position;
        m_NavMeshAgent.updatePosition = true;
        m_NavMeshAgent.updateRotation = true;

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

    private Chair currentChair;
    public IEnumerator C_SitInChair(Chair chair)
    {
        currentChair = chair;
        chair.occupied = true;
        
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.updatePosition = false;
        m_NavMeshAgent.updateRotation = false;

        while (!pickedUp && !ragdoll && !isDead)
        {
            yield return new WaitForFixedUpdate();
            transform.position = chair.sitSocket.position;
        }
        
        m_NavMeshAgent.isStopped = true;
        m_NavMeshAgent.nextPosition = transform.position;
        m_NavMeshAgent.updatePosition = false;
        m_NavMeshAgent.updateRotation = false;
        
        chair.occupied = false;
        currentChair = null;

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!ragdoll || pickedUp) return;

        Entity other = collision.gameObject.GetComponent<Entity>();

        if (other)
        {
            if (other.pickedUp) return;
            
            if(TeamOppositionChartSO.IsOpposingTeam(other.m_Team.name, m_Team.name))
            {
                other.Hit();
                Destroy(gameObject);
            }
        }
    }

    public void Hit()
    {
        isDead = true;
        ragdoll = false;
        if(currentChair != null)
            currentChair.occupied = false;
        currentChair = null;
        StopAllCoroutines();
        rb.isKinematic = true;
        m_NavMeshAgent.isStopped = false;
        m_NavMeshAgent.nextPosition = transform.position;
        m_NavMeshAgent.updatePosition = true;
        m_NavMeshAgent.updateRotation = true;

        m_NavMeshAgent.speed = 1.0f;

        Normal.SetActive(true);
        Corrupted.SetActive(false);
        
        m_Corruption.SetActive(false);

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            Destroy(c);
        }
        
        StartCoroutine(C_NavigateToDoor());
    } 
    
    public IEnumerator C_NavigateToDoor()
    {
        Vector3 door = EntityManager.Instance.GetRandomDoor();
        if (door == Vector3.zero)
        {
            Destroy(gameObject);
        }

        m_NavMeshAgent.SetDestination(door);
    
        while (Vector3.Distance(transform.position, door) > 2 && !pickedUp)
        {
            yield return new WaitForFixedUpdate();
        }
    
        Destroy(gameObject);
    }
    
    
}
