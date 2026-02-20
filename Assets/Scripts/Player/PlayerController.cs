using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Transform cam;

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float gravity = -9.81f;


    private Vector3 moveInput;
    private bool isGrounded;
    private float verticalVelocity;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }


    private void Update()
    {
        if (!characterController.enabled) return;

        transform.right = cam.right;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        move *= movementSpeed * Time.deltaTime;

        isGrounded = characterController.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Reset velocity when grounded
        }

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity * Time.deltaTime;

        characterController.Move(move);
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}