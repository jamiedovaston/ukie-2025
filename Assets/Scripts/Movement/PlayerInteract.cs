using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float m_Radius = 3.0f;

    private Entity m_CurrentlyHeldEntity;

    public void Interact()
    {
        if(m_CurrentlyHeldEntity != null)
        {
            m_CurrentlyHeldEntity.Drop();
            m_CurrentlyHeldEntity = null;
        }
        else
        {
            Collider[] collisions = Physics.OverlapSphere(transform.position, m_Radius, 3);
            Entity closestEntity = null;
            float closestDistance = Mathf.Infinity;

            foreach (Collider c in collisions)
            {
                Entity e = c.GetComponent<Entity>();
                if (e)
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
            }
        }

    }

}