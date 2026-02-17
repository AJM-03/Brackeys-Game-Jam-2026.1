using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactLayers;
    private Interactable hoveredObject;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        InteractionDetection();
    }

    private void InteractionDetection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, interactLayers))
        {
            Interactable interactable;
            hit.transform.TryGetComponent(out interactable);
            if (interactable != null)
            {
                if (interactable != hoveredObject)
                {
                    if (hoveredObject) hoveredObject.StopHover();
                    hoveredObject = interactable;
                    hoveredObject.Hover();
                }
            }
            else hoveredObject = null;
        }
        else
            hoveredObject = null;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && hoveredObject != null)
        {
            hoveredObject.Interact();
        }
    }
}
