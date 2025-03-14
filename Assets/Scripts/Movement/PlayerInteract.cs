using System.Collections;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float m_Radius = 3.0f;

    private Entity m_CurrentlyHeldEntity;

    private bool holding = false;

    public PlayerMovement m_Movement;

    public void Interact()
    {
        if(m_CurrentlyHeldEntity != null)
        {
            holding = false;
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
                StartCoroutine(C_Holding());
            }
        }
    }

    private IEnumerator C_Holding()
    {
        m_CurrentlyHeldEntity.transform.SetParent(transform);
        
        while (holding)
        {
            m_CurrentlyHeldEntity.transform.localPosition = Vector3.up * m_Radius;
            m_CurrentlyHeldEntity.lookToCamera.m_LookAtConstraint.rotationOffset = new Vector3(90.0f, -90.0f, 0.0f);
            yield return new WaitForFixedUpdate();
        }

        if (m_CurrentlyHeldEntity == null) yield break;
        
        m_CurrentlyHeldEntity.transform.SetParent(null);
        m_CurrentlyHeldEntity.lookToCamera.m_LookAtConstraint.rotationOffset = new Vector3(90.0f, 0.0f, 0.0f);

        Rigidbody rb = m_CurrentlyHeldEntity.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.linearVelocity = m_Movement.moveDirection.normalized * 10.0f;

        m_CurrentlyHeldEntity.Drop();
        m_CurrentlyHeldEntity = null;

    }
}