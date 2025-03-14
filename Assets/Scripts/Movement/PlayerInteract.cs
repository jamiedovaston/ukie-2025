using System;
using System.Collections;
using UnityEngine;
using JD.Utility.Audio;
using Unity.VisualScripting;

public class PlayerInteract : MonoBehaviour
{
    public float m_Radius = 3.0f;
    public float m_HoldingHeight = 0.5f;

    private Entity m_CurrentlyHeldEntity;

    private bool holding = false;

    public PlayerMovement m_Movement;

    private Animator m_Animator;

    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
    }

    public void Interact()
    {
        if(m_CurrentlyHeldEntity != null)
        {
            holding = false;
            m_Animator.SetBool("Holding", false);
        }
        else
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, m_Radius);
            Entity closestEntity = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider c in collisions)
            {
                Entity e = c.GetComponent<Entity>();
                if (e && !e.isDead)
                {
                    float distance = Vector3.Distance(transform.position, c.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEntity = e;
                    }
                }
            }

            if (closestEntity != null)
            {
                m_CurrentlyHeldEntity = closestEntity;
                closestEntity.Pickup();
                holding = true;
                m_Animator.SetBool("Holding", true);

                UIManager.Instance.SpawnColoursThatCanBeHit(TeamOppositionChartSO.GetAllOpposingTeams(closestEntity.m_Team.name));
                StartCoroutine(C_Holding());
            }
        }
    }

    private IEnumerator C_Holding()
    {
        m_CurrentlyHeldEntity.transform.SetParent(transform);
        
        while (holding)
        {
            m_CurrentlyHeldEntity.transform.localPosition = Vector3.up * m_HoldingHeight;
            m_CurrentlyHeldEntity.lookToCamera.m_LookAtConstraint.rotationOffset = new Vector3(0.0f, 90.0f, 90.0f);
            yield return new WaitForFixedUpdate();
        }

        if (m_CurrentlyHeldEntity == null) yield break;
        
        m_CurrentlyHeldEntity.transform.localPosition = Vector3.zero;
        m_CurrentlyHeldEntity.transform.SetParent(null);
        m_CurrentlyHeldEntity.lookToCamera.m_LookAtConstraint.rotationOffset = new Vector3(90.0f, 0.0f, 0.0f);
        
        Rigidbody rb = m_CurrentlyHeldEntity.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.linearVelocity = m_Movement.moveDirection.normalized * 10.0f;

        UIManager.Instance.DeleteColoursThatCanBeHit();

        m_CurrentlyHeldEntity.Drop();
        m_CurrentlyHeldEntity = null;

    }
}