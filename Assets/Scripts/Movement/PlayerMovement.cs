using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Animator m_Animator;
    private CharacterController controller;

    public float speed = 5f;
    [HideInInspector]
    public Vector3 moveDirection;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        m_Animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 _input)
    {
        if (_input == Vector2.zero) m_Animator.SetBool("Walking", false);
        else m_Animator.SetBool("Walking", true);
        moveDirection = new Vector3(_input.x, 0, _input.y) * speed;
        GetComponentInChildren<LookToDirection>().Look(moveDirection);
    }

    private void Update()
    {
        if (controller != null)
        {
            controller.Move(moveDirection * Time.deltaTime);
        
            // Ensure y position is always 1
            Vector3 newPosition = transform.position;
            newPosition.y = 1;
            transform.position = newPosition;
        }
    }

}