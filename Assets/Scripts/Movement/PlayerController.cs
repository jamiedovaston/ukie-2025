using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_Movement;
    [SerializeField] private PlayerInteract m_Interact;

    InputSystem_Actions m_ActionMap;

    private void Awake()
    {
        m_Movement = GetComponent<PlayerMovement>();
        m_Interact = GetComponent<PlayerInteract>();
    }

    public void OnEnable()
    {
        m_ActionMap = new InputSystem_Actions();

        m_ActionMap.Enable();

        m_ActionMap.Player.Move.performed += Move_performed;
        m_ActionMap.Player.Move.canceled += Move_canceled;

        m_ActionMap.Player.Interact.performed += Interact_performed;
    }

    private void OnDisable()
    {
        m_ActionMap.Player.Move.performed -= Move_performed;
        m_ActionMap.Player.Move.canceled -= Move_canceled;

        m_ActionMap.Player.Interact.performed -= Interact_performed;

        m_ActionMap.Disable();
    }

    #region Actions

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("E");
        m_Interact.Interact();
    }
    private void Move_performed(InputAction.CallbackContext obj)
    {
        Vector2 input = obj.ReadValue<Vector2>();

        Debug.Assert(input != null, "Movement values null!");

        m_Movement.Move(input);
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        m_Movement.Move(Vector2.zero);
    }

    #endregion

}
