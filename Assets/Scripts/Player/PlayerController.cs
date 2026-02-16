using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private Transform cam;

    [SerializeField] private float movementSpeed = 10f;

    private Vector3 moveInput;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        Vector3 move = moveInput * movementSpeed * Time.deltaTime;
        characterController.Move(move);
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveInput = transform.forward * context.ReadValue<Vector2>().y + transform.right * context.ReadValue<Vector2>().x;
    }
}
