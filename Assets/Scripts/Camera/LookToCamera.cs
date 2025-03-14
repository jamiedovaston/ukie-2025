using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(LookAtConstraint))]
public class LookToCamera : MonoBehaviour
{
    [HideInInspector]
    public LookAtConstraint m_LookAtConstraint;

    private void Start()
    {
        if (m_LookAtConstraint == null)
        {
            m_LookAtConstraint = GetComponent<LookAtConstraint>();
        }

        if (m_LookAtConstraint != null && Camera.main != null)
        {
            // Create a ConstraintSource for the main camera
            ConstraintSource source = new ConstraintSource
            {
                sourceTransform = Camera.main.transform,
                weight = 1f // Full influence
            };

            // Clear existing sources and add the new one
            m_LookAtConstraint.SetSources(new System.Collections.Generic.List<ConstraintSource> { source });

            // Enable the constraint
            m_LookAtConstraint.constraintActive = true;
        }
        else
        {
            Debug.LogWarning("LookAtConstraint or Camera.main is missing!");
        }
    }
}
