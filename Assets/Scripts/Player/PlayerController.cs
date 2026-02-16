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

    private Vector3 moveInput;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }


    private void Update()
    {
        transform.right = cam.right;
        Vector3 move = transform.forward * moveInput.y + transform.right * moveInput.x;
        move *= movementSpeed * Time.deltaTime;
        characterController.Move(move);
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}