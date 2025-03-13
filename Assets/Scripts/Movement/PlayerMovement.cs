using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;
    private Vector3 moveDirection;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector2 _input)
    {
        moveDirection = new Vector3(_input.x, 0, _input.y) * speed;
    }

    private void Update()
    {
        if (controller != null)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
