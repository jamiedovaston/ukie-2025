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
            m_CurrentlyHeldEntity.Drop();
            m_CurrentlyHeldEntity = null;
            Debug.Log("Entity dropped!");
            holding = false;
        }
        else
        {
            Debug.Log("Finding entity...");
            Collider[] collisions = Physics.OverlapSphere(transform.position, m_Radius);
            Entity closestEntity = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider c in collisions)
            {
                Entity e = c.GetComponent<Entity>();
                if (e)
                {
                    Debug.Log("Found Entity!");
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
                Debug.Log($"Entity picked up: {closestEntity.name}");  
                holding = true;
                StartCoroutine(C_Holding());
            }
        }
    }

    private IEnumerator C_Holding()
    {
        m_CurrentlyHeldEntity.transform.SetParent(transform);
        m_CurrentlyHeldEntity.transform.localPosition = Vector3.zero;
        
        while (holding)
        {
            yield return new WaitForFixedUpdate();
        }
        
        m_CurrentlyHeldEntity.transform.SetParent(null);

        Rigidbody rb = m_CurrentlyHeldEntity.GetComponent<Rigidbody>();
        rb.AddForce(m_Movement.moveDirection.normalized * 5.0f, ForceMode.Force);
    }
}