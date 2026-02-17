using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 positionChange;
    public void OnTriggerEnter(Collider other)
    {
        CharacterController controller;
        other.gameObject.TryGetComponent(out controller);
        if (controller != null)
        {
            controller.enabled = false; // Disable to avoid conflicts
            other.transform.position = other.transform.position + positionChange;
            controller.enabled = true; // Re-enable after teleport
        }
    }
}
