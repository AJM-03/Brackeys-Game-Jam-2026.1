using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        CharacterController controller;
        other.gameObject.TryGetComponent(out controller);
        if (controller != null)
        {
            if (GameManager.Instance.evidenceRequired <= GameManager.Instance.objectsCaptured)
            {
                controller.enabled = false;
                GameManager.Instance.EndDay();
            }
        }
    }
}
