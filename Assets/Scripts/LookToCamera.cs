using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Camera m_Camera;

    private void Start() => m_Camera = Camera.main; // Use main camera

    private void Update()
    {
        Vector3 upDirection = (m_Camera.transform.position - transform.position).normalized;

        // Define a stable forward direction (perpendicular to up)
        Vector3 forwardDirection = Vector3.Cross(upDirection, transform.right).normalized;

        // Ensure forward direction is truly perpendicular
        forwardDirection = Vector3.Cross(upDirection, forwardDirection).normalized;

        // Apply rotation: Y-axis points to camera
        transform.rotation = Quaternion.LookRotation(forwardDirection, upDirection);
    }
}
